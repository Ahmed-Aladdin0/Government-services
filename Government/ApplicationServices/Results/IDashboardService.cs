using Government.Contracts.Dashboard;
using Government.Contracts.DashBoard;
using Government.Contracts.Services;

namespace Government.ApplicationServices.Results
{
    public interface IDashboardService
    {

        Task<Result<Overview>> GetOverviewAsync();
        Task<Result<RequestStatisticsDto>> GetRequestStatisticsAsync();
        Task<Result<IEnumerable<MonthlyCountDto>>> GetRequestStatisticsPerMonthAsync();
        Task<Result<ServiceStatisticsDto>> GetServiceStatisticsAsync();
        Task<Result<IEnumerable<MostRequested>>> GetMostRequestedServicesAsync();

    }
}