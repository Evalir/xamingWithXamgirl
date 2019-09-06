using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MVMMLogin.Models;
using MVMMLogin.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace MVMMLogin.ViewModels
{
    public class ContactsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Contact _selectedContact;
        public Contact SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                if (_selectedContact != null)
                    ItemTapped(_selectedContact);
            }
        }

        internal async void FetchData()
        {
            Contacts = await App.Database.GetContactsForUserAsync(App.CurrentUser.Email);
            if (Contacts == null)
                Contacts = new ObservableCollection<Contact>();
        }

        public ObservableCollection<Contact> Contacts { get; set; }
        public ICommand NewContactCommand { get; set; }
        public ICommand MoreElementCommand { get; set; }
        public ICommand SignOutCommand { get; set; }
        public ICommand DeleteElementCommand { get; set; }

        public ContactsPageViewModel()
        {
            NewContactCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NewContactPage());
                MessagingCenter.Send<ContactsPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage, true);
            });

            SignOutCommand = new Command(async () =>
            {
                App.Current.MainPage = new NavigationPage(new MyPage())
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black
                };
                App.CurrentUser = null;
            });

            MoreElementCommand = new Command<Contact>(async (contact) =>
            {
                var result = await App.Current.MainPage.DisplayActionSheet($"What do you wish to do with {contact.Name}?", "Cancel", null, $"Call {contact.Phone}", "Edit");
                if (result == $"Call {contact.Name}")
                {
                    Device.OpenUri(new Uri(string.Format("tel:{0}", $"+{contact.Phone}")));
                }
                else if (result == "Edit")
                {
                    // Navigate to edit page.
                    MessagingCenter.Send<ContactsPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage, false);
                }
            });

            DeleteElementCommand = new Command<Contact>(async (contact) =>
            {
                var result = await App.Current.MainPage.DisplayActionSheet($"Delete Contact {contact.Name}?", "Cancel", "Delete");
                if (result == "Delete")
                {
                    await App.Database.DeleteContactAsync(contact);
                    Contacts.Remove(contact);
                }
            });
        }

        private async void ItemTapped(Contact contact)
        {
            await PopupNavigation.Instance.PushAsync(new ContactInfoPage());
            MessagingCenter.Send<ContactsPageViewModel, Contact>(this, new Constants().ContactInfoMessage, contact);
            SelectedContact = null;
        }
    }
}
