using System.Diagnostics.CodeAnalysis;

namespace System
{
    public readonly struct LiteDecimal : IEquatable<LiteDecimal>, IComparable<LiteDecimal>
    {
        public const long DecimalCountBitMask = 0b01111000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
        public const long ValueBitMask = 0b00000111_11111111_11111111_11111111_11111111_11111111_11111111_11111111;
        public const long SignBitMask = long.MinValue;
        public const long MaxValueLong = 0b00000111_11111111_11111111_11111111_11111111_11111111_11111111_11111111;

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
            _value = value & ~DecimalCountBitMask;
        }

        public LiteDecimal(uint value)
        {
            _value = value & ~DecimalCountBitMask;
        }

        public LiteDecimal(long value)
        {
            _value = value & ~DecimalCountBitMask;
        }

        public LiteDecimal(Span<byte> bytes)
        {
            _value = BitConverter.ToInt64(bytes);
        }

        internal int DecimalPlaces => (int)((_value & DecimalCountBitMask) >> 59);

        //alternate ((_value & SignBitMask) != 0) ? (_value | DecimalCountBitMask) : _value;
        internal long RawValue => ((((_value & SignBitMask) >> 63) * -1) * DecimalCountBitMask) | _value;

        public override string ToString()
        {
            var signedValue = RawValue;
            var devisor = (_value & DecimalCountBitMask) >> 59;
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
            throw new NotImplementedException();
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
