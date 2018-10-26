using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
using Newtonsoft.Json;
using Weather_Demo_Final_5_days.entity;
using Weather_Demo_Final_5_days.model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weather_Demo_Final_5_days
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            collection = new ObservableCollection<OpenWeatherMap.List>();
            this.DataContext = this;
        }

        public ObservableCollection<OpenWeatherMap.List> collection { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    var postion = await ApiHandle.GetPosition();

#pragma warning disable CS0618 // Type or member is obsolete
                    var lat = postion.Coordinate.Latitude;
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                    var lon = postion.Coordinate.Longitude;
#pragma warning restore CS0618 // Type or member is obsolete

                    OpenWeatherMap.RootObject forecast = await ApiHandle.GetWeather(lat, lon);
                    CityTextBlock.Text = forecast.city.name;
                    
                    for (int i = 0; i < forecast.list.Count; i++)
                    {
                        //collection.Add(forecast.list[i]);
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
