using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StyledNavigationx
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage(Color color, String buttonText)
        {
            InitializeComponent();
            pageButton.Text = buttonText;
            pageButton.TextColor = Color.White;
            General.BackgroundColor = color;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            await DisplayAlert("Hey!", "Going back to the previous page", "Ok");
            await Navigation.PopAsync();
        }
    }
}
