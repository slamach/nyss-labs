using CryptoApp.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Text;
using System.Web;

namespace CryptoApp.Tests.Models
{
    [TestClass]
    public class FileHandlerTest
    {
        private FileHandler fileHandler;

        [TestInitialize]
        public void SetupContext()
        {
            fileHandler = new FileHandler();
        }

        [TestMethod]
        public void ParsedTxtIsRight()
        {
            string text = "unit тесты";

            using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(text)))
            {
                Mock<HttpPostedFileBase> fileMock = new Mock<HttpPostedFileBase>();
                fileMock.Setup(f => f.InputStream).Returns(memoryStream);
                fileMock.Setup(f => f.FileName).Returns("test.txt");

                Assert.AreEqual(text, fileHandler.ParseFile(fileMock.Object));
            }
        }

        [TestMethod]
        public void ParsedDocxIsRight()
        {
            string text = "unit тесты";

            using (MemoryStream memoryStream = new MemoryStream())
            using (WordprocessingDocument document =
                WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document(
                    new Body(
                        new Paragraph(
                            new Run(
                                new Text(text)))));
                document.Save();
                Mock<HttpPostedFileBase> fileMock = new Mock<HttpPostedFileBase>();
                fileMock.Setup(f => f.InputStream).Returns(memoryStream);
                fileMock.Setup(f => f.FileName).Returns("test.docx");

                Assert.AreEqual(text, fileHandler.ParseFile(fileMock.Object));
            }
        }

        [TestMethod]
        public void TextBytesIsRight()
        {
            string text = "hello мир";

            Assert.AreEqual(text, Encoding.UTF8.GetString(fileHandler.GetTextBytes(text)));
        }

        [TestMethod]
        public void DocBytesIsRight()
        {
            string text = "hello мир";

            using (MemoryStream memoryStream = new MemoryStream(fileHandler.GetDocBytes(text)))
            using (WordprocessingDocument document = WordprocessingDocument.Open(memoryStream, false))
            {
                Assert.AreEqual(text, document.MainDocumentPart.Document.Body.InnerText);
            }
        }
    }
}
