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
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Race
{
    public class AmmoBonus : Bonus
    {
        int ammobonus_side = 25;
        MainWindow _mainWindow;
        BetterRandom RandForBonus;

        public int ammo_count = 50;

        public AmmoBonus(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RandForBonus = new BetterRandom();

            bonus_rectangle = new Rectangle();
            bonus_rectangle.Width = ammobonus_side;
            bonus_rectangle.Height = ammobonus_side;
            VisualBrush vb_for_background = new VisualBrush();
            vb_for_background.Stretch = Stretch.Fill;
            vb_for_background.Visual = (Visual)Application.Current.Resources["AmmoBonus"];
            bonus_rectangle.Fill = vb_for_background;
            bonus_rectangle.Margin = new Thickness(
                RandForBonus.Between(0, (int)(_mainWindow.MainCanvas.ActualWidth - bonus_rectangle.Width)),
                RandForBonus.Between(-(int)_mainWindow.MainCanvas.ActualHeight, -50),
                0, 0);
            _mainWindow.MainCanvas.Children.Add(bonus_rectangle);
        }
    }
}
