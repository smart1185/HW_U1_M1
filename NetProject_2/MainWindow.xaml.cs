using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace NetProject_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string uri;
        int countV = 0;
        int countB = 0;
        int countE = 0;
        string[] wordStr;
        string very = "потоки";
        string big = "ресурс";
        string even = "проблема";

        public MainWindow()
        {
            InitializeComponent();

            uri = txbUri.Text;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        wordStr = line.Split(' ');

                        foreach (var item in wordStr)
                        {
                            if (item.Equals(very))
                                countV++;
                            else if (item.Equals(big))
                                countB++;
                            else if (item.Equals(even))
                                countE++;

                        }
                    }

                    txbVeryWord.Text = countV.ToString();
                    txbBigWord.Text = countB.ToString();
                    txbEvenWord.Text = countE.ToString();
                }
            }
            response.Close();
            Console.WriteLine("Запрос выполнен");
            Console.Read();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(fileDouwnLoadMethod));
            myThread.Start();            
        }

        private void fileDouwnLoadMethod ()
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(uri, "myFile.pdf");
                txbFile.Text = "Файл загружен";
            }
            catch(Exception ex)
            {
                txbFile.Text = ex.Message;
            }

        


        }
    }
}
