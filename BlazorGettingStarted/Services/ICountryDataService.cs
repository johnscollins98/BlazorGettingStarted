using BlazorGettingStarted.Shared;

namespace BlazorGettingStarted.Services
{
    public interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int countryId);
    }
}