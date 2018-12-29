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
using Newtonsoft.Json;
using Traveller_UWP.Ultilities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();

            txtEmail.Text = "daodao1402@gmail.com";
            txtPassword.Password = "@bc123";
        }

        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            txtError.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtErrorEmail.Text = String.Empty;
            txtPassword.Password = String.Empty;
            txtErrorPassword.Text = String.Empty;
        }

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Member member = new Member();

            member.email = txtEmail.Text;
            member.password = txtPassword.Password;

            var content = ApiHandle.Login(member);

            if (content.Result.IsSuccessStatusCode)
            {
                txtError.Foreground = new SolidColorBrush(Colors.Green);
                txtError.Text = "Login Success!!!!!";
                await Task.Delay(1000);
                CurrentUser.currentUser = await ApiHandle.GetCurUser(member.email);
                Debug.WriteLine(CurrentUser.currentUser.address);
                if (CurrentUser.currentUser.Role_id == 1)
                {
                    var rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(TravellerBoard));
                }
                else if (CurrentUser.currentUser.Role_id == 2)
                {
                    var rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(GuideBoard));
                }
            }
            else
            {
                txtError.Foreground = new SolidColorBrush(Colors.Red);
                txtError.Text = "Login Faild!!!!";
            }
        }

        private void LinkRegister_OnClick(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Register));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage));
            }
        }

        private void ListBox_Flyout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Login1.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Login));
            }
            else if (Register.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Register));
            }
        }
    }
}
