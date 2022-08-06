using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;
namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecontElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecontElapsed++;
            timeTextBlock.Text = (tenthsOfSecontElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text;
                var message = "Well done , you found all the combinations " +
                                              $"\t Want to start a new game?";
                var title = "EndGame";
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                MessageBoxImage image = MessageBoxImage.Information;
                DialogResult result = MessageBox.Show(message, title, button, (MessageBoxIcon)image);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SetUpGame();
                  
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }

            }
        }

        private void SetUpGame()
        {

            var animalEmoji = new List<string>()
            {
                "💀", "💀",

                "❤️", "❤️",

                "😂", "😂",

                "😊", "😊",

                "✨", "✨",

                "🙉", "🙉",

                "⚽", "⚽",

                "🗺", "🗺"



            };

            var random = new Random();
            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {

                    textBlock.Visibility = Visibility.Visible;
                    var index = random.Next(animalEmoji.Count);
                    var nextEmoji = animalEmoji[index];

                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecontElapsed = 0;
            matchesFound = 0;
        }

        TextBlock _lastTextBlockClicked;
        bool _findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (_findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                _lastTextBlockClicked = textBlock;
                _findingMatch = true;
            }
            else if (textBlock.Text == _lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {

                SetUpGame();
            }
        }
    }
}
