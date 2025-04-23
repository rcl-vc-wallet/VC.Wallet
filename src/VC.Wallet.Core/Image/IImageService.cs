namespace VC.Wallet.Core
{
    public interface IImageService
    {
        public string ReadEmbeddedTextFromImageMetaData(byte[] imageBytes, string keyWord);
    }
}
