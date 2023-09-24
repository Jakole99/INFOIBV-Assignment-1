using System;
using System.Linq;
using System.Threading.Tasks;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class HistogramEqualizationFilter: Filter
    {
        private byte[] _lookUpTable;
        private int _m, _n, _k;
        private int _highest, _lowest;
        
        public override string Identifier => "Equalization";

        protected override async Task BeforeExecuteAsync(byte[,] input)
        {
            _lookUpTable = await CreateLookUpTable(input);
        }

        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            var a = input[u, v];
            var equalizedIntensity = (byte)(_lookUpTable[a] * (_k - 1) / (_m*_n));
            //var equalizedIntensity = (byte)(((LookUpTable[a] - _lowest) / (_highest - _lowest)) * (_K - 1));
            return equalizedIntensity;
        }

        internal async Task<byte[]> CreateLookUpTable(byte[,] input)
        {
            var histogramTable = new byte[Byte.MaxValue+1];
            _m = input.GetLength(0);
            _n = input.GetLength(1);

            _highest = input.Cast<byte>().Max();
            _lowest = input.Cast<byte>().Min();
            _k = _highest - _lowest;

            // Run the mapping on another thread
            await Task.Run(() =>
            {
                Parallel.For(0, input.Length, i =>
                {
                    var u = i % _m;
                    var v = i / _m;

                    var intensity = input[u,v];
                    histogramTable[intensity] += 1;
                });
            });

            var cumulativeHistogramTable = FilterHelper.Cumulation(histogramTable);
            return cumulativeHistogramTable;

        }

    }
    public static partial class PipelineExtensions
    {
        public static PipeLine AddHistogramEqualization(this PipeLine pipeLine)
        {
            return pipeLine.AddFilter(new HistogramEqualizationFilter());
        }
    }
}
