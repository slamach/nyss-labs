using CryptoApp.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace CryptoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public string Encrypt(string inputText, string key)
        {
            if (!String.IsNullOrEmpty(inputText) && !String.IsNullOrEmpty(key))
            {
                VigenereCryptographer cryptographer = new VigenereCryptographer();
                return cryptographer.Encrypt(inputText, key);
            }
            else
            {
                return "При обработке данных на сервере произошла ошибка.";
            }
        }

        [HttpPost]
        public string Decrypt(string inputText, string key)
        {
            if (!String.IsNullOrEmpty(inputText) && !String.IsNullOrEmpty(key))
            {
                VigenereCryptographer cryptographer = new VigenereCryptographer();
                return cryptographer.Decrypt(inputText, key);
            }
            else
            {
                return "При обработке данных на сервере произошла ошибка."; ;
            }
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase inputFile)
        {
            string result = null;
            if (inputFile != null)
            {
                FileHandler uploadedFile = new FileHandler();
                result = uploadedFile.ParseFile(inputFile);
            }
            if (result == null)
            {
                result = "При обработке файла на сервере произошла ошибка.";
            }

            return result;
        }

        [HttpPost]
        public ActionResult Download(string outputText, string outputFormat)
        {
            FileHandler fileHandler = new FileHandler();

            if (!String.IsNullOrEmpty(outputText) && outputFormat == ".txt")
            {
                return File(fileHandler.GetTextBytes(outputText),
                    "text/plain", "result.txt");
            }
            else if (!String.IsNullOrEmpty(outputText) && outputFormat == ".docx")
            {
                return File(fileHandler.GetDocBytes(outputText),
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "result.docx");
            }
            else
            {
                return new HttpStatusCodeResult(204);
            }
        }
    }
}