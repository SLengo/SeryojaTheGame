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
        public List<Rectangle> AllStars;
        BetterRandom RandGenerator;
        MainWindow _mainWindow;

        DispatcherTimer StarTimer;

        public Stars(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RandGenerator = new BetterRandom();
            AllStars = new List<Rectangle>();
            StarTimer = new DispatcherTimer();
            StarTimer.Interval = TimeSpan.FromMilliseconds(8);
            StarTimer.Tick += StarTimerTick;
            StarTimer.Start();
        }
        private void StarTimerTick(object sender, EventArgs e)
        {
            StarTimer.Interval = TimeSpan.FromSeconds(25);
            int starscount = RandGenerator.Between(2, 3);
            for (int i = 0; i < starscount; i++)
            {
                Rectangle ell = new Rectangle();
                VisualBrush vb = new VisualBrush();
                vb.Opacity = RandGenerator.Between(10, 40);
                vb.Visual = (Visual)Application.Current.Resources["cloud_" + Convert.ToString(RandGenerator.Between(1, 3))];
                ell.Fill = vb;
                ell.OpacityMask = vb;
                ell.Name = "star_ellipse";
                ell.Width = RandGenerator.Between(100, 200);
                ell.Height = RandGenerator.Between(40, 60);
                ell.Margin = new Thickness(
                    RandGenerator.Between((int)ell.Width, (int)(_mainWindow.MainCanvas.ActualWidth - ell.Width)),
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
