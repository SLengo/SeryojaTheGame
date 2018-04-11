using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Race
{
    public class Stars
    {
        public List<Ellipse> AllStars;
        BetterRandom RandGenerator;
        MainWindow _mainWindow;

        public DispatcherTimer StarTimer;

        public Stars(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RandGenerator = new BetterRandom();
            AllStars = new List<Ellipse>();
            StarTimer = new DispatcherTimer();
            StarTimer.Interval = TimeSpan.FromMilliseconds(500);
            StarTimer.Tick += StarTimerTick;
            StarTimer.Start();

            int starscount = RandGenerator.Between(50, 100);
            for (int i = 0; i < starscount; i++)
            {
                Ellipse ell = new Ellipse();
                ell.Fill = Brushes.White;
                ell.Name = "star_ellipse";
                ell.Width = 2; ell.Height = 2;
                ell.Margin = new Thickness(
                    RandGenerator.Between(0, (int)_mainWindow.MainCanvas.ActualWidth),
                    RandGenerator.Between(0, (int)_mainWindow.MainCanvas.ActualHeight),
                    ell.Margin.Top,
                    ell.Margin.Bottom
                    );
                AllStars.Add(ell);
                _mainWindow.MainCanvas.Children.Add(ell);
            }
        }
        private void StarTimerTick(object sender, EventArgs e)
        {
            int starscount = RandGenerator.Between(25, 25);
            for (int i = 0; i < starscount; i++)
            {
                Ellipse ell = new Ellipse();
                ell.Fill = Brushes.White;
                ell.Width = 2; ell.Height = 2;
                ell.Margin = new Thickness(
                    RandGenerator.Between(0, (int)_mainWindow.MainCanvas.ActualWidth),
                    RandGenerator.Between(-(int)_mainWindow.MainCanvas.ActualHeight / 2, 0),
                    ell.Margin.Top,
                    ell.Margin.Bottom
                    );
                AllStars.Add(ell);
                _mainWindow.MainCanvas.Children.Add(ell);
            }
            AnimationsRace.AnimationStars(AllStars);
        }
    }
}
