using System.Collections.Generic;
using BlazorGettingStarted.Shared;

namespace BlazorGettingStarted.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
