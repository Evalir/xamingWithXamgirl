using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace HW1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public async void LoginClicked(object sender, EventArgs args)
        {
            if (String.IsNullOrEmpty(usernameEntry.Text))
                await DisplayAlert("Login Issue", "Username cannot be empty.", "Ok");
            else if (String.IsNullOrEmpty(passwordEntry.Text))
                await DisplayAlert("Login Issue", "Password cannot be empty.", "Ok");
            else
                await DisplayAlert("Login successful", $"Welcome {usernameEntry.Text}!", "Ok");
        }
    }
}
