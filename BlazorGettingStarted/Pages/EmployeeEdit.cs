using BlazorGettingStarted.Services;
using BlazorGettingStarted.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorGettingStarted.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public ICountryDataService CountryDataService { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string CountryId { get; set; } = string.Empty;
        protected string JobCategoryId { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));

            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryService.GetAllJobCategories()).ToList();

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }
    }
}