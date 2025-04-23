using System.Text;

namespace VC.Wallet.Core
{
    internal class PNG
    {
        private readonly byte[] _header;
        private readonly IList<Chunk> _chunks;

        public PNG(byte[] ImageBytes)
        {
            _header = new byte[8];
            _chunks = new List<Chunk>();

            using (var memoryStream = new MemoryStream(ImageBytes))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);

                memoryStream.Read(_header, 0, _header.Length);

                while (memoryStream.Position < memoryStream.Length)
                    _chunks.Add(ChunkFromStream(memoryStream));

                memoryStream.Close();
            }
        }

        public string GetITXtChunkByKeyword(string keyWord)
        {
            if (_chunks.Count > 0)
            {
                foreach (var chunk in _chunks)
                {
                    string type = Encoding.UTF8.GetString(chunk._type);

                    if (type == "iTXt")
                    {
                        return Encoding.UTF8.GetString(chunk._data).Replace(keyWord, "").Replace("\0", "");
                    }
                }
            }

            return string.Empty;
        }

        private static Chunk ChunkFromStream(Stream stream)
        {
            var length = ReadBytes(stream, 4);
            var type = ReadBytes(stream, 4);
            var data = ReadBytes(stream, Convert.ToInt32(BitConverter.ToUInt32(length.Reverse().ToArray(), 0)));

            stream.Seek(4, SeekOrigin.Current);

            return new Chunk(type, data);
        }

        private static byte[] ReadBytes(Stream stream, int n)
        {
            var buffer = new byte[n];
            stream.Read(buffer, 0, n);
            return buffer;
        }

        private class Chunk
        {
            public byte[] _type { get; private set; }
            public byte[] _data { get; private set; }

            public Chunk(byte[] type, byte[] data)
            {
                _type = type;
                _data = data;
            }
        }
    }
}
