using Refit;
using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Township;

namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IStateApi
    {
        [Get("/api/state/{pageNo}/{pageSize}")]
        Task<StateListResponseModel> GetStates(int pageNo, int pageSize);

        [Get("/api/state/{stateCode}")]
        Task<StateResponseModel> GetStateByCode(string stateCode);

        [Post("/api/state/createState")]
        Task<StateResponseModel> CreateState(StateRequestModel requestModel);

        [Put("/api/state/{stateCode}")]
        Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel);

        [Delete("/api/state/{stateCode}")]
        Task<StateResponseModel> DeleteState(string stateCode);
    }
    public interface ITownship
    {
        [Get("/api/township/{pageNo}/{pageSize}")]
        Task<TownshipResponseModel> GetTownships(int pageNo, int pageSize);

        [Get("/api/township/{townshipCode}")]
        Task<TownshipResponseModel> GetTownship(string townshipCode);

        [Post("/api/township/createTownship")]
        Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel);

        [Put("/api/township{townshipCode}")]
        Task<TownshipResponseModel> UpdateTownship(TownshipRequestModel requestModel);

        [Delete("/api/township/{townshipCode}")]
        Task<TownshipResponseModel> DeleteTownship(string townshipCode);
    }
}