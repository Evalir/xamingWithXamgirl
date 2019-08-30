using System;
using System.ComponentModel;
using System.Windows.Input;
using MVMMLogin.Models;
using Xamarin.Forms;

namespace MVMMLogin.Views
{
    class MyPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LogInCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string LoginErrors { get; set; }
        public User User { get; set; }

        public MyPageViewModel()
        {
            User = new User();
            LogInCommand = new Command(async () =>
            {

                if (String.IsNullOrEmpty(User.Username))
                {
                    LoginErrors = "Username cannot be empty!";
                }
                else if (String.IsNullOrEmpty(User.Password))
                {
                    LoginErrors = "Password cannot be empty!";
                }
                else
                {
                    // Dirty trick to remove login page from view, not good?
                    App.Current.MainPage = new NavigationPage(new MyTabbedPage());
                    await App.Current.MainPage.Navigation.PopToRootAsync(true);
                }
            });

            RegisterCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new MySignUpPage());
            });
        }


    }
}
