using Refit;
using DotNet8.BankingManagementSystem.Models.Township;
using DotNet8.BankingManagementSystem.Models.TownShip;

namespace DotNet8.BankingManagementSystem.App.Api
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
    }
}