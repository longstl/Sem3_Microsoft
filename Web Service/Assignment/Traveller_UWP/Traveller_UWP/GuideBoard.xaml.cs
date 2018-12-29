using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GuideBoard : Page
    {
        public GuideBoard()
        {
            this.InitializeComponent();
            txtTitle.Text = CurrentUser.currentUser.firstName + " " + CurrentUser.currentUser.lastName;
        }

        private void ListBox_Flyout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogOut.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage));
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                this.MyFrame.Navigate(typeof(HomePage));
            }else if (CreatePosts.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(CreatePostsPage));
            }
            else if (MyPosts.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MyPostsPage));
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.MyFrame.Navigate(typeof(HomePage));
        }
    }
}
