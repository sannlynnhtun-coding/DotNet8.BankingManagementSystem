namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Township;

public interface ITownshipApi
{
    [Get("/api/Township/{pageNo}/{pageSize}")]
    Task<TownshipListResponceModel> GetTownShipList(int pageNo, int pageSize);

    [Get("/api/Township/{townshipCode}")]
    Task<TownshipResponseModel> GetTownShipByCode(string townshipCode);

    [Post("/api/Township")]
    Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel);

    [Put("/api/Township/{townshipCode}")]
    Task<TownshipResponseModel> UpdateTownship(string townshipCode, TownshipRequestModel requestModel);

    [Delete("/api/Township/{townshipCode}")]
    Task<TownshipResponseModel> DeleteTownship(string townshipCode);

    [Get("/api/Township/StateCode/{stateCode}")]
    Task<TownshipListResponceModel> GetTownShipByStateCode(string stateCode);
}