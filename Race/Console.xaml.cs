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
using System.Windows.Shapes;

namespace Race
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : Window
    {
        int itsFirstOn = 0;

        public Console()
        {
            InitializeComponent();
            this.Loaded += Loaded_Console;
            MainInput.TextChanged += MainInput_ch;
        }

        private void MainInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                string[] command_line = MainInput.Text.Split(' ');
                ConsoleMethod.WriteToConsole(MainInput.Text, Brushes.Red);
                string command = command_line.Length > 0 ? command_line[0] : "";
                string param = command_line.Length > 1 ? command_line[1] : "";
                ConsoleMethod.RunConsoleCommand(command, param);
            }
        }
        private void MainInput_ch(object sender, EventArgs e)
        {
            if (itsFirstOn == 0)
            {
                MainInput.Clear();
                itsFirstOn = 1;
            }
        }
        private void Loaded_Console(object sender, EventArgs e)
        {
            MainInput.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem3)
            {
                this.Close();
            }
        }
    }
}
