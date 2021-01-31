using EngineLibrary.EngineComponents;
using GameLibrary.Game;
using GameLibrary.Maze;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RenderingApplication application;
        private MazeScene mazeScene;

        public MainWindow()
        {
            InitializeComponent();

            application = new RenderingApplication();
            mazeScene = new MazeScene();
            GameEvents.ChangeCoins += ChangeCoins;
            GameEvents.EndGame += EndGame;
            GameEvents.ChangeEffect += ChangeEffect;
            GameEvents.ChangeGun += ChangeGun;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(application != null)
                application.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonPlay.IsEnabled = false;

            HelpPanel.Visibility = Visibility.Hidden;
            MenuPanel.Visibility = Visibility.Hidden;

            BluePlayerPanel.Visibility = Visibility.Visible;
            RedPlayerPanel.Visibility = Visibility.Visible;

            formhost.Child = application.RenderForm;

            application.SetScene(mazeScene);
            application.Run();
        }

        private void EndGame(string winPlayer)
        {
            formhost.Visibility = Visibility.Hidden;

            BPGun.Visibility = Visibility.Hidden;
            RPGun.Visibility = Visibility.Hidden;
            BPEffectText.Text = "";
            RPEffectText.Text = "";

            if (winPlayer == "Blue Player")
                WinPlayerText.Foreground = new SolidColorBrush(Color.FromRgb(9, 46, 168));
            else
                WinPlayerText.Foreground = new SolidColorBrush(Color.FromRgb(179, 22, 22));

            WinPlayerText.Text = winPlayer + " Win!";

            Uri resourceLocater = new Uri("/Images/"+ winPlayer + ".png", UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(resourceLocater);

            WinPlayerImage.Source = bitmap;

            WinPanel.Visibility = Visibility.Visible;

            GameEvents.EndGame -= EndGame;
            GameEvents.ChangeCoins -= ChangeCoins;
        }

        private void ChangeCoins(string player, int value)
        {
            if (player == "Blue Player")
                BluePlayerCoins.Text = value.ToString();
            else
                RedPlayerCoins.Text = value.ToString();
        }

        private void ChangeEffect(string player, string effect)
        {
            TextBlock textBlock;

            if (player == "Blue Player")
                textBlock = BPEffectText;
            else
                textBlock = RPEffectText;

            switch (effect)
            {
                case "Death":
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(246, 242, 0));
                    break;
                case "Slowdown":
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(29, 216, 0));
                    break;
                case "Frezze":
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 230, 255));
                    break;
            }

            textBlock.Text = effect;
        }

        private void ChangeGun(string player, string gun)
        {
            Image image;

            if (player == "Blue Player")
                image = BPGun;
            else
                image = RPGun;

            if (gun == "")
            {
                image.Visibility = Visibility.Hidden;
            }
            else
            {
                Uri resourceLocater = new Uri("/Images/Guns/" + gun + ".png", UriKind.Relative);
                BitmapImage bitmap = new BitmapImage(resourceLocater);

                image.Source = bitmap;
                image.Visibility = Visibility.Visible;
            }
        }
    }
}
