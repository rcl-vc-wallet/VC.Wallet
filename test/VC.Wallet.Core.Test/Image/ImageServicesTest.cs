#nullable disable

namespace VC.Wallet.Core.Test
{
    [TestClass]
    public class ImageServicesTest
    {
        private readonly IImageFactory _imageFactory;
        private const string imageFilePath = @"c:/test/testImage.PNG";
        private const string keyWord = "openbadges";

        public ImageServicesTest()
        {
            _imageFactory = (IImageFactory)DependencyResolver.ServiceProvider().GetService(typeof(IImageFactory));
        }

        [TestMethod]
        public void ReadEmbeddedTextInPNGTest()
        {
            try
            {
                IImageService imageService = _imageFactory.Create(ImageType.PNG);
                string txt = imageService.ReadEmbeddedTextFromImageMetaData(File.ReadAllBytes(imageFilePath),
                   keyWord);

                Assert.AreEqual("", txt);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
