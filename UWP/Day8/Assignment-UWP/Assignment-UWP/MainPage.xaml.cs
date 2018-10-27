using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
using Assignment_UWP.entity;
using Assignment_UWP.model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            collection = new ObservableCollection<NewsEntity.RootObject>();
            this.DataContext = this;
        }

        public ObservableCollection<NewsEntity.RootObject> collection { get; set; }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            ProgressRing.Visibility = Visibility.Visible;

            for (int i = 15; i >= 1; i--)
            {
                await Task.Delay(1000);
            }

            ProgressRing.IsActive = false;
            ProgressRing.Visibility = Visibility.Collapsed;

            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    List<string> contentList = new List<string>();
                    List<NewsEntity.RootObject> newList = await ApiHandle.GetNews();
                    for (int i = 0; i < newList.Count; i++)
                    {
                        string removeHtml = RemoveHtmlTag(newList[i].content.rendered);
                        var splitContent = removeHtml.Split('.');
                        var shortConent = splitContent[0] + "." + splitContent[1] + "." + splitContent[2] + "." + splitContent[3];
                        newList[i].content.rendered = shortConent;
                        collection.Add(newList[i]);
                    }
                    NewsGridView.ItemsSource = collection;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private static string RemoveHtmlTag(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }

        private void NewsGridView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
