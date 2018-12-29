using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;
using Traveller_UWP.Ultilities;
using HttpClient = System.Net.Http.HttpClient;
using HttpMethod = System.Net.Http.HttpMethod;
using HttpRequestMessage = System.Net.Http.HttpRequestMessage;
using HttpResponseMessage = System.Net.Http.HttpResponseMessage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImagePost : Page
    {
        public ImagePost()
        {
            this.InitializeComponent();
            txtTitle.Text = CurrentUser.currentUser.firstName + " " + CurrentUser.currentUser.lastName;
            btnSubmit.Visibility = Visibility.Collapsed;
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

        StorageFile photo;
        private Stream stream = new MemoryStream();
        private async void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            photo = await openPicker.PickSingleFileAsync();
            Debug.WriteLine(photo.Path);
            if (photo != null)
            {
               upload_image(photo.Path);
            }
        }

        async void upload_image(string path)
        {
            string url = "http://localhost:27489/api/v1/guide/upload";
            Console.WriteLine("Uploading {0}", path);
            try
            {
                using (var client = new HttpClient())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        var content = new MultipartFormDataContent();
                        var file_content = new ByteArrayContent(new StreamContent(stream).ReadAsByteArrayAsync().Result);
                        file_content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                        file_content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "File Name",
                            Name = "foo",
                        };
                        content.Add(file_content);
                        var response = await client.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine("Done");
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong while uploading the image");
            }
        }

        //private async void UploadImage(object sender, RoutedEventArgs e)
        //{
        //    string url = "http://localhost:27489/api/v1/guide/upload";

        //    Uri uri = new Uri("http://localhost:27489/api/v1/guide/upload");
        //    HttpClient client = new HttpClient();
        //    HttpStreamContent streamContent = new HttpStreamContent(stream.AsInputStream());
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
        //    //request.Content = streamContent;
        //    //HttpResponseMessage response = await client.PostAsync(uri, streamContent);
        //}
    }
}
