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
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;

namespace Stopwatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Countdown myCountDown = new Countdown(0, 0, 0); 
        public MainWindow()
        {
            InitializeComponent();
            
        }
        //This is the start of the timer. 
        //It starts the timer, but also hides the input boxes, and disables the reset button.
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                myCountDown._hours = int.Parse(HoursInput.Text);
                myCountDown._minuts = int.Parse(MinutsInput.Text);
                myCountDown._seconds = int.Parse(SecondsInput.Text);
                myCountDown.SetTime();
                myCountDown.CountdownChanged += CountdownUpdateChanged;
                myCountDown.PauseIsAvailable = true;
                myCountDown.IsPaused = false;

                StartStopButton.Content = "Stop";
                ResetButton.IsEnabled = false;
                InputsWrapPanel.Visibility = Visibility.Hidden;
            }
        }
        //This is the pause of the timer.
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            myCountDown.IsPaused = true;
            //myCountDown.CountdownChanged -= CountdownUpdateChanged;
            StartStopButton.Content = "Start";
            ResetButton.IsEnabled = true;
        }
        //This is the reset of the timer.
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InputsWrapPanel.Visibility = Visibility.Visible;
            myCountDown.PauseIsAvailable = false;
        }
        //Validates if the inputs are numbers.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //Checks if there is an input, if there is no input in any of them, then a messagebox will pop up.
        //If there is only an input in fx. Minuts, then it will set Hours and Seconds to 0.
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(HoursInput.Text) && string.IsNullOrEmpty(MinutsInput.Text) && string.IsNullOrEmpty(SecondsInput.Text))
            {
                MessageBox.Show("You need to input something.");
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(HoursInput.Text))
                {
                    HoursInput.Text = "0";
                }
                if (string.IsNullOrEmpty(MinutsInput.Text))
                {
                    MinutsInput.Text = "0";
                }
                if (string.IsNullOrEmpty(SecondsInput.Text))
                {
                    SecondsInput.Text = "0";
                }
                return true;
            }
        }
        //Event to write out the countdown time.
        private void CountdownUpdateChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                CounterHours.Content = "Hours: " + ((CountdownEventArgs)e).RemainingCountdownTime.Hours.ToString();
                CounterMinuts.Content = "Minuts: " + ((CountdownEventArgs)e).RemainingCountdownTime.Minutes.ToString();
                CounterSeconds.Content = "Seconds: " + ((CountdownEventArgs)e).RemainingCountdownTime.Seconds.ToString();
            });
        }
    }
}
