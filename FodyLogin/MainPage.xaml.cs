using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FodyLogin.ViewModels;
using Xamarin.Forms;

namespace FodyLogin
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            await DisplayAlert("Hello!", "You've logged in successfully.", "OK");
        }
    }
}
