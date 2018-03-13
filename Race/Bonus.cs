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
    public class Bonus
    {
        public Rectangle bonus_rectangle;
        public bool Hitted = false;
        public Rect GetHitBoxObst()
        {
            Rect BonusHitBox = new Rect();
            BonusHitBox.Width = bonus_rectangle.Width;
            BonusHitBox.Height = bonus_rectangle.Height;
            BonusHitBox.X = bonus_rectangle.Margin.Left;
            BonusHitBox.Y = bonus_rectangle.Margin.Top;
            return BonusHitBox;
        }
    }
}
