using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using MVMMLogin.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace MVMMLogin.ViewModels
{
    public class NewContactPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public Contact Contact { get; set; }
        public string ProfilePicture { get; set; } = "camera";
        bool SaveOrUpdate { get; set; }
        public ICommand ChoosePictureCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public NewContactPageViewModel(Contact SelectedContact = null)
        {
            if (SelectedContact == null)
                Contact = new Contact();
            else
                Contact = SelectedContact;

            MessagingCenter.Subscribe<ContactsPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage, (sender, ShouldSaveOrUpdate) =>
            {
                SaveOrUpdate = ShouldSaveOrUpdate;
                MessagingCenter.Unsubscribe<ContactsPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage);
            });

            MessagingCenter.Subscribe<ContactInfoPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage, (sender, ShouldSaveOrUpdate) =>
            {
                SaveOrUpdate = ShouldSaveOrUpdate;
                MessagingCenter.Unsubscribe<ContactInfoPageViewModel, bool>(this, new Constants().SaveOrUpdateToDBMessage);
            });

            ChoosePictureCommand = new Command(async () =>
            {
                var option = await App.Current.MainPage.DisplayActionSheet("Photo", "Cancel", null, "Take Photo", "Choose from Gallery");
                if (option == "Take Photo")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await App.Current.MainPage.DisplayAlert("No Camera", "No camera available.", "OK");
                        return;
                    }

                    var pic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg"
                    });

                    if (pic == null)
                        return;
                    Contact.PhotoPath = pic.Path;
                    ProfilePicture = pic.Path;
                }
                else if (option == "Choose from Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    var pic = await CrossMedia.Current.PickPhotoAsync();
                    if (pic == null)
                        return;
                    Contact.PhotoPath = pic.Path;
                    ProfilePicture = pic.Path;
                }
            });

            SaveCommand = new Command(async () =>
            {
                if (SaveOrUpdate)
                {
                    // Save to DB
                    Debug.WriteLine(App.CurrentUser);
                    Contact.ParentUserEmail = App.CurrentUser.Email;
                    await App.Database.SaveContactAsync(Contact);
                }
                else
                {
                    // Update to DB
                    Contact.ParentUserEmail = App.CurrentUser.Email;
                    await App.Database.UpdateContactAsync(Contact);
                }
                await App.Current.MainPage.Navigation.PopToRootAsync();

            });
        }
    }
}
