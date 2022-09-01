namespace LiteDecimalTests
{
    public partial class LiteDecimalTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(-1, -1, 0)]
        [InlineData(239847, 239847, 0)]
        [InlineData(-239847, -239847, 0)]
        [InlineData(int.MaxValue, int.MaxValue, 0)]
        [InlineData(int.MinValue, int.MinValue, 0)]
        public void ConstructorWithInt(int value, long rawValue, int decimalPlaces)
        {
            var lited = new LiteDecimal(value);
            Assert.Equal(rawValue, lited.RawValue);
            Assert.Equal(decimalPlaces, lited.DecimalPlaces);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1U, 1, 0)]
        [InlineData(239847U, 239847, 0)]
        [InlineData(uint.MaxValue, uint.MaxValue, 0)]
        [InlineData(uint.MinValue, uint.MinValue, 0)]
        public void ConstructorWithUInt(uint value, long rawValue, int decimalPlaces)
        {
            var lited = new LiteDecimal(value);
            Assert.Equal(rawValue, lited.RawValue);
            Assert.Equal(decimalPlaces, lited.DecimalPlaces);
        }

        [Theory]
        [InlineData(0L, 0L, 0)]
        [InlineData(1L, 1L, 0)]
        [InlineData(-1L, -1L, 0)]
        [InlineData(239847L, 239847L, 0)]
        [InlineData(-239847L, -239847L, 0)]
        [InlineData(-576460752303423487L, -576460752303423487L, 0)]
        [InlineData(576460752303423487L, 576460752303423487L, 0)]
        public void ConstructorWithLong(long value, long rawValue, int decimalPlaces)
        {
            var lited = new LiteDecimal(value);
            Assert.Equal(rawValue, lited.RawValue);
            Assert.Equal(decimalPlaces, lited.DecimalPlaces);
        }

        [Theory]
        [InlineData(0UL, 0L, 0)]
        [InlineData(1UL, 1L, 0)]
        [InlineData(239847UL, 239847L, 0)]
        [InlineData(576460752303423487UL, 576460752303423487L, 0)]
        public void ConstructorWithULong(ulong value, long rawValue, int decimalPlaces)
        {
            var lited = new LiteDecimal(value);
            Assert.Equal(rawValue, lited.RawValue);
            Assert.Equal(decimalPlaces, lited.DecimalPlaces);
        }

        [Theory]
        [MemberData(nameof(ConstructorWithDecimalData))]
        public void ConstructorWithDecimal(decimal value, long rawValue, int decimalPlaces)
        {
            var lited = new LiteDecimal(value);
            Assert.Equal(rawValue, lited.RawValue);
            Assert.Equal(decimalPlaces, lited.DecimalPlaces);
        }

        public static object[] ConstructorWithDecimalData = new object[]
            {
                new object[] { 0m, 0L, 0 },
                new object[] { 1m, 1L, 0 },
                new object[] { -1m, -1L, 0 },
                new object[] { 1.5m, 15L, 1 },
                new object[] { -1.5m, -15, 1 },
                new object[] { 239847m, 239847L, 0 },
                new object[] { -239847m, -239847L, 0 },
                new object[] { 239.847m, 239847L, 3 },
                new object[] { -239.847m, -239847L, 3 },
                new object[] { -576460752303423487m, -576460752303423487L, 0 },
                new object[] { 576460752303423487m, 576460752303423487L, 0 },
                new object[] { -57646075230.3423487m, -576460752303423487L, 7 },
                new object[] { 57646075230.3423487m, 576460752303423487L, 7 },
                new object[] { -576.460752303423487m, -576460752303423487L, 15 },
                new object[] { 576.460752303423487m, 576460752303423487L, 15 },
            };
    }
}
