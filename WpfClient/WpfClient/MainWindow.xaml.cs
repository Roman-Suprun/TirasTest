using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                TextBox1.Text
            });
            using (var client = new WebClient())
            {
                //var dataString = JsonConvert.SerializeObject(vm);

                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(new Uri("http://localhost:2714/api/values/pd"), "POST", json);
            }

        }

        struct MyStruct
        {
            public int Id;
            public string UserName;
        }
        private void GetBtn_Click(object sender, RoutedEventArgs e)
        {
            string data = string.Empty;
            string url = @"http://localhost:2714/api/values/getusers";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }

            JObject jsonResponse = JObject.Parse(data);
            string[] mas = new string[jsonResponse.First.First.Count()];
            
            for (int i = 0; i < jsonResponse.First.First.Count(); i++)
            {
                mas[i] = (jsonResponse.First.First[i].Last.Last()).ToString();
                
            }
           
            tb2.Text = string.Join("\n", mas);
        }
    }
}
