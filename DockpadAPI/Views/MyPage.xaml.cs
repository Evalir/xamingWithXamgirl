using System;
using System.Collections.Generic;
using DockpadAPI.ViewModels;
using Xamarin.Forms;

namespace DockpadAPI.Views
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
            BindingContext = new MyPageViewModel();
        }
    }
}
