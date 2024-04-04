namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.User;

public interface IUserApi
{
    [Get("/api/user/list/{pageNo}/{pageSize}")]
    Task<UserListResponseModel> GetUserList(int pageNo, int pageSize);

    [Get("/api/user/{userCode}")]
    Task<UserResponseModel> GetUserByCode(string userCode);

    [Post("/api/user/create")]
    Task<UserResponseModel> CreateUser(UserRequestModel requestModel); 
    
    [Put("/api/user/update")]
    Task<UserResponseModel> UpdateUser(UserRequestModel requestModel);

    [Delete("/api/user/delete{userCode}")]
    Task<UserResponseModel> DeleteUser(string userCode);
}