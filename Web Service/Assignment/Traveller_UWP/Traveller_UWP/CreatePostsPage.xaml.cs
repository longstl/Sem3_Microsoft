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
using Windows.UI.Xaml.Navigation;
using Traveller_UWP.Entity;
using Traveller_UWP.Ultilities;
using Image = Windows.UI.Xaml.Controls.Image;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePostsPage : Page
    {
        public CreatePostsPage()
        {
            this.InitializeComponent();
            txtTitle.Text = CurrentUser.currentUser.firstName + " " + CurrentUser.currentUser.lastName;

            txtTitlePost.Text = "Hanoi ơi Hanoi";
            txtContent.Text = "aaaaaaaaaaaaaaaaaaaa";
            txtTags.Text = "Abc, 123, Hanoi";
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
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(GuideBoard));
            }
            else if (CreatePosts.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(CreatePostsPage));
            }
            else
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MyPostsPage));
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            CreateParameters parameters = new CreateParameters();
            Posts post = new Posts();
            List<Tag> listTags = new List<Tag>();
            Tag tag = new Tag();

            post.title = txtTitlePost.Text;
            post.content = txtContent.Text;
            post.Traveler_id = CurrentUser.currentUser.id;
            
            string[] spit = txtTags.Text.Split(new[] { ","}, StringSplitOptions.None);
            for (int i = 0; i < spit.Length; i++)
            {
                tag.tag_name = spit[i].Trim();
                listTags.Add(tag);
            }

            parameters.Post = post;
            parameters.Tag = listTags;

            var content = ApiHandle.AddPost(parameters);
            if (content.Result.IsSuccessStatusCode)
            {
                txtError.Foreground = new SolidColorBrush(Colors.Green);
                txtError.Text = "Add Post Success!!!! Next Step";
                await Task.Delay(1000);
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(ImagePost));
            }
            else
            {
                txtError.Foreground = new SolidColorBrush(Colors.Red);
                txtError.Text = "Add Post Failed!!!!";
            }
        }

        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
