using Refit;
using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Models.State;

namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IStateApi
    {
        [Get("/api/state/{pageNo}/{pageSize}")]
        Task<StateListResponseModel> GetState(int pageNo, int pageSize);

        [Get("/api/state/{stateCode}")]
        Task<StateResponseModel> GetStateByCode(string stateCode);

        [Post("/api/state/createState")]
        Task<StateResponseModel> CreateState(StateRequestModel requestModel);

        [Put("/api/state/{stateCode}")]
        Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel);

        [Delete("/api/state/{stateCode}")]
        Task<StateResponseModel> DeleteState(string stateCode);
    }
}