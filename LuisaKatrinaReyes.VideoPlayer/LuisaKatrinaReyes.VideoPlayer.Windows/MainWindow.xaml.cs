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
using System.Windows.Threading;

namespace LuisaKatrinaReyes.VideoPlayer.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timer.Tick += new EventHandler(Timer_Tick);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Slider_seek.Value = MediaElement1.Position.TotalSeconds;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MediaElement1.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MediaElement1.Stop();
        }

        private void Slider_vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaElement1.Volume = (double)Slider_vol.Value;
        }

        private void Slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaElement1.Position = TimeSpan.FromSeconds(Slider_seek.Value);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            MediaElement1.Source = new Uri(filename);

            MediaElement1.LoadedBehavior = MediaState.Manual;
            MediaElement1.UnloadedBehavior = MediaState.Manual;
            MediaElement1.Volume = (double)Slider_vol.Value;
            MediaElement1.Play();
        }

        private void MediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = MediaElement1.NaturalDuration.TimeSpan;
            Slider_seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }
    }
}
