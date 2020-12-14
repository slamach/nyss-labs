using CryptoApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CryptoApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController homeController;

        [TestInitialize]
        public void SetupContext()
        {
            homeController = new HomeController();

        }

        [TestMethod]
        public void IndexViewIsIndexCshtml()
        {
            ViewResult viewResult = homeController.Index() as ViewResult;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void EncryptEmptyTextIsRight()
        {
            string result = homeController.Encrypt("", "тест");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void EncryptNullTextIsRight()
        {
            string result = homeController.Encrypt(null, "тест");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void EncryptEmptyKeyIsRight()
        {
            string result = homeController.Encrypt("ура тесты!", "");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void EncryptNullKeyIsRight()
        {
            string result = homeController.Encrypt("ура тесты!", null);

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void EncryptNotNullOrEmptyIsRight()
        {
            string result = homeController.Encrypt("ура тесты!", "тест");

            Assert.AreEqual("ёхс ечцдн!", result);
        }

        [TestMethod]
        public void DecryptEmptyTextIsRight()
        {
            string result = homeController.Decrypt("", "тест");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void DecryptNullTextIsRight()
        {
            string result = homeController.Decrypt(null, "тест");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void DecryptEmptyKeyIsRight()
        {
            string result = homeController.Decrypt("ёхс ечцдн!", "");

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void DecryptNullKeyIsRight()
        {
            string result = homeController.Decrypt("ёхс ечцдн!", null);

            Assert.AreEqual("При обработке данных на сервере произошла ошибка.", result);
        }

        [TestMethod]
        public void DecryptNotNullOrEmptyIsRight()
        {
            string result = homeController.Decrypt("ёхс ечцдн!", "тест");

            Assert.AreEqual("ура тесты!", result);
        }

        [TestMethod]
        public void UploadNullIsRight()
        {
            HttpPostedFileBase file = null;

            Assert.AreEqual("При обработке файла на сервере произошла ошибка.", homeController.Upload(file));
        }

        [TestMethod]
        public void UploadNotNullIsRight()
        {
            string text = "unit тесты";

            using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(text)))
            {
                Mock<HttpPostedFileBase> fileMock = new Mock<HttpPostedFileBase>();
                fileMock.Setup(f => f.InputStream).Returns(memoryStream);
                fileMock.Setup(f => f.FileName).Returns("test.txt");

                Assert.AreEqual(text, homeController.Upload(fileMock.Object));
            }
        }

        [TestMethod]
        public void DownloadTxtNotEmptyOrNullTextReturnsNotEmptyFile()
        {
            FileContentResult downloadedFile = homeController.Download("test", ".txt") as FileContentResult;

            Assert.IsTrue(downloadedFile.FileContents.Length > 0);
        }

        [TestMethod]
        public void DownloadDocxNotEmptyOrNullTextReturnsNotEmptyFile()
        {
            FileContentResult downloadedFile = homeController.Download("test", ".docx") as FileContentResult;

            Assert.IsTrue(downloadedFile.FileContents.Length > 0);
        }

        [TestMethod]
        public void DownloadWrongTypeReturnsRightCodeResult()
        {
            HttpStatusCodeResult codeResult = homeController.Download("test", ".test") as HttpStatusCodeResult;

            Assert.IsTrue(codeResult.StatusCode == 204);
        }

        [TestMethod]
        public void DownloadNullTypeReturnsRightCodeResult()
        {
            HttpStatusCodeResult codeResult = homeController.Download("test", null) as HttpStatusCodeResult;

            Assert.IsTrue(codeResult.StatusCode == 204);
        }

        [TestMethod]
        public void DownloadEmptyTextReturnsRightCodeResult()
        {
            HttpStatusCodeResult codeResult = homeController.Download("", ".txt") as HttpStatusCodeResult;

            Assert.IsTrue(codeResult.StatusCode == 204);
        }

        [TestMethod]
        public void DownloadNullTextReturnsRightCodeResult()
        {
            HttpStatusCodeResult codeResult = homeController.Download(null, ".txt") as HttpStatusCodeResult;

            Assert.IsTrue(codeResult.StatusCode == 204);
        }
    }
}
