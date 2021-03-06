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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Vorobiov.classes;

namespace Wpf_Vorobiov.pages
{
    /// <summary>
    /// Логика взаимодействия для media.xaml
    /// </summary>
    public partial class media : Page
    {
        TimeLine timeLine = new TimeLine();
        public static double currentMain { get; set; } = 0;
        public media()
        {
            InitializeComponent();
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.From = Color.FromRgb(255, 176, 46);
            colorAnimation.To = Color.FromRgb(152, 251, 152);
            colorAnimation.Duration = TimeSpan.FromSeconds(10);
            colorAnimation.AutoReverse = true;
            colorAnimation.RepeatBehavior = RepeatBehavior.Forever;
            DataContext = timeLine;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "Play":
                    VideoPlayer.Play();
                    timeLine.IsPlay = true;

                    break;
                case "Pause":
                    VideoPlayer.Pause();
                    timeLine.IsPlay = false;
                    break;
                case "Stop":
                    VideoPlayer.Stop();
                    timeLine.current = 0;
                    timeLine.IsPlay = false;
                    break;
            }
        }
        private void VideoPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            slPosition.Minimum = 0;
            slPosition.Maximum = VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            currentMain = VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void slPosition_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VideoPlayer.Position = TimeSpan.FromSeconds(slPosition.Value);
            timeLine.IsPlay = true;
        }
    }
}
