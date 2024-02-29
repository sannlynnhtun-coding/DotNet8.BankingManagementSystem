using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Users
{
    public class UserServices
    {
        private readonly AppDbContext _appDbContext;

        public UserServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

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
    }
}
