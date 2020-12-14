using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace CryptoApp.Models
{
    public class FileHandler
    {
        public string ParseFile(HttpPostedFileBase File)
        {
            try
            {
                if (File != null && File.FileName.EndsWith(".txt"))
                {
                    using (StreamReader streamReader = new StreamReader(File.InputStream, Encoding.Default))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
                else if (File != null && File.FileName.EndsWith(".docx"))
                {
                    using (WordprocessingDocument document = WordprocessingDocument.Open(File.InputStream, false))
                    {
                        return document.MainDocumentPart.Document.Body.InnerText;
                    }
                }
            }
            catch (Exception exception) { }

            return null;
        }

        public byte[] GetTextBytes(string outputText)
        {
            return Encoding.UTF8.GetBytes(outputText);
        }

        public byte[] GetDocBytes(string outputText)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (WordprocessingDocument document =
                WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                document.AddMainDocumentPart();
                document.MainDocumentPart.Document = new Document(
                    new Body(
                        new Paragraph(
                            new Run(
                                new Text(outputText)))));
                document.Save();
                return memoryStream.ToArray();
            }
        }
    }
}