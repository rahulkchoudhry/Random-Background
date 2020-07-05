using Newtonsoft.Json;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;

namespace random_background
{
    class Program
    {

        // user32.dll is needed to set the desktop wallpaper and update the system files.
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);
        private static UInt32 SPI_SETDESKWALLPAPER = 20;
        private static UInt32 SPIF_UPDATEINIFILE = 0x1;

        static void Main(string[] args)
        {

            // Get JSON info from unsplash through their API.
            string apiURL = "https://api.unsplash.com/";
            string clientID = "+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+";
            string strURL = apiURL + "/photos/random" + "?client_id=" + clientID + "&query=WALLPAPER&orientation=landscape";

            WebClient wc = new WebClient();
            string strJson = wc.DownloadString(strURL);

            // Obtain URL from JSON info using a dynamic object.
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strJson);
            string url = dobj["urls"]["raw"].ToString() + "&w=1920&h=1080&fit=crop";

            // Download from unsplash and save image and data to to local application data.
            DirectoryInfo di = Directory.CreateDirectory(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Random Background");
            string imageFileName = Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Random Background\\background.jpg";
            wc.DownloadFile(url, imageFileName);
            System.IO.File.WriteAllText(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Random Background\\information.json", strJson);

            // Update system parameters with the new wallpaper file.
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imageFileName, SPIF_UPDATEINIFILE);

        }
    }
}
