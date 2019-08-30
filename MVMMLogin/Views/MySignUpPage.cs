using System;

using Xamarin.Forms;

namespace MVMMLogin.Views
{
    public class MySignUpPage : ContentPage
    {
        public MySignUpPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

