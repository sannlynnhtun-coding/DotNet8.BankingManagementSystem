using Refit;
using DotNet8.BankingManagementSystem.Models.TownShip;

namespace DotNet8.BankingManagementSystem.Frontend.Api
{
    public interface ITownshipApi
    {
        [Get("/api/Township/{pageNo}/{pageSize}")]
        Task<TownshipListResponceModel> GetTownships(int pageNo, int pageSize);

        [Get("/api/Township/{townshipCode}")]
        Task<TownshipResponseModel> GetTownship(string townshipCode);

        [Post("/api/Township")]
        Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel);

        [Put("/api/Township/{townshipCode}")]
        Task<TownshipResponseModel> UpdateTownship(string townshipCode, TownshipRequestModel requestModel);

        [Delete("/api/Township/{townshipCode}")]
        Task<TownshipResponseModel> DeleteTownship(string townshipCode);

        [Get("/api/Township/StateCode/{stateCode}")]
        Task<TownshipListResponceModel> GetTownShipByStateCode(string stateCode);
    }
}