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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string CountryId { get; set; } = string.Empty;
        protected string JobCategoryId { get; set; } = string.Empty;

        // used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);
            if (employeeId == 0) // new employee is being created
            {
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }

        public async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0) // new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                Saved = true;
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
        }

        public void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        public async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);
            StatusClass = "alert-success";
            Message = "Employee deleted successfully.";
            Saved = true;
        }

        public void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}