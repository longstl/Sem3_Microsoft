using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1_Demo2
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

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Button

            Button myButtton = new Button();
            myButtton.Name = "Click Button";
            myButtton.Content = "Click Me";
            myButtton.Width = 200;
            myButtton.Height = 80;
            myButtton.Margin = new Thickness(20,10,20,10);
            myButtton.HorizontalAlignment = HorizontalAlignment.Left;
            myButtton.VerticalContentAlignment = VerticalAlignment.Center;
            myButtton.Background = new SolidColorBrush(Colors.Wheat);
            myButtton.Click += ClickMeButton_Click;
        } 

        private async void ClickMeButton_Click(object sender, RoutedEventArgs e)
        {
            var txtTextBox = MyTextBox.Text;
            MyTextBlock.Text = txtTextBox;
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txtTextBox);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
