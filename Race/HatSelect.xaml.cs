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
            ImageHat = new BitmapImage();
        }

        int current_hat = 0;
        int all_hat = 9;

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
            HatView.Source = (BitmapImage)Application.Current.Resources["btm_hat_" + current_hat];
            (Application.Current.MainWindow as MainWindow).selected_hat = "hat_" + current_hat;
        }

        private void forw_Click(object sender, RoutedEventArgs e)
        {
            if (current_hat + 1 == all_hat) return;
            current_hat++;
            HatView.Source = (BitmapImage)Application.Current.Resources["btm_hat_" + current_hat];
            (Application.Current.MainWindow as MainWindow).selected_hat = "hat_" + current_hat;
        }
        
    }
}
