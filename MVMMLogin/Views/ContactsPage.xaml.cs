using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MVMMLogin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVMMLogin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {

        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactsPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ContactViewModel = this.BindingContext as ContactsPageViewModel;

            if (ContactViewModel != null)
            {
                ContactViewModel.FetchData();
            }
        }

    }
}
