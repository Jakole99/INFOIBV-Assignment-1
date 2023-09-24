using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class HistogramEqualizationFilter: Filter
    {
        public byte[] LookUpTable;
        private int _M, _N, _K;
        private int _highest, _lowest;
        
        public override string Identifier => "Equalization";

        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            var a = input[u, v];
            var equalizedIntensity = (byte)(LookUpTable[a] * (_K - 1) / (_M*_N));
            //var equalizedIntensity = (byte)(((LookUpTable[a] - _lowest) / (_highest - _lowest)) * (_K - 1));
            return equalizedIntensity;
        }

        internal async Task<byte[]> CreateLookUpTable(byte[,] input)
        {
            var histogramTable = new byte[Byte.MaxValue+1];
            _M = input.GetLength(0);
            _N = input.GetLength(1);

            _highest = input.Cast<byte>().Max();
            _lowest = input.Cast<byte>().Min();
            _K = _highest - _lowest;

            // Run the mapping on another thread
            await Task.Run(() =>
            {
                Parallel.For(0, input.Length, i =>
                {
                    var u = i % _M;
                    var v = i / _M;

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
        public static PipeLine AddHistogramEqualization(this PipeLine pipeLine, HistogramEqualizationFilter h)
        {
            
            return pipeLine.AddFilter(h);
        }
    }
}
