#nullable disable

namespace VC.Wallet.Core
{
    internal class PNGService : IImageService
    {
        public string ReadEmbeddedTextFromImageMetaData(byte[] imageBytes, string keyWord)
        {
            PNG png = new PNG(imageBytes);
            return png.GetITXtChunkByKeyword(keyWord);
        }
    }
}