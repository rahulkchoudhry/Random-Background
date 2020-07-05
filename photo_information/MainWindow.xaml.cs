using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace photo_information
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string name;

        string description;


        string profile;


        string url;
        dynamic dobj;

        public MainWindow()
        {
            InitializeComponent();
            string strJson = System.IO.File.ReadAllText(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Random Background\\information.json");
             dobj = JsonConvert.DeserializeObject<dynamic>(strJson);

            

            name = dobj["user"]["name"].ToString();

            description = dobj["description"].ToString();

            profile = dobj["user"]["links"]["html"].ToString();

            url = dobj["urls"]["raw"].ToString() + "&w=1920&h=1080&fit=crop";



            var uriSource = new Uri(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Random Background\\background.jpg", UriKind.Absolute);
            photo.Source = new BitmapImage(uriSource);

            photographer_name.Content = name;
            photo_description.Text = description;

            
        }

        private void download_image_Click(object sender, RoutedEventArgs e)
        {
            //Trigger unsplash download.
            string clientID = "+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+";
            WebClient wc = new WebClient();
            string download_location = dobj["links"]["download_location"].ToString() + "?client_id=" + clientID;

            string downloaded_json = wc.DownloadString(download_location);

            dynamic dobj_link = JsonConvert.DeserializeObject<dynamic>(downloaded_json);

            string download_url = dobj_link["url"].ToString();
            System.Diagnostics.Process.Start(download_url);
        }

        private void visit_profile_Click(object sender, RoutedEventArgs e)
        {  
            System.Diagnostics.Process.Start(profile+ "?utm_source=random_background&utm_medium=referral");
        }
    }
}
