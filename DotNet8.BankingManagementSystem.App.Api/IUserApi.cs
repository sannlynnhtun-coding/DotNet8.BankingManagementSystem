using DotNet8.BankingManagementSystem.Models.Users;
using Refit;


namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IUserApi
    {
        [Get("/api/user/{pageNo}/{pageSize}")]
        Task<UserListResponseModel> GetStates(int pageNo, int pageSize);

        [Get("/api/user/{userCode}")]
        Task<UserResponseModel> GetUserByCode(string userCode);

        [Post("/api/user")]
        Task<UserResponseModel> CreateUser(UserRequestModel requestModel); 
    
        [Put("/api/user/{userCode}")]
        Task<UserResponseModel> UpdateUser(string userCode,UserRequestModel requestModel);

        [Delete("/api/user/{userCode}")]
        Task<UserResponseModel> DeleteUser(string userCode);

    }
}
