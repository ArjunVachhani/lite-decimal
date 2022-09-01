namespace ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("1 => " + new LiteDecimal(1));
            Console.WriteLine("-1 => " + new LiteDecimal(-1));
            Console.WriteLine("2 => " + new LiteDecimal(2));
            Console.WriteLine("-2 => " + new LiteDecimal(-2));
            Console.WriteLine($"{int.MaxValue} => " + new LiteDecimal(int.MaxValue));
            Console.WriteLine($"{int.MinValue} => " + new LiteDecimal(int.MinValue));
            Console.WriteLine($"{576460752303423487L} => " + new LiteDecimal(576460752303423487L));
            Console.WriteLine($"{-576460752303423487L} => " + new LiteDecimal(-576460752303423487L));
            Console.WriteLine($"{int.MinValue} bits => " + GetBitString(BitConverter.GetBytes(int.MinValue)));
            Console.WriteLine("-576460752303423488L bits => " + GetBitString(BitConverter.GetBytes(-576460752303423488L)));
            Console.WriteLine("-576460752303423487L bits => " + GetBitString(BitConverter.GetBytes(-576460752303423487L)));
            Console.WriteLine("1 + 1 => " + (new LiteDecimal(1) + new LiteDecimal(1)));
            Console.WriteLine("1 + -1 => " + (new LiteDecimal(1) + new LiteDecimal(-1)));
            Console.WriteLine("1 - 1 => " + (new LiteDecimal(1) - new LiteDecimal(1)));
            Console.WriteLine("1 - -1 => " + (new LiteDecimal(1) - new LiteDecimal(-1)));
            Console.WriteLine("1 + 2 => " + (new LiteDecimal(1) + new LiteDecimal(2)));
            Console.WriteLine("1 - 2 => " + (new LiteDecimal(1) - new LiteDecimal(2)));
            Console.WriteLine("1 + 9 => " + (new LiteDecimal(1) + new LiteDecimal(9)));
            Console.WriteLine("1 - 9 => " + (new LiteDecimal(1) - new LiteDecimal(9)));

            Console.WriteLine($"Decimal {int.MinValue} " + new decimal(int.MinValue));
            Console.WriteLine($"Decimal {int.MinValue} " + new decimal(int.MinValue));


            Console.WriteLine(GetBitString(BitConverter.GetBytes(long.MinValue)));
            long l = long.MinValue;
            Console.WriteLine(GetBitString(BitConverter.GetBytes((ulong)l)));

            Console.WriteLine("2L CompareTO 10L " + 2L.CompareTo(10L));
            Console.WriteLine($"LiteDecimal Max {new LiteDecimal(LiteDecimal.MaxValueLong)} bits => " + GetBitString(new LiteDecimal(LiteDecimal.MaxValueLong).GetBytes()));
            Console.WriteLine($"LiteDecimal Min {new LiteDecimal(LiteDecimal.MinValueLong)} bits => " + GetBitString(new LiteDecimal(LiteDecimal.MinValueLong).GetBytes()));
            var d1 = 0.000_000_000_000_000_000_001m;
            var d2 = 100_000_000_000_000_000_000m;
            Console.WriteLine("Decimal Divide : " + (d1 * d2));

            var d3 = 10m;
            var liteDecimal = new LiteDecimal(d3);
            Console.WriteLine("Decimal Construtor : " + liteDecimal);

            var d4 = -10m;
            var liteDecimal2 = new LiteDecimal(d4);
            Console.WriteLine("Decimal Construtor 2 : " + liteDecimal2);


            var d5 = 10.005m;
            var liteDecimal3 = new LiteDecimal(d5);
            Console.WriteLine("Decimal Construtor : " + liteDecimal3);

            var d6 = -10.005m;
            var liteDecimal4 = new LiteDecimal(d6);
            Console.WriteLine("Decimal Construtor 2 : " + liteDecimal4);


            Console.WriteLine("Decimal overflow : " + ((1111_1111_1111_1111_1111m) + GetTwo()));
        }

        public static decimal GetTwo()
        {
            return 0.12345678901234567890m;
        }

        public static string GetBitString(byte[] bytes) => string.Join(" ", bytes.Reverse().Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
    }
}