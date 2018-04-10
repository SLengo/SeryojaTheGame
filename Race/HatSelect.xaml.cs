using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;

namespace Race
{
    /// <summary>
    /// Interaction logic for HatSelect.xaml
    /// </summary>
    public partial class HatSelect : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged requirements
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
        public HatSelect()
        {
            InitializeComponent();
            //ImageHat = new BitmapImage();
            GetHats();
        }
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }
        public void SaveDefaultHats()
        {
            for (int i = 1; i < 9; i++)
            {
                BitmapImage img = (BitmapImage)Application.Current.Resources["btm_hat_" + i];
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));
                using (var filestream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "hats/hat_" +
                    Convert.ToString(i) + ".png", FileMode.Create))
                    encoder.Save(filestream);
            }
        }
        public void GetHats()
        {
            if (!(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "hats")))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "hats");
                SaveDefaultHats();
            }
            string[] hats_files = GetFiles(AppDomain.CurrentDomain.BaseDirectory + "hats", "*.png|*.jpg|*.jpeg",
                SearchOption.TopDirectoryOnly);
            if (hats_files.Length == 0)
                SaveDefaultHats();
            else
            {
                foreach (string item in hats_files)
                {
                    hats.Add(System.IO.Path.GetFileName(item));
                }
                all_hat = hats.Count;
            }
            ImageHat = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "hats/" + hats[0]));
            HatView.Source = ImageHat;
            (Application.Current.MainWindow as MainWindow).selected_hat_name = hats[0];
        }

        int current_hat = 0;
        int all_hat = 9;
        List<string> hats = new List<string>();

        BitmapImage image_hat;
        public BitmapImage ImageHat
        {
            get { return image_hat; }
            set
            {
                image_hat = value;
                OnPropertyChanged("ImageHat");
            }
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            if (current_hat - 1 == -1) return;
            current_hat--;
            BitmapImage bitmapImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "hats/" + hats[current_hat]));
            HatView.Source = bitmapImage;
            (Application.Current.MainWindow as MainWindow).selected_hat_name = hats[current_hat];
        }

        private void forw_Click(object sender, RoutedEventArgs e)
        {
            if (current_hat + 1 == all_hat) return;
            current_hat++;
            BitmapImage bitmapImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "hats/" + hats[current_hat]));
            HatView.Source = bitmapImage;
            (Application.Current.MainWindow as MainWindow).selected_hat_name = hats[current_hat];
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            current_hat = 0;
            hats.Clear();
            GetHats();
        }
    }
}
