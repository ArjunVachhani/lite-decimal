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

            Console.WriteLine($"Decimal {int.MinValue} " + new decimal(int.MinValue));
            Console.WriteLine($"Decimal {int.MinValue} " + new decimal(int.MinValue));
        }

        public static string GetBitString(byte[] bytes) => string.Join(" ", bytes.Reverse().Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
    }
}