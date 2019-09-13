using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DockpadAPI.Models;
using Refit;

namespace DockpadAPI.Services
{
    public class APIService : IAPIService
    {
        public APIService()
        {
        }

        public async Task<ObservableCollection<Mood>> GetMoods()
        {
            var apiService = RestService.For<IAPIService>("https://dockpad.xyz");
            var moods = await apiService.GetMoods();
            return moods;
        }
    }
}
