﻿using System;
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

namespace Race
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public List<Obstacle> CurrentObsts = null;
        public List<Bonus> CurrentBonuses = null;

        bool leftpress = false;
        bool rightpress = false;
        bool uppress = false;
        bool downpress = false;
        bool spacepress = false;
        bool ser_hurt = false;

        public int game_time_sec = 0;

        public string selected_hat = "hat_0";

        //story board flags
        bool gotospace = false;

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConsoleMethod.WriteToConsole("Game window loaded", Brushes.White);
            Sounds.PlayBackGround();
            clouds = new Clouds(this);
        }

        private void InitGame()
        {
            for (int i = MainCanvas.Children.Count - 1; i >= 0; i--)
            {
                if ((MainCanvas.Children[i] is StackPanel) ||
                    (MainCanvas.Children[i] is Rectangle && (MainCanvas.Children[i] as Rectangle).Name == "hat"))
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
            ship = new StarShip(this);
            ship.HatSprite.Visual = (Visual)Application.Current.Resources[selected_hat];
            this.DataContext = ship;
            StoryBoardTimer.Start();
            BonusGeneratorTimer.Start();
            ObstsGeneratorTimer.Start();
            KeyTrackTimer.Start();
            CollisionTimer.Start();
        }
        private void GameOver()
        {
            Sounds.StopBackGround();
            Sounds.ShipDestroySoundPlay();
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
            for (int i = 0; i < ship.CurrentAmmos.Count; i++)
            {
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
