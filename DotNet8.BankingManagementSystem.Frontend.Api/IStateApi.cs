using Refit;
using DotNet8.BankingManagementSystem.Models.State;

namespace DotNet8.BankingManagementSystem.Frontend.Api
{
    public interface IStateApi
    {
        [Get("/api/state")]
        Task<StateListResponseModel> GetStates();

        [Get("/api/state/{pageNo}/{pageSize}")]
        Task<StateListResponseModel> GetStates(int pageNo, int pageSize);

        [Get("/api/state/{stateCode}")]
        Task<StateResponseModel> GetStateByCode(string stateCode);

        [Post("/api/state")]
        Task<StateResponseModel> CreateState(StateRequestModel requestModel);

        [Put("/api/state/{stateCode}")]
        Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel);

        [Delete("/api/state/{stateCode}")]
        Task<StateResponseModel> DeleteState(string stateCode);
    }
}