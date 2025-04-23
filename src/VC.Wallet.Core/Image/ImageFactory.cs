namespace VC.Wallet.Core
{
    internal class ImageFactory : IImageFactory
    {
        public IImageService Create(ImageType imageType)
        {
            if (imageType == ImageType.PNG)
            {
                return new PNGService();
            }
            else if (imageType == ImageType.SVG)
            {
                return new SVGService();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
