using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMBindingNavx.ViewModels;
using Xamarin.Forms;

namespace MVVMBindingNavx
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
            var btn = sender as Button;
            entryLabel.TextColor = btn.BackgroundColor;
            await Navigation.PushAsync(new SecondPage(btn.BackgroundColor, btn.Text));
        }
    }
}
