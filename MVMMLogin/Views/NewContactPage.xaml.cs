using System;
using System.Collections.Generic;
using MVMMLogin.Models;
using MVMMLogin.ViewModels;
using Xamarin.Forms;

namespace MVMMLogin.Views
{
    public partial class NewContactPage : ContentPage
    {
        public NewContactPage()
        {
            InitializeComponent();
            BindingContext = new NewContactPageViewModel();
        }

        public NewContactPage(Contact contact)
        {
            InitializeComponent();
            BindingContext = new NewContactPageViewModel(contact);
        }

    }
}
