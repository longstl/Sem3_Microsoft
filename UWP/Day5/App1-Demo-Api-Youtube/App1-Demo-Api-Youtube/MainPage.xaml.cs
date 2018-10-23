using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using App1_Demo_Api_Youtube.entity;
using App1_Demo_Api_Youtube.model;
using App1_Demo_Api_Youtube.view;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1_Demo_Api_Youtube
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            loadingProgres();
        }

        private async void loadingProgres()
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;

            for (int i = 15; i >= 1; i--)
            {
                await Task.Delay(1000);
            }
            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;
            this.MyFrame.Navigate(typeof(HomeContent));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_Flyout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Account.RootObject account = await ApiHandle.GetAccount();
                txtTitle.Text = account.items[0].snippet.title;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(account.items[0].snippet.thumbnails.@default.url, UriKind.Absolute));
                btnImage.Content = image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not connect to API");
            }
        }

        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            string param = args.QueryText;
        }
    }
}
