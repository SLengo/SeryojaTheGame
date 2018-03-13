using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Race
{
    public class HealthBonus : Bonus
    {
        int healthbonus_side = 25;
        MainWindow _mainWindow;
        BetterRandom RandForBonus;

        public int health_count = 40;

        public HealthBonus(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RandForBonus = new BetterRandom();

            bonus_rectangle = new Rectangle();
            bonus_rectangle.Width = healthbonus_side;
            bonus_rectangle.Height = healthbonus_side;
            VisualBrush vb_for_background = new VisualBrush();
            vb_for_background.Stretch = Stretch.Fill;
            vb_for_background.Visual = (Visual)Application.Current.Resources["HpBonus"];
            bonus_rectangle.Fill = vb_for_background;
            bonus_rectangle.Margin = new Thickness(
                RandForBonus.Between(0, (int)(_mainWindow.MainCanvas.ActualWidth - bonus_rectangle.Width)),
                RandForBonus.Between(-(int)_mainWindow.MainCanvas.ActualHeight, -50),
                0, 0);
            _mainWindow.MainCanvas.Children.Add(bonus_rectangle);
        }
    }
}
