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
    public class ConsoleMethod
    {
        public static void WriteToConsole(string str_to_out, SolidColorBrush color)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if(item is Console)
                {
                    Run run = new Run(str_to_out + "\n");
                    run.Foreground = color;
                    (item as Console).MainOutput.Inlines.Add(run);
                    (item as Console).ScrollMainOutput.ScrollToEnd();
                    break;
                }
            }
        }
        public static void RunConsoleCommand(string command, string param)
        {
            switch (command)
            {
                case "sethp":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            double hp_count = 0;
                            Double.TryParse(param, out hp_count);
                            (Application.Current.MainWindow as MainWindow).ship.ShipHp =
                                hp_count == 0 ? 100 : hp_count;
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "giveammo":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            double ammo_count = 0;
                            Double.TryParse(param, out ammo_count);
                            (Application.Current.MainWindow as MainWindow).ship.ShipAmmo =
                                ammo_count == 0 ? 250 : ammo_count;
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "showhitbox":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            (Application.Current.MainWindow as MainWindow).ship.ShowHitBoxes =
                               !(Application.Current.MainWindow as MainWindow).ship.ShowHitBoxes ?
                                true : false;
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }

                case "giveammobonus":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            AnimationsRace.AnimationBonus(new AmmoBonus((Application.Current.MainWindow as MainWindow)));
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "givehpbonus":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            AnimationsRace.AnimationBonus(new HealthBonus((Application.Current.MainWindow as MainWindow)));
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "givescore":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            int score = 0;
                            Int32.TryParse(param, out score);
                            (Application.Current.MainWindow as MainWindow).ship.ShipScore =
                                score == 0 ? 250 : score;
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "gotospace":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            AnimationsRace.AnimationGoToSpace();
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "setgametime":
                    {
                        if ((Application.Current.MainWindow as MainWindow).ship != null)
                        {
                            int time = 0;
                            Int32.TryParse(param, out time);
                            if(time != 0)
                            {
                                (Application.Current.MainWindow as MainWindow).game_time_sec = time;
                            }
                        }
                        else
                        {
                            WriteToConsole("Game ist started!", Brushes.Red);
                        }
                        break;
                    }
                case "quit":
                    {
                        Application.Current.Shutdown();
                        break;
                    }
            }
        }
    }
}
