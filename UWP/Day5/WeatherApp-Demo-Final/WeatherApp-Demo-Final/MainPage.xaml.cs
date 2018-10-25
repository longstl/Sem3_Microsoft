using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WeatherAppDemo;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherApp_Demo_Final
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var postion = await LocationData.getPosition();

                var lat = postion.Coordinate.Latitude;
                var lon = postion.Coordinate.Longitude;

                RootObject myWeather = await APIManager.GetWeather(lat, lon);
                Debug.WriteLine(myWeather);
                string icon = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[0].weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                TempTextBlock.Text = myWeather.list[0].main.temp.ToString();
                DescriptionTextBlock.Text = myWeather.list[0].weather[0].description;
                LocationTextBlock.Text = myWeather.list[0].name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not connect to API");
            }
        }

    }
}
