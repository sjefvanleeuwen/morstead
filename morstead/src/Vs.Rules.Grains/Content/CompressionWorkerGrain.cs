using EasyCompressor;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vs.Rules.Grains.Interfaces.Content
{
    [StatelessWorker]
    [Reentrant]
    public class CompressionWorkerGrain : Grain, ICompressionWorkerGrain
    {
        public async Task<byte[]> Compress(CompressionType type, byte[] uncompressed)
        {
            if (type != CompressionType.LZ4)
                throw new ArgumentException("Only LZ4 Compression is supported at the moment.");
            return await Task.Run(() => {
                LZ4Compressor comp = new LZ4Compressor("", K4os.Compression.LZ4.LZ4Level.L12_MAX);
                return comp.Compress(uncompressed); 
            });
        }

        public async Task<byte[]> Decompress(CompressionType type, byte[] compressed)
        {
            if(type != CompressionType.LZ4)
                throw new ArgumentException("Only LZ4 Compression is supported at the moment.");
            return await Task.Run(() => {
                LZ4Compressor comp = new LZ4Compressor("", K4os.Compression.LZ4.LZ4Level.L12_MAX);
                return comp.Decompress(compressed);
            });
        }
    }
}
