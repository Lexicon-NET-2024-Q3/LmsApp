using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;

namespace LMS.Blazor.Client.Services;

public interface IApiService
{
    Task<IEnumerable<DemoDto>> CallApiAsync(string endpoint);
    Task<IEnumerable<CourseDto>> CallApiAsync2(string endpoint);
}
