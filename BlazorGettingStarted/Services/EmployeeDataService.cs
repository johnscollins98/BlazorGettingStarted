using System.Text;
using System.Text.Json;
using BlazorGettingStarted.Shared;

namespace BlazorGettingStarted.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await httpClient.DeleteAsync($"api/employee/{employeeId}");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>
                (await httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            await httpClient.PutAsync("api/employee", employeeJson);
        }
    }
}