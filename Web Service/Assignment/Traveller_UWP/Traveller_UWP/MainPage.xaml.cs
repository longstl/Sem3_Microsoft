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
using Traveller_UWP.Ultilities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                this.MyFrame.Navigate(typeof(HomePage));
            }
        }

        private void ListBox_Flyout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Login.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Login));
            }
            else if(Register.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Login));
            }
        }


        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(HomePage));
        }
    }
}
