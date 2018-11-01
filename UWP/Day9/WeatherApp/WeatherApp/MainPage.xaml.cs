using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherApp.view;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadingProgres();
        }

        private async void LoadingProgres()
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;

            for (int i = 15; i >= 1; i--)
            {
                await Task.Delay(1000);
            }
            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
            this.MyFrame.Navigate(typeof(OneDay));
            OneDay.IsSelected = true;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OneDay.IsSelected)
            {
                ProgressRing.IsActive = true;
                ProgressRing.Visibility = Visibility.Visible;

                for (int i = 15; i >= 1; i--)
                {
                    await Task.Delay(1000);
                }
                ProgressRing.IsActive = false;
                ProgressRing.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(OneDay));
            }
            else if (FiveDay.IsSelected)
            {
                ProgressRing.IsActive = true;
                ProgressRing.Visibility = Visibility.Visible;

                for (int i = 15; i >= 1; i--)
                {
                    await Task.Delay(1000);
                }
                ProgressRing.IsActive = false;
                ProgressRing.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(FiveDay));
            }
        }
    }
}
