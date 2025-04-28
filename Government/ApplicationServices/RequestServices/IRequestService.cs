using Government.Contracts.Request;
using Government.Contracts.Request.Submiting;
using SurvayBasket.Abstractions;

namespace Government.ApplicationServices.RequestServices
{
    public interface IRequestService
    {
   
        Task<Result<PaginationList<RequestsDetails>>> GetAllRequests(RequestQueryParameters parameters,CancellationToken cancellationToken);

        Task<Result<IEnumerable<RequestsDetailstoUser>>> GetAllUserRequests(CancellationToken cancellationToken);

        Task<Result<RequestDetailsResponse>> GetUserRequestAsync(int requestId, CancellationToken cancellationToken); // Search
       
        Task<Result<IEnumerable<RequestsDetailstoUser>>> GetUserequestsByStatusAsync(string requestStatus, CancellationToken cancellationToken);  //filter

        Task<Result <SubmitResponseDto>> SubmitRequestAsync(SubmitRequestDto requestDto, CancellationToken cancellationToken);




    }
}
// Task<Result<RequestDetailsResponse>> GetRequestByIdAsync(int requestId, CancellationToken cancellationToken);
// Task<Result> UpdateRequestAsync(int requestId,IEnumerable< UpdateRequest> requestDto, CancellationToken cancellationToken);
