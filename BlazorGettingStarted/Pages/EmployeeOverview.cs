using BlazorGettingStarted.Services;
using BlazorGettingStarted.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorGettingStarted.Pages
{
    public partial class EmployeeOverview
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }
    }
}
