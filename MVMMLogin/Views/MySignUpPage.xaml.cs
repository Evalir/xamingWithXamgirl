using System;
using System.Collections.Generic;
using MVMMLogin.ViewModels;
using Xamarin.Forms;

namespace MVMMLogin.Views
{
    public partial class MySignUpPage : ContentPage
    {
        public MySignUpPage()
        {
            InitializeComponent();
            BindingContext = new MySignUpPageViewModel();
        }
    }
}
