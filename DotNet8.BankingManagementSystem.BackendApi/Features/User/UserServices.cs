using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.User
{
    public class UserServices
    {
        private readonly AppDbContext _appDbContext;

        public UserServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Get Users

        public async Task<UserListResponseModel> GetUserList(int pageNo, int pageSize)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var result = await query
                .OrderByDescending(x => x.UserId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;
            var lst = result.Select(x => x.Change()).ToList();

            UserListResponseModel model = new UserListResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }

        #endregion

        #region Get User

        public async Task<UserResponseModel> GetUserByCode(string userCode)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.UserCode == userCode);
            UserResponseModel model = new UserResponseModel
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }

        #endregion

        #region Create User

        public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel)
        {
            var item = requestModel.Change();//need extra changes here
            await _appDbContext.TblUsers.AddAsync(item);
            var result = await _appDbContext.SaveChangesAsync();

            UserResponseModel model = new UserResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "User has created successfully.")
            };
            return model;
        }

        #endregion

        #region Update User info

        public async Task<UserResponseModel> UpdateUserInfo(string userCode, UserRequestModel requestModel)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.UserCode == userCode);
            if (item is null)
            {
                throw new Exception("User is null.");
            }

            item.UserCode = requestModel.UserCode;
            item.UserName = requestModel.UserName;
            item.FullName = requestModel.FullName;
            item.Nrc = requestModel.Nrc;
            item.Address = requestModel.Address;
            item.Email = requestModel.Email;

            _appDbContext.Entry(item).State = EntityState.Modified;
            _appDbContext.TblUsers.Update(item);
            var result = await _appDbContext.SaveChangesAsync();

            UserResponseModel model = new UserResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "User information have updated successfully.")
            };
            return model;
        }

        #endregion 

        #region Delete User

        public async Task<UserResponseModel> DeleteUser(string userCode)
        {
            var query = _appDbContext.TblUsers.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.UserCode == userCode);
            if (item is null)
            {
                throw new Exception("User is null.");
            }
            UserResponseModel model = new UserResponseModel
            {
                Response = new MessageResponseModel(true, "User has deleted successfully.")
            };
            return model;
        }

        #endregion
    }
}
