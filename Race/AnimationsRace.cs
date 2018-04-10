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
        //easter egg animation

        public static async void AnimationMainScreen(StarShip starShip_title, MainWindow mainWindow)
        {
            int anim_duration = 500;
            try
            {
                ThicknessAnimation ta_bonus = new ThicknessAnimation(); // up
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left,
                    starShip_title.shipRectangle.Margin.Top - 20,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));

                ta_bonus = new ThicknessAnimation();
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left,
                    starShip_title.shipRectangle.Margin.Top + 20,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration * 2));

                // down x3
                for (int i = 0; i < 3; i++)
                {
                    ta_bonus = new ThicknessAnimation();
                    ta_bonus.From = starShip_title.shipRectangle.Margin;
                    ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left,
                        starShip_title.shipRectangle.Margin.Top + 20,
                        0, 0);
                    ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                    starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                    await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));
                    ta_bonus = new ThicknessAnimation();
                    ta_bonus.From = starShip_title.shipRectangle.Margin;
                    ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left,
                        starShip_title.shipRectangle.Margin.Top - 20,
                        0, 0);
                    ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                    starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                    await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));
                }
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));

                //left
                ta_bonus = new ThicknessAnimation();
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left - 20,
                    starShip_title.shipRectangle.Margin.Top,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));

                ta_bonus = new ThicknessAnimation();
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left + 20,
                    starShip_title.shipRectangle.Margin.Top,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration * 2));

                //right
                ta_bonus = new ThicknessAnimation();
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left + 20,
                    starShip_title.shipRectangle.Margin.Top,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration));

                ta_bonus = new ThicknessAnimation();
                ta_bonus.From = starShip_title.shipRectangle.Margin;
                ta_bonus.To = new Thickness(starShip_title.shipRectangle.Margin.Left - 20,
                    starShip_title.shipRectangle.Margin.Top,
                    0, 0);
                ta_bonus.Duration = TimeSpan.FromMilliseconds(anim_duration);
                starShip_title.shipRectangle.BeginAnimation(Ellipse.MarginProperty, ta_bonus);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(anim_duration * 2));
            }
            catch { }
        }

        public static void AnimationEasterEgg(MainWindow mainWindow)
        {
            BetterRandom betterRandom = new BetterRandom();
            DoubleAnimation da_piece = new DoubleAnimation();
            da_piece.From = 1; da_piece.To = 0;
            da_piece.Duration = TimeSpan.FromSeconds(1);
            da_piece.FillBehavior = FillBehavior.HoldEnd;
            Image image = new Image();
            image.Source = (BitmapImage)Application.Current.Resources["ee_" + betterRandom.Between(1, 5)];
            image.Height = mainWindow.MainCanvas.ActualHeight;

            double coef = (image.Source as BitmapImage).Height / image.Height;

            image.Margin = new Thickness(mainWindow.MainCanvas.ActualWidth / 2 - ((image.Source as BitmapImage).Width / coef) / 2,
                0,
                0,0);
            mainWindow.MainCanvas.Children.Add(image);
            da_piece.Completed += (s, _) => AnimationEasterEggCompleted(image);
            image.BeginAnimation(Rectangle.OpacityProperty, da_piece);
        }

        public static void AnimationEasterEggCompleted(UIElement sender)
        {
            (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Remove(sender);
            for (int i = 0; i < (Application.Current.MainWindow as MainWindow).arrow_arr.Length; i++)
            {
                (Application.Current.MainWindow as MainWindow).arrow_arr[i] = 0;
            }
            (Application.Current.MainWindow as MainWindow).easter_egg_find = false;
        }

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

        public async static void AnimationBulletfire(Rectangle fire, Rectangle ShipRectangle, double coord_y)
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
        public static async void AnimationWalkBossRectangle(Boss _boss, MainWindow mainWindow)
        {
            _boss.NowMove = true;
            BetterRandom betterRandom = new BetterRandom();
            int repeat_times = 1;
            int speed_time = betterRandom.Between(2, 4);
            ThicknessAnimation ta_piece = new ThicknessAnimation();
            ta_piece.From = _boss.BossRectangle.Margin;
            ta_piece.Duration = TimeSpan.FromSeconds(1);
            ta_piece.FillBehavior = FillBehavior.HoldEnd;
            Thickness thickness_to = new Thickness(
                20,
                20,
                0, 0
                );
            ta_piece.To = thickness_to;
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece);
            await Task.Run(() => System.Threading.Thread.Sleep(1010));

            for (int i = 0; i < repeat_times; i++)
            {
                ThicknessAnimation from_left_to_right = new ThicknessAnimation();
                from_left_to_right.From = _boss.BossRectangle.Margin;
                from_left_to_right.Duration = TimeSpan.FromSeconds(2);
                from_left_to_right.FillBehavior = FillBehavior.HoldEnd;
                from_left_to_right.To = new Thickness(mainWindow.MainCanvas.ActualWidth - _boss.BossRectangle.Width - 20,
                    _boss.BossRectangle.Margin.Top
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_left_to_right);
                await Task.Run(() => System.Threading.Thread.Sleep(2010));

                ThicknessAnimation from_up_to_down = new ThicknessAnimation();
                from_up_to_down.From = _boss.BossRectangle.Margin;
                from_up_to_down.Duration = TimeSpan.FromSeconds(1);
                from_up_to_down.FillBehavior = FillBehavior.HoldEnd;
                from_up_to_down.To = new Thickness(_boss.BossRectangle.Margin.Left,
                    mainWindow.MainCanvas.ActualHeight / 2 - _boss.BossRectangle.Height / 2
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_up_to_down);
                await Task.Run(() => System.Threading.Thread.Sleep(1010));

                ThicknessAnimation from_right_to_left = new ThicknessAnimation();
                from_right_to_left.From = _boss.BossRectangle.Margin;
                from_right_to_left.Duration = TimeSpan.FromSeconds(2);
                from_right_to_left.FillBehavior = FillBehavior.HoldEnd;
                from_right_to_left.To = new Thickness(20,
                    _boss.BossRectangle.Margin.Top
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_right_to_left);
                await Task.Run(() => System.Threading.Thread.Sleep(2010));

                ThicknessAnimation from_down_to_up = new ThicknessAnimation();
                from_down_to_up.From = _boss.BossRectangle.Margin;
                from_down_to_up.Duration = TimeSpan.FromSeconds(1);
                from_down_to_up.FillBehavior = FillBehavior.HoldEnd;
                from_down_to_up.To = new Thickness(20,
                    20
                    , 0, 0);
                from_down_to_up.Completed += (s, _) => AnimationMoveBossCompleted(_boss, null);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_down_to_up);
            }
        }
        public static async void AnimationWalkBossCircle(Boss _boss, MainWindow mainWindow)
        {
            _boss.NowMove = true;
            BetterRandom betterRandom = new BetterRandom();
            int repeat_times = 1;
            int speed_time = betterRandom.Between(2, 4);
            ThicknessAnimation ta_piece = new ThicknessAnimation();
            ta_piece.From = _boss.BossRectangle.Margin;
            ta_piece.Duration = TimeSpan.FromSeconds(1);
            ta_piece.FillBehavior = FillBehavior.HoldEnd;
            Thickness thickness_to = new Thickness(
                20,
                mainWindow.MainCanvas.ActualHeight / 2 - _boss.BossRectangle.Height / 2,
                0, 0
                );
            ta_piece.To = thickness_to;
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece);
            await Task.Run(() => System.Threading.Thread.Sleep(1010));

            for (int i = 0; i < repeat_times; i++)
            {
                ThicknessAnimation from_left_to_right = new ThicknessAnimation();
                from_left_to_right.From = _boss.BossRectangle.Margin;
                from_left_to_right.Duration = TimeSpan.FromSeconds(2);
                from_left_to_right.FillBehavior = FillBehavior.HoldEnd;
                from_left_to_right.To = new Thickness(mainWindow.MainCanvas.ActualWidth / 2 - _boss.BossRectangle.Width / 2 - 20,
                    20
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_left_to_right);
                await Task.Run(() => System.Threading.Thread.Sleep(2010));

                ThicknessAnimation from_up_to_down = new ThicknessAnimation();
                from_up_to_down.From = _boss.BossRectangle.Margin;
                from_up_to_down.Duration = TimeSpan.FromSeconds(1);
                from_up_to_down.FillBehavior = FillBehavior.HoldEnd;
                from_up_to_down.To = new Thickness(mainWindow.MainCanvas.ActualWidth - _boss.BossRectangle.Width - 20,
                    mainWindow.MainCanvas.ActualHeight / 2 - _boss.BossRectangle.Height / 2
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_up_to_down);
                await Task.Run(() => System.Threading.Thread.Sleep(1010));

                ThicknessAnimation from_right_to_left = new ThicknessAnimation();
                from_right_to_left.From = _boss.BossRectangle.Margin;
                from_right_to_left.Duration = TimeSpan.FromSeconds(2);
                from_right_to_left.FillBehavior = FillBehavior.HoldEnd;
                from_right_to_left.To = new Thickness(mainWindow.MainCanvas.ActualWidth / 2 - _boss.BossRectangle.Width / 2,
                    mainWindow.MainCanvas.ActualHeight / 2
                    , 0, 0);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_right_to_left);
                await Task.Run(() => System.Threading.Thread.Sleep(2010));

                ThicknessAnimation from_down_to_up = new ThicknessAnimation();
                from_down_to_up.From = _boss.BossRectangle.Margin;
                from_down_to_up.Duration = TimeSpan.FromSeconds(1);
                from_down_to_up.FillBehavior = FillBehavior.HoldEnd;
                from_down_to_up.To = new Thickness(_boss.BossRectangle.Margin.Left,
                    20
                    , 0, 0);
                from_down_to_up.Completed += (s, _) => AnimationMoveBossCompleted(_boss, null);
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, from_down_to_up);
            }
        }

        public static async void AnimationBossInit(Boss _boss, MainWindow mainWindow)
        {
            _boss.NowMove = true;
            _boss.NowInit = 1;
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
            ta_piece2.FillBehavior = FillBehavior.HoldEnd;
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
            await Task.Run(() => System.Threading.Thread.Sleep(3010));


            ThicknessAnimation ta_piece4 = new ThicknessAnimation();
            Thickness from_t4 = new Thickness(mainWindow.MainCanvas.ActualWidth / 2 - _boss.BossRectangle.Width / 2,
                -_boss.BossRectangle.Height,
                0, 0);
            ta_piece4.From = from_t4;
            ta_piece4.Duration = TimeSpan.FromSeconds(3);
            Thickness thickness_to = new Thickness(
                from_t4.Left,
                from_t4.Top + _boss.BossRectangle.Height + 20,
                0, 0
                );
            ta_piece4.To = thickness_to;
            ta_piece4.Completed += (s, _) => AnimationMoveBossCompleted(_boss, null);
           // ta_piece4.FillBehavior = FillBehavior.HoldEnd;
            _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece4);
            await Task.Run(() => System.Threading.Thread.Sleep(3010));
            BetterRandom betterRandom = new BetterRandom();
            Sounds.PlaySoundOnce("boss_scream_"+ betterRandom.Between(1,2) +".wav");
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_mouth_open_angry"];
            await Task.Run(() => System.Threading.Thread.Sleep(2000));
            _boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];

        }

        public static async void AnimationBossFire(Boss _boss, MainWindow mainWindow, int count_of_ammos)
        {
            _boss.NowFire = true;
            BetterRandom betterRandom = new BetterRandom();
            int attack_type = betterRandom.Between(1, 2);
            if (_boss.StopFire) return;
            //int attack_type = 2;
            if (attack_type == 1)
            {
                for (int i = 0; i < count_of_ammos; i++)
                {
                    if (_boss == null) return;
                    Ellipse fire = new Ellipse();
                    fire.Width =  _boss.size_of_ammo;
                    fire.Height = _boss.size_of_ammo;
                    fire.Fill = Brushes.Red;
                    fire.Name = "boss_fire";
                    _boss.CurrentBossAmmos.Add(fire);
                    fire.Margin = new Thickness(_boss.BossRectangle.Margin.Left + _boss.BossRectangle.Width / 2 - 10,
                        _boss.BossRectangle.Margin.Top + _boss.BossRectangle.Height * 0.7,
                        0, 0);
                    mainWindow.MainCanvas.Children.Add(fire);

                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = fire.Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(2);
                    ta_piece.To = new Thickness(
                        fire.Margin.Left,
                        mainWindow.ActualHeight + fire.Height,
                        0, 0
                        );
                    if (i == count_of_ammos - 1)
                    {
                        try
                        {
                            fire.Name = "finish";
                            (Application.Current.MainWindow as MainWindow).boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
                        }
                        catch { }
                    }

                    Ellipse ell = fire;
                    ta_piece.Completed += (s, _) => AnimationFireBossCompleted(ell);
                    fire.BeginAnimation(Rectangle.MarginProperty, ta_piece);
                    await Task.Run(() => System.Threading.Thread.Sleep(80));
                }
            }
            else if (attack_type == 2)
            {
                double to_left = 0;
                int plusOrMinus = 0;
                double to_top = (Application.Current.MainWindow as MainWindow).ActualHeight;
                for (int i = 0; i < count_of_ammos; i++)
                {
                    if (_boss == null) return;
                    Ellipse fire = new Ellipse();
                    fire.Width = _boss.size_of_ammo;
                    fire.Height = _boss.size_of_ammo;
                    fire.Fill = Brushes.Red;
                    _boss.CurrentBossAmmos.Add(fire);
                    fire.Margin = new Thickness(_boss.BossRectangle.Margin.Left + _boss.BossRectangle.Width / 2 - 10,
                        _boss.BossRectangle.Margin.Top + _boss.BossRectangle.Height * 0.7,
                        0, 0);
                    mainWindow.MainCanvas.Children.Add(fire);

                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = fire.Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(2);
                    ta_piece.To = new Thickness(
                        to_left,
                        to_top + fire.Height,
                        0, 0
                        );
                    if (i == count_of_ammos - 1)
                    {
                        try
                        {
                            fire.Name = "finish";
                            (Application.Current.MainWindow as MainWindow).boss.BossSprite.Visual = (Visual)Application.Current.Resources["boss_regular_angry"];
                        }
                        catch { }
                    }

                    Ellipse ell = fire;
                    ta_piece.Completed += (s, _) => AnimationFireBossCompleted(ell);
                    fire.BeginAnimation(Rectangle.MarginProperty, ta_piece);
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

        public static async void AnimationBossNoMore(Boss _boss)
        {
            BetterRandom betterRandom = new BetterRandom();
            for (int i = 0; i < 20; i++)
            {
                ThicknessAnimation ta_piece = new ThicknessAnimation();
                ta_piece.From = _boss.BossRectangle.Margin;
                ta_piece.Duration = TimeSpan.FromMilliseconds(100);
                int x_margin = betterRandom.Between(-1, 1);
                int y_margin = betterRandom.Between(-1, 1);
                ta_piece.To = new Thickness(
                    _boss.BossRectangle.Margin.Left + x_margin * 10,
                    _boss.BossRectangle.Margin.Top + y_margin * 10,
                    0, 0
                    );
                _boss.BossRectangle.BeginAnimation(Rectangle.MarginProperty, ta_piece);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(110));
            }

            for (int i = (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Count - 1; i >= 0; i--)
            {
                if ((Application.Current.MainWindow as MainWindow).MainCanvas.Children[i] is Rectangle
                    && ((Application.Current.MainWindow as MainWindow).MainCanvas.Children[i] as Rectangle).Name == "Boss")
                {
                    (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Remove((Application.Current.MainWindow as MainWindow).MainCanvas.Children[i]);
                    break;
                }
            }

            BetterRandom RandForAnimaObst = new BetterRandom();
            int count_of_pieces = RandForAnimaObst.Between(5,5);
            int count_of_repeat = RandForAnimaObst.Between(3,3);
            for (int j = 0; j < count_of_repeat; j++)
            {
                for (int i = 0; i < count_of_pieces; i++)
                {
                    // piece init
                    Ellipse piece = new Ellipse();
                    piece.Fill = RandForAnimaObst.Between(1, 2) == 1 ? Brushes.Red : Brushes.Yellow;
                    piece.Width = RandForAnimaObst.Between((int)(_boss.bossRectangle.Width * 0.1), (int)(_boss.bossRectangle.Width * 0.4));
                    piece.Name = "gameover";
                    piece.Height = RandForAnimaObst.Between((int)(_boss.bossRectangle.Height * 0.1), (int)(_boss.bossRectangle.Height * 0.4));

                    piece.Margin = new Thickness(
                        _boss.bossRectangle.Margin.Left + _boss.bossRectangle.Width / 2,
                        _boss.bossRectangle.Margin.Top + _boss.bossRectangle.Height / 2,
                        0, 0
                        );
                    (Application.Current.MainWindow as MainWindow).MainCanvas.Children.Add(piece);

                    // piece animation
                    DoubleAnimation da_piece = new DoubleAnimation();
                    da_piece.From = 1; da_piece.To = 0;
                    da_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 2));
                    da_piece.FillBehavior = FillBehavior.HoldEnd;
                    if(i == count_of_pieces - 1)
                        piece.Name = "win_a";
                    da_piece.Completed += (s, _) => AnimationShipDamageCompleted(piece);

                    ThicknessAnimation ta_piece = new ThicknessAnimation();
                    ta_piece.From = piece.Margin;
                    ta_piece.Duration = TimeSpan.FromSeconds(RandForAnimaObst.Between(2, 2));
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


        private static void AnimationMoveBossFillMarginCompleted(Thickness thickness)
        {
            //(Application.Current.MainWindow as MainWindow).boss.BossRectangle.Margin = thickness;
        }

        private static void AnimationMoveBossCompleted(Boss element, TranslateTransform translateTransform)
        {
            element.NowMove = false;
            element.NowInit = 2;
        }

        private static void AnimationFireBossCompleted(UIElement element)
        {
            try
            {
                if ((element as Ellipse).Name == "finish")
                {
                    (Application.Current.MainWindow as MainWindow).boss.NowFire = false;
                    (Application.Current.MainWindow as MainWindow).boss.CurrentBossAmmos.Clear();
                }
            }
            catch
            {
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
           // if((element as Ellipse).Name == "win_a")
           // {
                //(Application.Current.MainWindow as MainWindow).SetWinImages();
            //}
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
