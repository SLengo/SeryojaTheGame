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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Race
{
    public class AnimationsRace
    {
        public static void AnimationBonus(Bonus bonus)
        {
            ThicknessAnimation ta_bonus = new ThicknessAnimation();
            ta_bonus.From = bonus.bonus_rectangle.Margin;
            ta_bonus.To = new Thickness(bonus.bonus_rectangle.Margin.Left, bonus.bonus_rectangle.Margin.Top +
                Math.Abs(bonus.bonus_rectangle.Margin.Top) + Application.Current.MainWindow.ActualHeight + bonus.bonus_rectangle.Width
                , bonus.bonus_rectangle.Margin.Right, bonus.bonus_rectangle.Margin.Bottom);
            ta_bonus.FillBehavior = FillBehavior.HoldEnd;
            ta_bonus.Completed += (s, _) => AnimationBonusCompleted(bonus.bonus_rectangle);
            BetterRandom rand = new BetterRandom();
            ta_bonus.Duration = TimeSpan.FromSeconds(rand.Between(5, 7));
            bonus.bonus_rectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
        }

        public static void AnimationStars(List<Ellipse> stars)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                ThicknessAnimation ta_stars = new ThicknessAnimation();
                ta_stars.From = stars[i].Margin;
                double stars_to = stars[i].Margin.Top < 0 ? stars[i].Margin.Top * 2 + Application.Current.MainWindow.ActualHeight + stars[i].Width :
                    Application.Current.MainWindow.ActualHeight - stars[i].Margin.Top + stars[i].Width;
                ta_stars.To = new Thickness(stars[i].Margin.Left, stars[i].Margin.Top + stars_to, stars[i].Margin.Right, stars[i].Margin.Bottom);
                ta_stars.FillBehavior = FillBehavior.HoldEnd;
                ta_stars.Duration = TimeSpan.FromSeconds(Math.Abs(stars_to) / 150);
                Ellipse curr_ell = stars[i];
                ta_stars.Completed += (s, _) => AnimationStarsCompleted(curr_ell);
                stars[i].BeginAnimation(Ellipse.MarginProperty, ta_stars);
            }
        }

        public static void AnimationClouds(List<Rectangle> stars)
        {
            BetterRandom rand = new BetterRandom();
            for (int i = 0; i < stars.Count; i++)
            {
                ThicknessAnimation ta_stars = new ThicknessAnimation();
                ta_stars.From = stars[i].Margin;
                double stars_to = stars[i].Margin.Top < 0 ? stars[i].Margin.Top * 2 + Application.Current.MainWindow.ActualHeight + stars[i].Width :
                    Application.Current.MainWindow.ActualHeight - stars[i].Margin.Top + stars[i].Width;
                ta_stars.To = new Thickness(stars[i].Margin.Left, stars[i].Margin.Top + stars_to, stars[i].Margin.Right, stars[i].Margin.Bottom);
                ta_stars.FillBehavior = FillBehavior.HoldEnd;
                ta_stars.Duration = TimeSpan.FromSeconds(rand.Between(20,30));
                Rectangle curr_ell = stars[i];
                ta_stars.Completed += (s, _) => AnimationCloudCompleted(curr_ell);
                stars[i].BeginAnimation(Ellipse.MarginProperty, ta_stars);
            }
        }

        public static void AnimationRemoveClouds(Clouds cloud)
        {
            DoubleAnimation da_piece = new DoubleAnimation();
            da_piece.From = 1; da_piece.To = 0;
            da_piece.Duration = TimeSpan.FromSeconds(1);
            da_piece.FillBehavior = FillBehavior.HoldEnd;
            foreach (Rectangle item in cloud.AllStars)
            {
                item.BeginAnimation(Rectangle.OpacityProperty, da_piece);
            }
        }

        public static void AnimationObstacles(Obstacle obst)
        {
            ThicknessAnimation ta_obst = new ThicknessAnimation();
            ta_obst.From = obst.ObstToCanvas.Margin;
            ta_obst.To = new Thickness(obst.ObstToCanvas.Margin.Left, obst.ObstToCanvas.Margin.Top +
                Math.Abs(obst.ObstToCanvas.Margin.Top) + Application.Current.MainWindow.ActualHeight + obst.ObstToCanvas.Width
                , obst.ObstToCanvas.Margin.Right, obst.ObstToCanvas.Margin.Bottom);
            ta_obst.FillBehavior = FillBehavior.HoldEnd;
            ta_obst.Completed += (s, _) => AnimationObstaclesCompleted(obst);
            BetterRandom rand = new BetterRandom();
            ta_obst.Duration = TimeSpan.FromSeconds(rand.Between(5, 7));
            obst.ObstToCanvas.BeginAnimation(Ellipse.MarginProperty, ta_obst);
        }

        public static void AnimationBulletfire(Rectangle fire, Rectangle ShipRectangle, double coord_y)
        {
            ThicknessAnimation ta_fire = new ThicknessAnimation();
            ta_fire.From = fire.Margin;
            ta_fire.To = new Thickness(
                fire.Margin.Left,
                fire.Margin.Top - coord_y * (Application.Current.MainWindow as MainWindow).MainCanvas.ActualHeight,
                fire.Margin.Right,
                fire.Margin.Bottom
                );
            ta_fire.Duration = TimeSpan.FromMilliseconds(1000);
            ta_fire.Completed += (s, _) => AnimationBulletfireCompleted(fire);
            fire.BeginAnimation(Rectangle.MarginProperty, ta_fire);
        }

        public static void AnimationObstFired(Obstacle obst)
        {
            BetterRandom RandForAnimaObst = new BetterRandom();
            //List<Rectangle> ObstPieces = new List<Rectangle>();
            int count_of_pieces = RandForAnimaObst.Between(3,5);
            for (int i = 0; i < count_of_pieces; i++)
            {
                // piece init
                Rectangle piece = new Rectangle();
                VisualBrush vb_for_background = new VisualBrush();
                vb_for_background.Stretch = Stretch.Fill;
                int image_num = RandForAnimaObst.Between(1, 4);
                vb_for_background.Visual = (Visual)Application.Current.Resources["bact_" + image_num];
                piece.Fill = vb_for_background;
                piece.Width = RandForAnimaObst.Between((int)(obst.ObstToCanvas.Width * 0.3), (int)(obst.ObstToCanvas.Width * 0.4));
                piece.Name = "piece";
                piece.Height = RandForAnimaObst.Between((int)(obst.ObstToCanvas.Height * 0.3), (int)(obst.ObstToCanvas.Height * 0.4));
                VisualBrush vb = new VisualBrush();
                vb.Visual = (Visual)Application.Current.Resources["bact_" + image_num];
                piece.OpacityMask = vb;
                piece.Margin = new Thickness(
                    obst.ObstToCanvas.Margin.Left + obst.ObstToCanvas.Width / 2,
                    obst.ObstToCanvas.Margin.Top + obst.ObstToCanvas.Height / 2,
                    0,0
                    );
                //ObstPieces.Add(piece);
                (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Add(piece);

                // piece animation
                DoubleAnimation da_piece = new DoubleAnimation();
                da_piece.From = 1; da_piece.To = 0;
                da_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2,4));
                da_piece.FillBehavior = FillBehavior.HoldEnd;
                da_piece.Completed += (s, _) => AnimationObstFiredCompleted(piece);

                ThicknessAnimation ta_piece = new ThicknessAnimation();
                ta_piece.From = piece.Margin;
                ta_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 4));
                double coord_x = Math.Cos(Math.PI / 180 * ( RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1) ) + 90));
                double coord_y = Math.Cos(Math.PI / 180 * (RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1))));
                ta_piece.To = new Thickness(
                    piece.Margin.Left - (coord_x * RandForAnimaObst.Between(50,100)),
                    piece.Margin.Top - (coord_y * RandForAnimaObst.Between(50,100)),
                    0,0
                    );

                piece.BeginAnimation(Rectangle.OpacityProperty, da_piece);
                piece.BeginAnimation(Rectangle.MarginProperty, ta_piece);
            }
        }

        public static void AnimationShipDamage(StarShip ship)
        {
            BetterRandom RandForAnimaObst = new BetterRandom();
            //List<Rectangle> ObstPieces = new List<Rectangle>();
            int count_of_pieces = RandForAnimaObst.Between(1, 2);
            for (int i = 0; i < count_of_pieces; i++)
            {
                // piece init
                Ellipse piece = new Ellipse();
                piece.Fill = RandForAnimaObst.Between(1, 2) == 1 ? Brushes.Red : Brushes.Yellow;
                piece.Width = ship.shipRectangle.Width * 0.05;
                piece.Name = i == count_of_pieces - 1 ? "last_damage" : "damage";
                piece.Height = ship.shipRectangle.Height * 0.04;
                
                piece.Margin = new Thickness(
                    ship.shipRectangle.Margin.Left + ship.shipRectangle.Width / 2,
                    ship.shipRectangle.Margin.Top + ship.shipRectangle.Height / 2,
                    0, 0
                    );
                //ObstPieces.Add(piece);
                (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Add(piece);

                // piece animation
                DoubleAnimation da_piece = new DoubleAnimation();
                da_piece.From = 1; da_piece.To = 0;
                da_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 4));
                da_piece.FillBehavior = FillBehavior.HoldEnd;
                da_piece.Completed += (s, _) => AnimationShipDamageCompleted(piece);

                ThicknessAnimation ta_piece = new ThicknessAnimation();
                ta_piece.From = piece.Margin;
                ta_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 4));
                double coord_x = Math.Cos(Math.PI / 180 * (RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1)) + 90));
                double coord_y = Math.Cos(Math.PI / 180 * (RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1))));
                ta_piece.To = new Thickness(
                    piece.Margin.Left - (coord_x * RandForAnimaObst.Between(50, 100)),
                    piece.Margin.Top - (coord_y * RandForAnimaObst.Between(50, 100)),
                    0, 0
                    );

                piece.BeginAnimation(Rectangle.OpacityProperty, da_piece);
                piece.BeginAnimation(Rectangle.MarginProperty, ta_piece);
            }
        }

        public static void AnimationShipGameOver(StarShip ship)
        {
            BetterRandom RandForAnimaObst = new BetterRandom();
            int count_of_pieces = RandForAnimaObst.Between(30, 40);
            int count_of_repeat = RandForAnimaObst.Between(5, 10);
            for (int j = 0; j < count_of_repeat; j++)
            {
                for (int i = 0; i < count_of_pieces; i++)
                {
                    // piece init
                    Ellipse piece = new Ellipse();
                    piece.Fill = RandForAnimaObst.Between(1, 2) == 1 ? Brushes.Red : Brushes.Yellow;
                    piece.Width = RandForAnimaObst.Between((int)(ship.shipRectangle.Width * 0.1), (int)(ship.shipRectangle.Width * 0.4));
                    piece.Name = "gameover";
                    piece.Height = RandForAnimaObst.Between((int)(ship.shipRectangle.Height * 0.1), (int)(ship.shipRectangle.Height * 0.4));

                    piece.Margin = new Thickness(
                        ship.shipRectangle.Margin.Left + ship.shipRectangle.Width / 2,
                        ship.shipRectangle.Margin.Top + ship.shipRectangle.Height / 2,
                        0, 0
                        );
                    (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Add(piece);

                    // piece animation
                    DoubleAnimation da_piece = new DoubleAnimation();
                    da_piece.From = 1; da_piece.To = 0;
                    da_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 4));
                    da_piece.FillBehavior = FillBehavior.HoldEnd;
                    da_piece.Completed += (s, _) => AnimationShipDamageCompleted(piece);

                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = piece.Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 4));
                    double coord_x = Math.Cos(Math.PI / 180 * (RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1)) + 90));
                    double coord_y = Math.Cos(Math.PI / 180 * (RandForAnimaObst.Between((360 / count_of_pieces) * i, (360 / count_of_pieces) * (i + 1))));
                    ta_piece.To = new Thickness(
                        piece.Margin.Left - (coord_x * RandForAnimaObst.Between(50, 100)),
                        piece.Margin.Top - (coord_y * RandForAnimaObst.Between(50, 100)),
                        0, 0
                        );

                    piece.BeginAnimation(Rectangle.OpacityProperty, da_piece);
                    piece.BeginAnimation(Rectangle.MarginProperty, ta_piece);
                }
            }
        }
        
        public static void AnimationGoToSpace()
        {
            SolidColorBrush rootElementBrush = (Application.Current.MainWindow as MainWindow).Resources["CanvasBrush"] as SolidColorBrush;

            ColorAnimation da_c = new ColorAnimation();
            da_c.To = Colors.Black;
            da_c.Duration = TimeSpan.FromSeconds(5);
            rootElementBrush.BeginAnimation(SolidColorBrush.ColorProperty, da_c);
        }

        public static void AnimationGameOver()
        {
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = new Thickness(
                                    0,
                - (((Application.Current.MainWindow as MainWindow).Resources["GameOverPanel"] as StackPanel).Height + 10),
                0,0
                );
            ta.To = new Thickness(0,
                (Application.Current.MainWindow as MainWindow).MainCanvas.ActualHeight / 2
                - (((Application.Current.MainWindow as MainWindow).Resources["GameOverPanel"] as StackPanel).Height / 2),
                0, 0
                );
            ta.FillBehavior = FillBehavior.HoldEnd;
            ta.Duration = TimeSpan.FromSeconds(5);
            (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Add(
                (Application.Current.MainWindow as MainWindow).Resources["GameOverPanel"] as StackPanel
                );
            ((Application.Current.MainWindow as MainWindow).Resources["GameOverPanel"] as StackPanel).BeginAnimation(Ellipse.MarginProperty, ta);
        }

        #region boss

        public static async void AnimationBossInit(Boss _boss, MainWindow mainWindow)
        {
            ThicknessAnimation ta_piece = new ThicknessAnimation();
            _boss.BossRectangle.Margin = new Thickness(mainWindow.MainCanvas.ActualWidth + 10,
                0,
                0, 0);
            ta_piece.From = _boss.BossRectangle.Margin;
            ta_piece.Duration = TimeSpan.FromSeconds(3);
            ta_piece.To = new Thickness(
                _boss.BossRectangle.Margin.Left - _boss.BossRectangle.Width / 2,
                _boss.BossRectangle.Margin.Top,
                0, 0
                );
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece);
            await Task.Run(() => System.Threading.Thread.Sleep(3010));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
            await Task.Run(() => System.Threading.Thread.Sleep(1000));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular"];

            ThicknessAnimation ta_piece1 = new ThicknessAnimation();
            ta_piece1.From = _boss.BossRectangle.Margin;
            ta_piece1.Duration = TimeSpan.FromSeconds(3);
            ta_piece1.To = new Thickness(
                _boss.BossRectangle.Margin.Left + _boss.BossRectangle.Width / 2,
                _boss.BossRectangle.Margin.Top,
                0, 0
                );
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece1);
            await Task.Run(() => System.Threading.Thread.Sleep(3010));

            ThicknessAnimation ta_piece2 = new ThicknessAnimation();
            Thickness from_t = new Thickness(mainWindow.MainCanvas.ActualWidth  - _boss.BossRectangle.Width - 10,
                mainWindow.MainCanvas.ActualHeight - 10,
                0, 0);
            ta_piece2.From = from_t;
            ta_piece2.Duration = TimeSpan.FromSeconds(3);
            ta_piece2.To = new Thickness(
                from_t.Left,
                from_t.Top - _boss.BossRectangle.Height / 2,
                0, 0
                );
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece2);
            await Task.Run(() => System.Threading.Thread.Sleep(3010));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
            await Task.Run(() => System.Threading.Thread.Sleep(1000));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular"];

            ThicknessAnimation ta_piece3 = new ThicknessAnimation();
            
            ta_piece3.From = _boss.BossRectangle.Margin;
            ta_piece3.Duration = TimeSpan.FromSeconds(3);
            ta_piece3.To = new Thickness(
                from_t.Left,
                from_t.Top + _boss.BossRectangle.Height / 2,
                0, 0
                );
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece3);



            ThicknessAnimation ta_piece4 = new ThicknessAnimation();
            Thickness from_t4 = new Thickness(mainWindow.MainCanvas.ActualWidth / 2 - _boss.BossRectangle.Width / 2,
                -_boss.BossRectangle.Height,
                0, 0);
            ta_piece4.From = from_t4;
            ta_piece4.Duration = TimeSpan.FromSeconds(3);
            ta_piece4.To = new Thickness(
                from_t4.Left,
                from_t4.Top + _boss.BossRectangle.Height + 20,
                0, 0
                );
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece4);
            await Task.Run(() => System.Threading.Thread.Sleep(3010));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_mouth_open_angry"];
            await Task.Run(() => System.Threading.Thread.Sleep(2000));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];

        }

        public static async void AnimationBossFire(Boss _boss, MainWindow mainWindow)
        {
            BetterRandom betterRandom = new BetterRandom();
            int attack_type = betterRandom.Between(1, 2);
            //int attack_type = 2;
            if (attack_type == 1)
            {
                for (int i = 0; i < _boss.CurrentBossAmmos.Count; i++)
                {
                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = _boss.CurrentBossAmmos[i].Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(2);
                    ta_piece.To = new Thickness(
                        _boss.CurrentBossAmmos[i].Margin.Left,
                        mainWindow.ActualHeight + _boss.CurrentBossAmmos[i].Height,
                        0, 0
                        );
                    if (i == _boss.CurrentBossAmmos.Count - 1)
                    {
                        _boss.CurrentBossAmmos[i].Name = "finish";
                        (Application.Current.MainWindow as MainWindow).boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
                    }

                    Ellipse ell = _boss.CurrentBossAmmos[i];
                    ta_piece.Completed += (s, _) => AnimationFireBossCompleted(ell);
                    _boss.CurrentBossAmmos[i].BeginAnimation(Rectangle.MarginProperty, ta_piece);
                    await Task.Run(() => System.Threading.Thread.Sleep(100));
                }
            }
            else if (attack_type == 2)
            {
                double to_left = 0;
                int plusOrMinus = 0;
                double to_top = (Application.Current.MainWindow as MainWindow).ActualHeight;
                for (int i = 0; i < _boss.CurrentBossAmmos.Count; i++)
                {
                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = _boss.CurrentBossAmmos[i].Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(2);
                    ta_piece.To = new Thickness(
                        to_left,
                        to_top + _boss.CurrentBossAmmos[i].Height,
                        0, 0
                        );
                    if (i == _boss.CurrentBossAmmos.Count - 1)
                    {
                        _boss.CurrentBossAmmos[i].Name = "finish";
                        (Application.Current.MainWindow as MainWindow).boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
                    }

                    Ellipse ell = _boss.CurrentBossAmmos[i];
                    ta_piece.Completed += (s, _) => AnimationFireBossCompleted(ell);
                    _boss.CurrentBossAmmos[i].BeginAnimation(Rectangle.MarginProperty, ta_piece);
                    await Task.Run(() => System.Threading.Thread.Sleep(70));
                    if (plusOrMinus == 0)
                    {
                        if (to_left >= (Application.Current.MainWindow as MainWindow).ActualWidth)
                        {
                            plusOrMinus = 2;
                        }
                        to_left += 20;
                    }
                    else
                    {
                        if (to_left <= 0)
                        {
                            plusOrMinus = 0;
                        }
                        to_left -= 20;
                    }
                }
            }
        }

        private static void AnimationFireBossCompleted(UIElement element)
        {
            if ((element as Ellipse).Name == "finish")
            {
                (Application.Current.MainWindow as MainWindow).boss.CurrentBossAmmos.Clear();
            }
        }

        #endregion

        // complete events
        private static void AnimationBonusCompleted(UIElement element)
        {
            (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
        }

        private static void AnimationObstFiredCompleted(UIElement element)
        {
            (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
        }

        private static void AnimationShipDamageCompleted(UIElement element)
        {
            (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
        }

        private static void AnimationBulletfireCompleted(UIElement element)
        {
            (Application.Current.MainWindow as MainWindow).ship.CurrentAmmos.Remove(element as Rectangle);
            (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
        }

        private static void AnimationCloudCompleted(UIElement element)
        {
            try
            {
                (Application.Current.MainWindow as MainWindow).clouds.AllStars.Remove(element as Rectangle);
                (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
            }
            catch { }
        }

        private static void AnimationStarsCompleted(UIElement element)
        {
            try
            {
                (Application.Current.MainWindow as MainWindow).stars.AllStars.Remove(element as Ellipse);
                (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element);
            }
            catch { }
        }

        private static void AnimationObstaclesCompleted(Obstacle element)
        {
            (Application.Current.MainWindow as MainWindow).CurrentObsts.Remove(element);
            (Application.Current.MainWindow as MainWindow).RemoveElementAfterAnimation(element.ObstToCanvas);
        }
    }
}
