using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ListViewPractice.Models;
using Xamarin.Forms;

namespace ListViewPractice.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        User _selectedUser;

        public event PropertyChangedEventHandler PropertyChanged;

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                ItemTapped(_selectedUser);
            }
        }

        public ObservableCollection<User> Users { get; set; }
        public MainPageViewModel()
        {
            Users = new ObservableCollection<User>();
            Users.Add(new User()
            {
                Name = "Enrique",
                Photo = "https://picsum.photos/128",
                Active = true
            });
            Users.Add(new User()
            {
                Name = "Alberto",
                Photo = "https://picsum.photos/128",
                Active = false
            });
            Users.Add(new User()
            {
                Name = "Daniel",
                Photo = "https://picsum.photos/128",
                Active = false
            });
            Users.Add(new User()
            {
                Name = "Luismarc",
                Photo = "https://picsum.photos/128",
                Active = false
            });
        }

        private async void ItemTapped(User user)
        {
            await App.Current.MainPage.DisplayAlert("User Alert!", $"Tapped {user.Name}.", "Ok");
        }
    }
}
