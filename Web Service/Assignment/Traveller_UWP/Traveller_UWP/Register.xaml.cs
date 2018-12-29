using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Traveller_UWP.Ultilities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Traveller_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
            DoBCalenderPicker.Date = DateTime.Today;
        }

        private async void BtnRegister_OnClick(object sender, RoutedEventArgs e)
        {
            Member user = new Member();
            user.email = this.txtEmail.Text;
            user.password = this.txtPassword.Password;
            user.address = this.txtAddress.Text;
            user.phone = this.txtPhone.Text;
            user.dob = this.DoBCalenderPicker.Date.Value.ToString("MM-dd-yyyy");
            user.firstName = this.txtFirstName.Text;
            user.lastName = this.txtLastName.Text;
            var content = ApiHandle.Register(user);
            if (content.Result.IsSuccessStatusCode)
            {
                txtError.Foreground = new SolidColorBrush(Colors.Green);
                txtError.Text = "Register Success!!!!";
                await Task.Delay(1000);
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MainPage));
            }
            else
            {
                txtError.Foreground = new SolidColorBrush(Colors.Red);
                txtError.Text = "Register Failed!!!!";
            }
        }

        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = String.Empty;
            txtPassword.Password = String.Empty;
            txtAddress.Text = String.Empty;
            txtPhone.Text = String.Empty;
            DoBCalenderPicker.Date = DateTime.Today;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtError.Text = String.Empty;
            txtErrorEmail.Text = String.Empty;
            txtErrorPassword.Text = String.Empty;
            txtErrorAddress.Text = String.Empty;
            txtErrorDoB.Text = String.Empty;
            txtErrorFirstName.Text = String.Empty;
            txtErrorLastName.Text = String.Empty;
            txtErrorPhone.Text = String.Empty;
        }

        private void LinkLogin_OnClick(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
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
            if (Login.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Login));
            }
            else if (Register1.IsSelected)
            {
                var rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Register));
            }
        }
    }
}
