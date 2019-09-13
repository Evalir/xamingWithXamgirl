using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using DockpadAPI.Models;
using DockpadAPI.Services;

namespace DockpadAPI.ViewModels
{
    public class MyPageViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Mood> Moods { get; set; }
        APIService Service { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        async void fetch()
        {
            var moods = await Service.GetMoods();
            Moods = moods;
            var a = 2;
        }

        public MyPageViewModel()
        {
            Moods = new ObservableCollection<Mood>();
            Service = new APIService();
            fetch();
        }
    }
}
