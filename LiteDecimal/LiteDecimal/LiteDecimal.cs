using System.Diagnostics.CodeAnalysis;

namespace System
{
    public readonly struct LiteDecimal : IEquatable<LiteDecimal>, IComparable<LiteDecimal>
    {
        public const long DecimalCountBitMask = 0b01111000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
        public const long DecimalCountBitMaskInverse = ~DecimalCountBitMask;
        public const long ValueBitMask = 0b00000111_11111111_11111111_11111111_11111111_11111111_11111111_11111111;
        public const long SignBitMask = long.MinValue;
        public const long MaxValueLong = 0b00000111_11111111_11111111_11111111_11111111_11111111_11111111_11111111;
        public const long MinValueLong = ~MaxValueLong;

        public static readonly LiteDecimal MaxValue = new LiteDecimal(MaxValueLong);

        private static readonly long[] Power10 = new long[]
        {
            1,
            10,
            100,
            1000,
            10_000,
            100_000,
            1_000_000,
            10_000_000,
            100_000_000,
            1_000_000_000,
            10_000_000_000,
            100_000_000_000,
            1_000_000_000_000,
            10_000_000_000_000,
            100_000_000_000_000,
            1_000_000_000_000_000,
        };

        internal readonly long _value;

        public static long SignBitMaskP => SignBitMask;

        public LiteDecimal(int value)
        {
            _value = value & DecimalCountBitMaskInverse;
        }

        public LiteDecimal(uint value)
        {
            _value = value & DecimalCountBitMaskInverse;
        }

        public LiteDecimal(long value)
        {
            if (value < MinValueLong || value > MaxValueLong)
                throw new OverflowException($"value {value} is outside of valid range.");

            _value = value & DecimalCountBitMaskInverse;
        }

        public LiteDecimal(ulong value)
        {
            if (value > MaxValueLong)
                throw new OverflowException($"value {value} is outside of valid range.");

            _value = ((long)value) & DecimalCountBitMaskInverse;
        }

        public LiteDecimal(Span<byte> bytes)
        {
            _value = BitConverter.ToInt64(bytes);
        }

        public LiteDecimal(decimal d)
        {
            Span<int> bytes = stackalloc int[4];
            decimal.TryGetBits(d, bytes, out _);
            var low = bytes[0];
            var mid = bytes[1];
            var high = bytes[2];
            var flags = bytes[3];
            var sign = flags & int.MinValue;
            var scale = (flags & 0x00FF0000) >> 16;

            var longValue = (((long)mid) << 32) | (uint)low;
            if (high == 0 && longValue >= MinValueLong && longValue <= MaxValueLong && scale <= 15)
            {
                longValue = sign != 0 ? ((-longValue) | SignBitMask) : longValue;
                _value = (longValue & DecimalCountBitMaskInverse) | (((long)scale) << 59);
            }
            else
                throw new OverflowException($"value {d} is outside of valid range.");
        }

        internal LiteDecimal(long rawValue, int decimalPlaces)
        {
            _value = (rawValue & DecimalCountBitMaskInverse) | (((long)decimalPlaces) << 59);
        }

        internal int DecimalPlaces => (int)((_value & DecimalCountBitMask) >> 59);

        internal long RawValue => (_value & SignBitMask) != 0 ? _value | DecimalCountBitMask : _value & DecimalCountBitMaskInverse;

        public override string ToString()
        {
            var signedValue = RawValue;
            var devisor = DecimalPlaces;
            decimal value = new decimal(signedValue) / (decimal)Math.Pow(10, devisor);
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return 275 + _value.GetHashCode();
        }

        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(_value);
        }

        public bool TryWriteBytes(Span<byte> bytes)
        {
            return BitConverter.TryWriteBytes(bytes, _value);
        }

        public static LiteDecimal FromBytes(Span<byte> bytes)
        {
            return new LiteDecimal(bytes);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is LiteDecimal other)
            {
                return Equals(this, other);
            }

            return base.Equals(obj);
        }

        public bool Equals(LiteDecimal other)
        {
            return _value == other._value;
        }

        public int CompareTo(LiteDecimal other)
        {
            var thisDecimalPlaces = DecimalPlaces;
            var otherDecimalPlaces = other.DecimalPlaces;
            var difference = thisDecimalPlaces - otherDecimalPlaces;
            var thisValue = difference > 0 ? _value : (_value * Power10[-difference]);
            var otherValue = difference > 0 ? (other._value * Power10[difference]) : other._value;
            if (thisValue > otherValue)
            {
                return 1;
            }
            else if (thisValue < otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static LiteDecimal operator +(LiteDecimal left, LiteDecimal right)
        {
            return AddOrSubtract(left, right);
        }

        public static LiteDecimal operator -(LiteDecimal left, LiteDecimal right)
        {
            return AddOrSubtract(left, right, true);
        }

        private static LiteDecimal AddOrSubtract(LiteDecimal left, LiteDecimal right, bool subtract = false)
        {
            var leftDecimalPlaces = left.DecimalPlaces;
            var rightDecimalPlaces = right.DecimalPlaces;
            long leftValueScaled, rightValueScaled;

            if (leftDecimalPlaces != rightDecimalPlaces)
            {
                var leftValue = left._value;
                var rightValue = right._value;
                var difference = leftDecimalPlaces - rightDecimalPlaces;
                leftValueScaled = difference < 0 ? (leftValue * Power10[-difference]) : leftValue;
                rightValueScaled = difference > 0 ? (rightValue * Power10[difference]) : rightValue;
            }
            else
            {
                leftValueScaled = left._value;
                rightValueScaled = right._value;
            }

            var rawValue = !subtract ? leftValueScaled + rightValueScaled : leftValueScaled - rightValueScaled;
            var decimalPlaces = leftDecimalPlaces > rightDecimalPlaces ? leftDecimalPlaces : rightDecimalPlaces;
            while (decimalPlaces > 0 && rawValue % 10 == 0)
            {
                --decimalPlaces;
                rawValue /= 10;
            }
            return new LiteDecimal(rawValue, decimalPlaces);
        }

        public static bool operator ==(LiteDecimal left, LiteDecimal right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LiteDecimal left, LiteDecimal right)
        {
            return !(left == right);
        }

        public static bool operator <(LiteDecimal left, LiteDecimal right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(LiteDecimal left, LiteDecimal right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(LiteDecimal left, LiteDecimal right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(LiteDecimal left, LiteDecimal right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
