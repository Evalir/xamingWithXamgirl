using System;
using System.ComponentModel;
using System.Windows.Input;
using MVMMLogin.Models;
using Xamarin.Forms;

namespace MVMMLogin.ViewModels
{
    public class MySignUpPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RegisterCommand { get; set; }
        public string SignupErrors { get; set; }
        public string PasswordVerifier { get; set; }
        public User User { get; set; }

        public MySignUpPageViewModel()
        {
            User = new User();
            RegisterCommand = new Command(async () =>
            {
                if (String.IsNullOrEmpty(User.Email))
                {
                    SignupErrors = "Email cannot be empty!";
                }
                else if (String.IsNullOrEmpty(User.Username))
                {
                    SignupErrors = "Username cannot be empty!";
                }
                else if (String.IsNullOrEmpty(User.Password))
                {
                    SignupErrors = "Password cannot be empty!";
                }
                else if (User.Password != PasswordVerifier)
                {
                    SignupErrors = "Passwords do not match!";
                }
                else
                {
                    await App.Current.MainPage.Navigation.PopToRootAsync(true);
                }
            });
        }
    }
}
