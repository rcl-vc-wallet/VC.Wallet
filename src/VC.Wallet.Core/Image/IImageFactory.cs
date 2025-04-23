namespace VC.Wallet.Core
{
    public interface IImageFactory
    {
        public IImageService Create(ImageType imageType);
    }
}
