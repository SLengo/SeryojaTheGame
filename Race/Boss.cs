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
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace Race
{
    public class Boss
    {
        MainWindow _mainWindow;

        double size_of_ammo = 15;

        public List<Ellipse> CurrentBossAmmos = null;

        public Rectangle bossRectangle;
        public Rectangle BossRectangle
        {
            get { return bossRectangle; }
            set
            {
                bossRectangle = value;
                OnPropertyChanged("BossRectangle");
            }
        }

        double health_point;
        public double BossHealthPoint
        {
            get { return health_point; }
            set
            {
                health_point = value;
                OnPropertyChanged("BossHealthPoint");
            }
        }

        VisualBrush boss_sprite;
        public VisualBrush BossSprite
        {
            get { return boss_sprite; }
            set
            {
                boss_sprite = value;
                OnPropertyChanged("BossSprite");
            }
        }

        public Boss(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            CurrentBossAmmos = new List<Ellipse>();

            health_point = 300;
            BossRectangle = new Rectangle();
            VisualBrush vb = new VisualBrush();
            vb.Visual = (Visual)Application.Current.Resources["boss_regular"];
            BossSprite = vb;
            BossRectangle.Fill = BossSprite;
            BossRectangle.Name = "Boss";
            BossRectangle.Width = 200;
            BossRectangle.Height = 250;
            BossRectangle.Margin = new Thickness(_mainWindow.MainCanvas.ActualWidth / 2 - BossRectangle.Width / 2,
               0,
               0, 0);
            //BossRectangle.Margin = new Thickness(_mainWindow.MainCanvas.ActualWidth / 2 - BossRectangle.Width / 2,
            //    -BossRectangle.Height,
            //    0, 0);
            _mainWindow.MainCanvas.Children.Add(BossRectangle);
        }

        public Rect GetBossHitBox()
        {
            Rect result = new Rect();
            result.Width = BossRectangle.Width * 0.7;
            result.Height = BossRectangle.Height * 0.7;
            result.X = BossRectangle.Margin.Left + ((BossRectangle.Width - result.Width) / 2);
            result.Y = BossRectangle.Margin.Top + ((BossRectangle.Height - result.Height) / 2);
            return result;
        }

        public void BossFire()
        {
            BetterRandom betterRandom = new BetterRandom();
            int count_of_ammos = betterRandom.Between(100, 150);

            this.BossSprite.Visual = (Visual)Application.Current.Resources["boss_mouth_open_angry"];

            for (int i = 0; i < count_of_ammos; i++)
            {
                Ellipse fire = new Ellipse();
                fire.Width = size_of_ammo;
                fire.Height = size_of_ammo;
                fire.Fill = Brushes.Red;
                CurrentBossAmmos.Add(fire);
                fire.Margin = new Thickness(BossRectangle.Margin.Left + BossRectangle.Width / 2 - 10,
                    BossRectangle.Margin.Top + BossRectangle.Height * 0.7,
                    0,0);
                _mainWindow.MainCanvas.Children.Add(fire);
            }
            AnimationsRace.AnimationBossFire(this, _mainWindow);
        }

        #region INotifyPropertyChanged requirements
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
