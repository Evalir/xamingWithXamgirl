using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DockpadAPI.Models;
using Refit;
namespace DockpadAPI.Services
{
    // Forgive me father, for I have sinned
    [Headers("Authorization: Token d49c14415c1340c41cf5c17b350d67186af31b02")]
    public interface IAPIService
    {
        [Get("/moods/")]
        Task<ObservableCollection<Mood>> GetMoods();
    }
}
