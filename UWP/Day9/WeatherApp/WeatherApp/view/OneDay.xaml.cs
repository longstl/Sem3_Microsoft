using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WeatherApp.entity;
using WeatherApp.model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherApp.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OneDay : Page
    {
        public OneDay()
        {
            this.InitializeComponent();
        }

        private async void OneDay_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var postion = await LocationData.getPosition();

#pragma warning disable CS0618 // Type or member is obsolete
                var lat = postion.Coordinate.Latitude;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                var lon = postion.Coordinate.Longitude;
#pragma warning restore CS0618 // Type or member is obsolete

                OpenWeatherMap1day.RootObject myWeather = await ApiHandle.GetWeatherOneDay(lat, lon);
                Debug.WriteLine(myWeather);
                string icon = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                TempTextBlock.Text = myWeather.main.temp.ToString();
                DescriptionTextBlock.Text = myWeather.weather[0].description;
                LocationTextBlock.Text = myWeather.name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not connect to API");
            }
        }
    }
}
