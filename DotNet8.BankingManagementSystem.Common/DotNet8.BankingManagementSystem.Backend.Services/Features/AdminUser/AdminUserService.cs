namespace DotNet8.BankingManagementSystem.Backend.Services.Features.AdminUser;

public class AdminUserService
{
    private readonly AppDbContext _appDbContext;

    public AdminUserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #region GetAdminUsers

    public async Task<AdminUserListResponseModel> GetAdminUsers()
    {
        var query = _appDbContext.TblAdminUsers
            .AsNoTracking();

        var result = await query
            .OrderByDescending(x => x.AdminUserName)
            .ToListAsync();

        var lst = result.Select(x => x.Change()).ToList();

        AdminUserListResponseModel model = new AdminUserListResponseModel()
        {
            Data = lst,
            Response = new MessageResponseModel(true, "success")
        };
        return model;
    }

    #endregion
    
    #region GetAdminUsersList

    public async Task<AdminUserListResponseModel> GetAdminUsersList(int pageNo, int pageSize)
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
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "success")
        };
        return model;
    }

    #endregion

    #region GetAdminUserByCode

    public async Task<AdminUserResponseModel> GetAdminUserByCode(string adminUserCode)
    {
        var query = _appDbContext.TblAdminUsers
            .AsNoTracking();

        var item = await query
            .FirstOrDefaultAsync(x => x.AdminUserCode == adminUserCode);

        if (item == null)
        {
            throw new Exception("Invalid AdminUser");
        }

        AdminUserResponseModel model = new AdminUserResponseModel()
        {
            Data = item!.Change(),
            Response = new MessageResponseModel(true, "success")
        };
        return model;
    }

    #endregion

    #region CreateAdminUser

    public async Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel)
    {
        var item = requestModel.Change();
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

    #region UpdateAdminUser

    public async Task<AdminUserResponseModel> UpdateAdminUser(AdminUserRequestModel reqModel)
    {
        var query = _appDbContext.TblAdminUsers.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AdminUserCode == reqModel.AdminUserCode);

        if (item is null)
        {
            throw new Exception("Invalid Admin User");
        }

        item!.AdminUserName = reqModel.AdminUserName;
        item.AdminUserCode = reqModel.AdminUserCode;
        item.MobileNo = reqModel.MobileNo;
        item.UserRoleCode = reqModel.UserRoleCode;

        _appDbContext.Entry(item).State = EntityState.Modified;
        _appDbContext.TblAdminUsers.Update(item);
        var result = _appDbContext.SaveChangesAsync();

        AdminUserResponseModel model = new AdminUserResponseModel()
        {
            Data = item.Change(),
            Response = new MessageResponseModel(true, "Update AdminUser Successfully.")
        };
        return model;
    }

    #endregion
    
    #region DeleteAdminUser

    public async Task<AdminUserResponseModel> DeleteAdminUser(string adminUserCode)
    {
        var query = _appDbContext.TblAdminUsers.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AdminUserCode == adminUserCode);

        if (item == null)
        {
            throw new Exception("Invalid AdminUser");
        }

        _appDbContext.Entry(item!).State = EntityState.Deleted;
        _appDbContext.TblAdminUsers.Remove(item!);
        var result = await _appDbContext.SaveChangesAsync();

        AdminUserResponseModel model = new AdminUserResponseModel()
        {
            Response = new MessageResponseModel(true, "Deleted AdminUser Successfully")
        };
        return model;
    }

    #endregion
}