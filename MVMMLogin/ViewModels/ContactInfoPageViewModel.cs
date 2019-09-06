using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MVMMLogin.Models;
using MVMMLogin.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MVMMLogin.ViewModels
{
    public class ContactInfoPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Contact Contact { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand CallCommand { get; set; }
        public ICommand PopPopupCommand { get; set; }

        public ContactInfoPageViewModel()
        {
            MessagingCenter.Subscribe<ContactsPageViewModel, Contact>(this, new Constants().ContactInfoMessage, (sender, param) =>
            {
                Debug.WriteLine(sender);
                Debug.WriteLine(param);
                Contact = param;
                MessagingCenter.Unsubscribe<ContactsPageViewModel, Contact>(this, "CONTACT_INFO");
            });

            EditCommand = new Command(async (contact) =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NewContactPage(contact as Contact));
                await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send<ContactInfoPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage, false);
            });

            CallCommand = new Command((phone) =>
            {
                Device.OpenUri(new Uri($"tel:{phone}"));
                Debug.WriteLine(phone);
            });

            PopPopupCommand = new Command(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
            });
        }
    }
}
