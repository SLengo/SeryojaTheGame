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
    public class Obstacle
    {
        MainWindow _mainWindow;
        BetterRandom RandForObst;
        public Rect ObstacleHitBox;
        public Rectangle ObstToCanvas;
        public bool ShowHitBoxes = true;
        public bool Hitted = false;

        public double ObstDamage = 0.5;

        public Obstacle(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RandForObst = new BetterRandom();


            ObstToCanvas = new Rectangle();
            VisualBrush vb_for_background = new VisualBrush();
            vb_for_background.Stretch = Stretch.Fill;
            int image_num = RandForObst.Between(1,4);
            vb_for_background.Visual = (Visual)Application.Current.Resources["bact_" + image_num];
            ObstToCanvas.Fill = vb_for_background;
            ObstToCanvas.Width = RandForObst.Between(30, 60);
            ObstToCanvas.Name = "obst";
            ObstToCanvas.Height = RandForObst.Between(30, 60);
            VisualBrush vb = new VisualBrush();
            vb.Visual = (Visual)Application.Current.Resources["bact_" + image_num];
            ObstToCanvas.OpacityMask = vb;
            ObstToCanvas.Margin = new Thickness(
                RandForObst.Between(0, (int)(_mainWindow.MainCanvas.ActualWidth - ObstToCanvas.Width)),
                RandForObst.Between(-(int)_mainWindow.MainCanvas.ActualHeight, -50),
                0, 0);
            _mainWindow.MainCanvas.Children.Add(ObstToCanvas);
        }

        public Rect GetHitBoxObst()
        {
            ObstacleHitBox.Width = ObstToCanvas.Width * 0.8;
            ObstacleHitBox.Height = ObstToCanvas.Height * 0.8;
            ObstacleHitBox.X = ObstToCanvas.Margin.Left + ((ObstToCanvas.Width - ObstToCanvas.Width) / 2);
            ObstacleHitBox.Y = ObstToCanvas.Margin.Top + ((ObstToCanvas.Height - ObstToCanvas.Height) / 2);
            return ObstacleHitBox;
        }

        public void ObstacleFiredAnimation()
        {
            AnimationsRace.AnimationObstFired(this);
        }
    }
}
