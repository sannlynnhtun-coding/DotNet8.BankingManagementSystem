using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.User;

public interface IUserApi
{
    [Get("/api/user/{pageNo}/{pageSize}")]
    Task<UserListResponseModel> GetUserList(int pageNo, int pageSize);

    [Get("/api/user/{userCode}")]
    Task<UserResponseModel> GetUserByCode(string userCode);

    [Post("/api/user")]
    Task<UserResponseModel> CreateUser(UserRequestModel requestModel); 
    
    [Put("/api/user/{userCode}")]
    Task<UserResponseModel> UpdateUser(UserRequestModel requestModel);

    [Delete("/api/user/{userCode}")]
    Task<UserResponseModel> DeleteUser(string userCode);
}