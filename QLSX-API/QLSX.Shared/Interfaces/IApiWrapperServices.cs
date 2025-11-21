using QLSX.Shared.Data.Responses;
using QLSX.Shared.Interfaces;
using System.Threading.Tasks;

namespace QLSX.Shared.Interfaces
{
    public interface IApiWrapperServices
    {
        Task<ApiResponseBase<TResponse>> SendApiPostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class, IApiWrapperRequest
            where TResponse : class, IApiWrapperResponse;

        Task<ReportResponseBase<TResponse>> SendReportPostAsync<TRequest, TResponse>(TRequest request)
           where TRequest : class, IApiWrapperRequest
           where TResponse : class, IApiWrapperResponse;
        Task<ApiResponseBase<TResponse>> SendApiGetAsync<TRequest, TResponse>(TRequest request)
           where TRequest : class, IApiWrapperRequest
           where TResponse : class, IApiWrapperResponse;
    }
}
