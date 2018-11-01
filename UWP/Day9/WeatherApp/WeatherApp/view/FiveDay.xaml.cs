using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using WeatherApp.entity;
using WeatherApp.model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherApp.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FiveDay : Page
    {
        public FiveDay()
        {
            this.InitializeComponent();
            collection = new ObservableCollection<OpenWeatherMap5days.List>();
            this.DataContext = this;
        }

        public ObservableCollection<OpenWeatherMap5days.List> collection { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    var postion = await LocationData.getPosition();

#pragma warning disable CS0618 // Type or member is obsolete
                    var lat = postion.Coordinate.Latitude;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                    var lon = postion.Coordinate.Longitude;
#pragma warning restore CS0618 // Type or member is obsolete

                    OpenWeatherMap5days.RootObject forecast = await ApiHandle.GetWeatherFiveDay(lat, lon);
                    CityTextBlock.Text = forecast.city.name;

                    for (int i = 0; i < forecast.list.Count; i++)
                    {
                        for (int j = 0; j < forecast.list[i].weather.Count; j++)
                        {
                            string icon = string.Format("ms-appx:///Assets/Weather/{0}.png", forecast.list[i].weather[j].icon);
                            var listReplace = forecast.list[i].weather[j];
                            listReplace.icon = icon;
                        }
                        collection.Add(forecast.list[i]);
                    }

                    ForeCastGridView.ItemsSource = collection;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
