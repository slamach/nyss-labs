using System.Net;

namespace ParserApp.Services
{
    public static class DownloadService
    {
        public static bool DownloadFile(string address, string dataPath)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(address, dataPath);
                }
                return true;
            }
            catch (WebException exception)
            {
                // MessageBox.Show(exception.ToString(), "Debug");
            }
            return false;
        }
    }
}
