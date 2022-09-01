using BenchmarkDotNet.Attributes;

namespace LiteDecimalPerformance
{
    public class Addition
    {
        [Benchmark]
        public LiteDecimal AddLiteDecimal()
        {

            return new LiteDecimal(7234) + new LiteDecimal(2423840810234);
        }

        [Benchmark]
        public decimal AddDecimal()
        {
            return new decimal(7234) + new decimal(2423840810234);
        }
    }
}
