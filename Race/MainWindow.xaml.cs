using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.IO;
using WpfAnimatedGif;

namespace Race
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer EasterEggTimer;
        public int[] arrow_arr = new int[6];
        int curr_arr_i = 0;
        int count_of_press = 0;
        public bool easter_egg_find = false;
        public bool show_ee_tip = true;

        DispatcherTimer ObstsGeneratorTimer;
        DispatcherTimer CollisionTimer;
        DispatcherTimer KeyTrackTimer;
        DispatcherTimer BonusGeneratorTimer;

        DispatcherTimer StoryBoardTimer;

        BetterRandom RandForSomethings;

        int obst_cout_increasing_for_up_difficulty = 5; // 5 - start difficulty

        public int Score;
        int prev_ship_score = 0;

        public StarShip ship;
        public Clouds clouds;
        public Stars stars;

        public Boss boss;

        public List<Obstacle> CurrentObsts = null;
        public List<Bonus> CurrentBonuses = null;

        bool leftpress = false;
        bool rightpress = false;
        bool uppress = false;
        bool downpress = false;
        bool spacepress = false;
        bool ser_hurt = false;


        public int game_time_sec = 0;

        public string selected_hat_name = "";
        public Image selected_hat_image;
        public BitmapImage selected_hat_bitmap_image;

        ProgressBar progressBarHealthBoss;

        //story board flags
        bool gotospace = false;
        bool bossfight = false;

        bool pause = false;
        BackgroundWorker _pause;
        Label labelPause;

        public MainWindow()
        {
            InitializeComponent();

            CurrentObsts = new List<Obstacle>();
            CurrentBonuses = new List<Bonus>();
            RandForSomethings = new BetterRandom();

            BonusGeneratorTimer = new DispatcherTimer();
            BonusGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            BonusGeneratorTimer.Tick += new EventHandler(BonusGeneratorTimerTick);

            ObstsGeneratorTimer = new DispatcherTimer();
            ObstsGeneratorTimer.Interval = TimeSpan.FromMilliseconds(3500);
            ObstsGeneratorTimer.Tick += new EventHandler(ObstsGeneratorTimerTick);

            KeyTrackTimer = new DispatcherTimer();
            KeyTrackTimer.Interval = TimeSpan.FromMilliseconds(8);
            KeyTrackTimer.Tick += new EventHandler(KeyTrackTimerTimerTick);

            CollisionTimer = new DispatcherTimer();
            CollisionTimer.Interval = TimeSpan.FromMilliseconds(8);
            CollisionTimer.Tick += new EventHandler(CollisionTimerTimerTick);

            StoryBoardTimer = new DispatcherTimer();
            StoryBoardTimer.Interval = TimeSpan.FromMilliseconds(1000);
            StoryBoardTimer.Tick += new EventHandler(StoryBoardTimerTimerTick);


            EasterEggTimer = new DispatcherTimer();
            EasterEggTimer.Interval = TimeSpan.FromMilliseconds(8);
            EasterEggTimer.Tick += new EventHandler(EasterEggTimerTimerTick);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConsoleMethod.WriteToConsole("Game window loaded", Brushes.White);
            Sounds.PlayBackGround();
            clouds = new Clouds(this);


            StarShip starShip_title = new StarShip(null);
            starShip_title.shipRectangle.Margin = new Thickness(MainCanvas.ActualWidth / 2 - starShip_title.shipRectangle.Width / 2,
                MainCanvas.ActualHeight - 200,
                0,0);
            starShip_title.shipRectangle.Name = "ee_tip";
            MainCanvas.Children.Add(starShip_title.shipRectangle);
            while (show_ee_tip)
            {
                AnimationsRace.AnimationMainScreen(starShip_title, this);
                await System.Threading.Tasks.Task.Run(() => System.Threading.Thread.Sleep(23 * 500 + 3000));
            }
        }

        private void InitGame()
        {
            show_ee_tip = false;
            for (int i = MainCanvas.Children.Count - 1; i >= 0; i--)
            {
                if ((MainCanvas.Children[i] is StackPanel) ||
                    (MainCanvas.Children[i] is Rectangle && (MainCanvas.Children[i] as Rectangle).Name == "hat"))
                {
                    MainCanvas.Children.Remove(MainCanvas.Children[i]);
                }
                if(MainCanvas.Children[i] is Rectangle && (MainCanvas.Children[i] as Rectangle).Name == "Boss" ||
                    (MainCanvas.Children[i] is Rectangle && (MainCanvas.Children[i] as Rectangle).Name == "ee_tip"))
                {
                    MainCanvas.Children.Remove(MainCanvas.Children[i]);
                }
            }
            if (ship != null)
            {
                Sounds.PlayBackGround();
            }
            Sounds.GameOverSoundStop();
            this.DataContext = null;
            ship = null;
            boss = null;
            bossfight = false;
            ship = new StarShip(this);
            game_time_sec = 0;
            MainCanvas.Children.Remove(progressBarHealthBoss);
            //ship.HatSprite.Visual = (Visual)Application.Current.Resources[selected_hat_name];
            this.DataContext = ship;
            StoryBoardTimer.Start();
            BonusGeneratorTimer.Start();
            ObstsGeneratorTimer.Start();
            KeyTrackTimer.Start();
            CollisionTimer.Start();



            EasterEggTimer.Start();
            for (int i = 0; i < arrow_arr.Length; i++)
            {
                arrow_arr[i] = 0;
            }
        }

        private void Pause()
        {
            if (ship == null) return;
            if (!pause)
            {
                pause = true;
                StoryBoardTimer.Stop();
                BonusGeneratorTimer.Stop();
                ObstsGeneratorTimer.Stop();
                KeyTrackTimer.Stop();
                CollisionTimer.Stop();
                clouds.StarTimer.Stop();

                if (bossfight)
                    boss.BossActionTimer.Stop();

                labelPause = new Label();
                labelPause.Width = MainCanvas.ActualWidth;
                labelPause.Height = MainCanvas.ActualHeight;
                labelPause.VerticalContentAlignment = VerticalAlignment.Center;
                labelPause.HorizontalContentAlignment = HorizontalAlignment.Center;
                labelPause.Content = "Pause";
                labelPause.FontSize = 25;
                Panel.SetZIndex(labelPause, 99);
                labelPause.Background = gotospace ? Brushes.Black : Brushes.White;
                labelPause.Foreground = gotospace ? Brushes.White : Brushes.Black;
                MainCanvas.Children.Add(labelPause);
            }
            else
            {
                pause = false;
                MainCanvas.Children.Remove(labelPause);

                if (bossfight)
                    boss.BossActionTimer.Start();

                clouds.StarTimer.Start();
                StoryBoardTimer.Start();
                BonusGeneratorTimer.Start();
                ObstsGeneratorTimer.Start();
                KeyTrackTimer.Start();
                CollisionTimer.Start();
            }
        }
        //private void DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    while (pause)
        //    {
        //        Thread.Sleep(1000);
        //        if (_pause.CancellationPending || !pause)
        //        {
        //            e.Cancel = true;    
        //            return;
        //        }
        //        Dispatcher.BeginInvoke((Action)(() => {
        //            Thread.Sleep(1000);
        //        }));
        //    }
        //}

        private void GameOver()
        {
            Sounds.StopBackGround();
            Sounds.ShipDestroySoundPlay();
            Sounds.StopBossBackGround();
            AnimationsRace.AnimationShipGameOver(ship);
            RemoveElementAfterAnimation(ship.shipRectangle);
            BonusGeneratorTimer.Stop();
            ObstsGeneratorTimer.Stop();
            KeyTrackTimer.Stop();
            CollisionTimer.Stop();
            foreach (Obstacle item in CurrentObsts)
            {
                item.ObstacleFiredAnimation();
                RemoveElementAfterAnimation(item.ObstToCanvas);
            }
            CurrentObsts.Clear();
            Sounds.GameOverSoundPlay();
            AnimationsRace.AnimationGameOver();
        }
        private void GameWin()
        {
            Sounds.PlaySoundOnce("win_gto.wav");
            Sounds.StopBackGround();
            Sounds.StopBossBackGround();
            BonusGeneratorTimer.Stop();
            ObstsGeneratorTimer.Stop();
            KeyTrackTimer.Stop();
            CollisionTimer.Stop();
            foreach (Ellipse item in boss.CurrentBossAmmos)
            {
                MainCanvas.Children.Remove(item);
            }
            foreach (Obstacle item in CurrentObsts)
            {
                item.ObstacleFiredAnimation();
                RemoveElementAfterAnimation(item.ObstToCanvas);
            }
            CurrentObsts.Clear();
            boss.StopFire = true;
            AnimationsRace.AnimationBossNoMore(boss);
        }
        public async void SetWinImages()
        {
            List<string> gifs_for_win = new List<string>();
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "win_gifs"))
            {
                string[] hats_files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "win_gifs/", "*.gif",
                    SearchOption.TopDirectoryOnly);

                for (int i = 0; i < hats_files.Length; i++)
                {
                    gifs_for_win.Add(System.IO.Path.GetFileName(hats_files[i]));
                }
            }

            BetterRandom betterRandom = new BetterRandom();

            gifs_for_win = Shuffle<string>(gifs_for_win);

            for (int i = 0; i < gifs_for_win.Count; i++)
            {
                Image img = new Image();
                img.Width = betterRandom.Between(100, 200);
                MainCanvas.Children.Add(img);
                img.Margin = new Thickness(betterRandom.Between(50, (int)MainCanvas.ActualWidth - (int)img.Width),
                    betterRandom.Between(10, (int)MainCanvas.ActualHeight - 10),
                    0, 0);
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "win_gifs/" + gifs_for_win[i]);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(img, image);
                await Task.Run(() => Thread.Sleep(100));
            }
        }
        



        private Random rng = new Random();
        public List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        private void ShowEasterEgg()
        {
            AnimationsRace.AnimationEasterEgg(this);
        }
        private void EasterEggTimerTimerTick(object sender, EventArgs e)
        {
            if (easter_egg_find) return;
            string res = "";
            foreach (int item in arrow_arr)
            {
                res += Convert.ToString(item);
            }
            if (res == "122234")
            {
                easter_egg_find = true;
                ShowEasterEgg();
            }
        }
        private void setarrowarr(Key k)
        {
            if (curr_arr_i == arrow_arr.Length || k == Key.Up) curr_arr_i = 0;
            switch (k)
            {
                case Key.Up:
                    {
                        arrow_arr[curr_arr_i] = 1;
                        break;
                    }
                case Key.Down:
                    {
                        arrow_arr[curr_arr_i] = 2;
                        break;
                    }
                case Key.Left:
                    {
                        arrow_arr[curr_arr_i] = 3;
                        break;
                    }
                case Key.Right:
                    {
                        arrow_arr[curr_arr_i] = 4;
                        break;
                    }
            }
            curr_arr_i++;
        }

        private void StoryBoardTimerTimerTick(object sender, EventArgs e)
        {
            game_time_sec++;
            if (game_time_sec >= 60 && !gotospace)
            {
                AnimationsRace.AnimationGoToSpace();
                AnimationsRace.AnimationRemoveClouds(clouds);
                if(stars == null)
                    stars = new Stars(this);
                clouds.StarTimer.Stop();
                gotospace = true;
            }
            if(game_time_sec >= 180 && !bossfight)
            {
                bossfight = true;
                Sounds.StopBackGround();
                Sounds.PlayBossBackGround();
                boss = new Boss(this);

                progressBarHealthBoss = new ProgressBar();
                progressBarHealthBoss.Minimum = 0;
                progressBarHealthBoss.Width = MainCanvas.ActualWidth;
                progressBarHealthBoss.Height = 15;
                progressBarHealthBoss.Background = Brushes.Black;
                progressBarHealthBoss.Maximum = boss.BossHealthPoint;
                progressBarHealthBoss.Value = boss.BossHealthPoint;
                MainCanvas.Children.Add(progressBarHealthBoss);
            }
        }

        private void ObstsGeneratorTimerTick(object sender, EventArgs e)
        {
            for (int i = 0; i < obst_cout_increasing_for_up_difficulty; i++)
            {
                Obstacle obst = new Obstacle(this);
                CurrentObsts.Add(obst);
                AnimationsRace.AnimationObstacles(obst);
            }
        }

        private void BonusGeneratorTimerTick(object sender, EventArgs e)
        {
            // up difficulty
            if(ship.ShipScore > prev_ship_score + 500 && obst_cout_increasing_for_up_difficulty < 15)
            {
                prev_ship_score = ship.ShipScore;
                obst_cout_increasing_for_up_difficulty += 2;
                ConsoleMethod.WriteToConsole("Difficulty up! Now " + obst_cout_increasing_for_up_difficulty, Brushes.Green);
            }


            ship.ShipScore += 1;
            if(RandForSomethings.Between(0,5) == 1)
            {
                if (RandForSomethings.Between(0, 1) == 0)
                {
                    AmmoBonus bonus = new AmmoBonus(this);
                    CurrentBonuses.Add(bonus);
                    AnimationsRace.AnimationBonus(bonus);
                }
                else
                {
                    HealthBonus bonus = new HealthBonus(this);
                    CurrentBonuses.Add(bonus);
                    AnimationsRace.AnimationBonus(bonus);
                }
            }
        }

        private void CollisionTimerTimerTick(object sender, EventArgs e)
        {
            BetterRandom random_for_some = new BetterRandom();

            // check collision obsts and ship
            for (int i = 0; i < CurrentObsts.Count; i++)
            {
                if (ship.ShipHitBox.IntersectsWith(CurrentObsts[i].GetHitBoxObst()))
                {
                    ser_hurt = true;
                    ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_hurt"];
                    ConsoleMethod.WriteToConsole("Obst number " + i + " hitted!", Brushes.White);
                    ship.ShipHp = ship.ShipHp - CurrentObsts[i].ObstDamage <= 0 ?
                        0 : (int)(ship.ShipHp - CurrentObsts[i].ObstDamage);
                    AnimationsRace.AnimationShipDamage(ship);

                    Sounds.PlaySoundOnce("hurt_"+ random_for_some.Between(1, 2) +".wav");
                    
                    if(ship.ShipHp == 0)
                    {
                        GameOver();
                    }
                }
            }

            // check collision bullets and obsts
            for (int i = ship.CurrentAmmos.Count - 1; i >=0; i--)
            {
                // bullet on obsts
                for (int j = 0; j < CurrentObsts.Count; j++)
                {
                    if (CurrentObsts[j].GetHitBoxObst().IntersectsWith(
                        ship.GetHitBoxFire(ship.CurrentAmmos[i])) &&
                        !CurrentObsts[j].Hitted)
                    {
                        CurrentObsts[j].Hitted = true;
                        ship.ShipScore += 10;
                        ConsoleMethod.WriteToConsole("Obst number " + j + " fired by bullet number "+ i + "!", Brushes.White);
                        CurrentObsts[j].ObstacleFiredAnimation();
                        RemoveElementAfterAnimation(CurrentObsts[j].ObstToCanvas);
                        CurrentObsts.Remove(CurrentObsts[j]);
                        Sounds.ObstDamageSoundPlay();
                    }
                }

                // bullet on boss
                if(bossfight)
                {
                    if(boss.GetBossHitBox().IntersectsWith(ship.GetHitBoxFire(ship.CurrentAmmos[i])))
                    {
                        if (boss.BossHealthPoint >= 0)
                        {
                            MainCanvas.Children.Remove(ship.CurrentAmmos[i]);
                            boss.BossHealthPoint -= ship.ShipFireDamage;
                            progressBarHealthBoss.Value = boss.BossHealthPoint;
                            ConsoleMethod.WriteToConsole("Boss hitted!", Brushes.GreenYellow);
                            continue;
                        }
                        else
                        {
                            GameWin();
                        }
                    }
                }
            }

            // collision boss fire with seryoja
            if (bossfight)
            {
                for (int i = 0; i < boss.CurrentBossAmmos.Count; i++)
                {
                    try
                    {
                        Rect BossFireHitBox = new Rect();
                        BossFireHitBox.Width = boss.CurrentBossAmmos[i].Width;
                        BossFireHitBox.Height = boss.CurrentBossAmmos[i].Height;
                        BossFireHitBox.X = boss.CurrentBossAmmos[i].Margin.Left;
                        BossFireHitBox.Y = boss.CurrentBossAmmos[i].Margin.Top;

                        if (BossFireHitBox.IntersectsWith(ship.ShipHitBox))
                        {
                            if (ship.ShipHp > 0)
                                ship.ShipHp = ship.ShipHp - boss.BossFireDamage <= 0 ? 0 : ship.ShipHp - boss.BossFireDamage;
                            else
                                GameOver();
                        }
                    }
                    catch { }
                }
            }

            // check collision with bonuses
            for (int i = 0; i < CurrentBonuses.Count; i++)
            {
                if(ship.ShipHitBox.IntersectsWith(CurrentBonuses[i].GetHitBoxObst()) && !CurrentBonuses[i].Hitted)
                {
                    CurrentBonuses[i].Hitted = true;
                    if (CurrentBonuses[i] is AmmoBonus)
                    {
                        ser_hurt = true;
                        ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_happy"];
                        ConsoleMethod.WriteToConsole("Ammo Bonus obtained!", Brushes.White);
                        ship.ShipAmmo += (CurrentBonuses[i] as AmmoBonus).ammo_count;
                        Sounds.PlaySoundOnce("meh_" + random_for_some.Between(1, 2) + ".wav");
                    }
                    else if (CurrentBonuses[i] is HealthBonus)
                    {
                        ConsoleMethod.WriteToConsole("HP Bonus obtained!", Brushes.White);
                        if (ship.ShipHp < 100)
                        {
                            ser_hurt = true;
                            ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_happy"];
                            ship.ShipHp = ship.ShipHp + (CurrentBonuses[i] as HealthBonus).health_count >= 100 ?
                                100 : ship.ShipHp + (CurrentBonuses[i] as HealthBonus).health_count;
                            Sounds.PlaySoundOnce("meh_" + random_for_some.Between(1, 2) + ".wav");
                        }
                        else
                            continue;
                    }
                    RemoveElementAfterAnimation(CurrentBonuses[i].bonus_rectangle);
                    CurrentBonuses.Remove(CurrentBonuses[i]);
                }
            }
        }

        public void RemoveElementAfterAnimation(UIElement element)
        {
            MainCanvas.Children.Remove(element);
        }

        private bool CheckShipHBToCanvasBorder()
        {
            if (ship.ShipRectangle.Margin.Left <= 0)
            {
                ship.ShipRectangle.Margin = new Thickness(
                    1, ship.ShipRectangle.Margin.Top, 0,0
                    );
                return false;
            }
            else if (ship.ShipRectangle.Margin.Left + ship.ShipRectangle.Width >= MainCanvas.ActualWidth)
            {
                ship.ShipRectangle.Margin = new Thickness(
                    MainCanvas.ActualWidth - ship.ShipRectangle.Width - 1, ship.ShipRectangle.Margin.Top, 0, 0
                    );
                return false;
            }
            else if (ship.ShipRectangle.Margin.Top <= 0)
            {
                ship.ShipRectangle.Margin = new Thickness(
                    ship.ShipRectangle.Margin.Left,
                    1, 0, 0
                    );
                return false;
            }
            else if (ship.ShipRectangle.Margin.Top + ship.ShipRectangle.Height >= MainCanvas.ActualHeight)
            {
                ship.ShipRectangle.Margin = new Thickness(
                    ship.ShipRectangle.Margin.Left,
                    MainCanvas.ActualHeight - ship.ShipRectangle.Height - 1, 0, 0
                    );
                return false;
            }
            return true;
        }
        private void KeyTrackTimerTimerTick(object sender, EventArgs e)
        {
            if (!CheckShipHBToCanvasBorder()) return;

            if(!ser_hurt)
                ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_mouth_close"];

            if (leftpress)
            {
                ser_hurt = false;
                ship.ShipLeft();
            }
            else if (rightpress)
            {
                ser_hurt = false;
                ship.ShipRight();
            }
            else if (uppress)
            {
                ser_hurt = false;
                ship.ShipUp();
            }
            else if (downpress)
            {
                ser_hurt = false;
                ship.ShipDown();
            }
            else if (spacepress)
            {
                ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_mouth_open"];
                ser_hurt = false;
                ship.ShipFire();
            }

            if (!spacepress && !ser_hurt)
            {
                ship.ShipSprite.Visual = (Visual)Application.Current.Resources["seryoja_mouth_close"];
            }

            if (leftpress && spacepress)
            {
                ship.ShipFire();
                ship.ShipLeft();
            }
            else if (rightpress && spacepress)
            {
                ship.ShipFire();
                ship.ShipRight();
            }
            else if (uppress && spacepress)
            {
                ship.ShipFire();
                ship.ShipUp();
            }
            else if (downpress && spacepress)
            {
                ship.ShipFire();
                ship.ShipDown();
            }

            if (leftpress && uppress)
            {
                ship.ShipLeft();
                ship.ShipUp();
            }
            else if (rightpress && uppress)
            {
                ship.ShipRight();
                ship.ShipUp();
            }
            else if (leftpress && downpress)
            {
                ship.ShipLeft();
                ship.ShipDown();
            }
            else if (rightpress && downpress)
            {
                ship.ShipRight();
                ship.ShipDown();
            }

            if (leftpress && uppress && spacepress)
            {
                ship.ShipFire();
                ship.ShipLeft();
                ship.ShipUp();
            }
            else if (rightpress && uppress && spacepress)
            {
                ship.ShipFire();
                ship.ShipRight();
                ship.ShipUp();
            }
            else if (leftpress && downpress && spacepress)
            {
                ship.ShipFire();
                ship.ShipLeft();
                ship.ShipDown();
            }
            else if (rightpress && downpress && spacepress)
            {
                ship.ShipFire();
                ship.ShipRight();
                ship.ShipDown();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            setarrowarr(e.Key);
            switch (e.Key)
            {
                case Key.Left:
                    {
                        leftpress = true;
                        break;
                    }
                case Key.Up:
                    {
                        uppress = true;
                        break;
                    }
                case Key.Right:
                    {
                        rightpress = true;
                        break;
                    }
                case Key.Down:
                    {
                        downpress = true;
                        break;
                    }
                case Key.Space:
                    {
                        if (ship.ShipAmmo > 0)
                        {
                            Sounds.PlaySoundOnce("laser_shoot.wav");
                        }
                        else
                        {
                            Sounds.EmpLaserSoundPlay();
                        }
                        spacepress = true;
                        break;
                    }
                case Key.Oem3:
                    {
                        bool has_console = false;
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is Console)
                            {
                                has_console = true;
                                item.Close();
                                break;
                            }
                        }
                        if (!has_console)
                        {
                            Console _console = new Console();
                            _console.Owner = this;
                            _console.Show();
                        }
                        break;
                    }
                case Key.Escape:
                    {
                        Pause();
                        break;
                    }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    {
                        leftpress = false;
                        break;
                    }
                case Key.Up:
                    {
                        uppress = false;
                        break;
                    }
                case Key.Right:
                    {
                        rightpress = false;
                        break;
                    }
                case Key.Down:
                    {
                        downpress = false;
                        break;
                    }
                case Key.Space:
                    {
                        spacepress = false;
                        break;
                    }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //MainCanvas.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Hidden;
            StatusBar.Visibility = Visibility.Visible;
            InitGame();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Customize_Click(object sender, RoutedEventArgs e)
        {
            HatSelect hatSelect = new HatSelect();
            hatSelect.Owner = this;
            hatSelect.ShowDialog();
        }
    }
}
