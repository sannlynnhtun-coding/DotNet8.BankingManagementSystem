using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.AdminUser;
using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.AdminUser
{
    public class AdminUserService
    {
        private readonly AppDbContext _appDbContext;

        public AdminUserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region GetAdminUsers
        public async Task<AdminUserListResponseModel> GetAdminUsers(int pageNo, int pageSize)
        {
            var query = _appDbContext.TblAdminUsers
                .AsNoTracking();

            var result = await query
                .OrderByDescending(x => x.AdminUserName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();
            int pageCount = count / pageSize;

            if (count % pageSize > 0) pageCount++;

            var lst = result.Select(x => x.Change()).ToList();

            AdminUserListResponseModel model = new AdminUserListResponseModel()
            {
                Data = lst,
                Response = new MessageResponseModel(true, "success")
            };
            return model;
        }
        #endregion

        #region GetAdminUserByAdminUserCode
        public async Task<AdminUserResponseModel> GetAdminUser(string AdminUserCode)
        {
            var query = _appDbContext.TblAdminUsers
                .AsNoTracking();

            var item = await query
                .FirstOrDefaultAsync(x => x.AdminUserCode == AdminUserCode);

            AdminUserResponseModel model = new AdminUserResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "success")
            };
            return model;
        }
        #endregion

        #region CreateAdminUser
        public async Task<AdminUserRequestModel> CreateAdminUser(AdminUserRequestModel requestModel)
        {
            var item = new TblAdminUser
            {
                AdminUserCode = requestModel.AdminUserCode,
                AdminUserName = requestModel.AdminUserName,
                MobileNo = requestModel.MobileNo,
                UserRoleCode = requestModel.UserRoleCode
            };

            await _appDbContext.AddAsync(item);
            var result = await _appDbContext.SaveChangesAsync();

            AdminUserResponseModel model = new AdminUserResponseModel
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "AdminUser has create successfully")
            };
            return model;
        }
        #endregion
    }
}
