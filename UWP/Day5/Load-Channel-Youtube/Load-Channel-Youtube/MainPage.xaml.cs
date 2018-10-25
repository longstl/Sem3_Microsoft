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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Load_Channel_Youtube.entity;
using MyToolkit.Multimedia;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Load_Channel_Youtube
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            RootObject myChannel = await YoutubeProxy.GetChannel();
            txtTitle.Text = myChannel.items[0].snippet.channelTitle;
            ResultDescription.Text = myChannel.items[0].snippet.description;

            string YoutubeId = myChannel.items[0].contentDetails.upload.videoId;
            YouTubeUri url = await YouTube.GetVideoUriAsync(YoutubeId, YouTubeQuality.Quality720P);
            YoutubePlayer.Source = url.Uri;
            YoutubePlayer.Play();
        }
    }
}
