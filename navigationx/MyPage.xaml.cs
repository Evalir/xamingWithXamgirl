using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace navigationx
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }

        public void OnEntryChange(object sender, EventArgs args)
        {
            entryLabel.Text = (sender as Entry).Text;
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            var btn = sender as Button;
            entryLabel.TextColor = btn.BackgroundColor;
            await Navigation.PushAsync(new SecondPage(btn.BackgroundColor, btn.Text));
        }
    }
}
