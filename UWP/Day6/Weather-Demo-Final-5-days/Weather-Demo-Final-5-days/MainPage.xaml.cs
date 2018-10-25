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
        ObservableCollection<OpenWeatherMap.List> list;


        public MainPage()
        {
            this.InitializeComponent();
        }

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

                    OpenWeatherMap.RootObject root = await ApiHandle.GetWeather(lat, lon);
                    list = root.list;
                    for (int i = 0; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].weather.Count; j++)
                        {
                            if (MyListView.Items != null) MyListView.Items.Add(list[i].weather[j]);
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
