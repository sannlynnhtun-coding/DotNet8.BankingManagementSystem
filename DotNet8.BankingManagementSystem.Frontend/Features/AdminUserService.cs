namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class AdminUserService
{
    private readonly LocalStorageService _localStorageService;

    public AdminUserService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<AdminUserListResponseModel> GetAdminUsers()
    {
        AdminUserListResponseModel model = new AdminUserListResponseModel();
        var query = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
        var result = query
            .OrderByDescending(x => x.AdminUserId)
            .ToList();
        
        model.Data = result.Select(x => x.Change()).ToList();
        model.Response = new MessageResponseModel(true, "Success");
        return model;
    }

    public async Task<AdminUserListResponseModel> GetAdminUserList(int pageNo, int pageSize)
    {
        AdminUserListResponseModel model = new AdminUserListResponseModel();
        var query = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
        var result = query
            .OrderByDescending(x => x.AdminUserId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var count = query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;

        model.Data = result.Select(x => x.Change()).ToList();
        model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount);
        model.Response = new MessageResponseModel(true, "Success");
        return model;
    }

    public async Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel)
    {
        AdminUserResponseModel model = new AdminUserResponseModel();
        try
        {
            var item = requestModel.Change();
            var query = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
            query ??= [];
            query.Add(item);
            await _localStorageService.SetList(EnumService.Tbl_AdminUser.ToString(), query);
            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "Admin has created successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }

        return model;
    }

    public async Task<AdminUserResponseModel> GetAdminUserByCode(string adminUserCode)
    {
        AdminUserResponseModel model = new AdminUserResponseModel();
        var lst = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
        lst ??= [];
        var item = lst.FirstOrDefault(x => x.AdminUserCode == adminUserCode);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        model.Data = item.Change();
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<AdminUserResponseModel> UpdateAdminUser(AdminUserRequestModel requestModel)
    {
        AdminUserResponseModel model = new AdminUserResponseModel();
        var lst = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
        var result = lst.FirstOrDefault(x => x.AdminUserCode == requestModel.AdminUserCode);
        var index = lst.FindIndex(x => result != null && x.AdminUserCode == result.AdminUserCode);
        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        result.AdminUserCode = requestModel.AdminUserCode;
        result.AdminUserName = requestModel.AdminUserName;
        result.MobileNo = requestModel.MobileNo;
        result.UserRoleCode = requestModel.UserRoleCode;
        lst[index] = result;

        await _localStorageService.SetList(EnumService.Tbl_AdminUser.ToString(), lst);
        model.Data = result.Change();
        model.Response = new MessageResponseModel(true, "Admin has been updated.");
        return model;
    }

    public async Task<AdminUserResponseModel> DeleteAdminUser(string adminUserCode)
    {
        AdminUserResponseModel model = new AdminUserResponseModel();
        var lst = await _localStorageService.GetList<TblAdminUser>(EnumService.Tbl_AdminUser.ToString());
        lst ??= [];
        var item = lst.FirstOrDefault(x => x.AdminUserCode == adminUserCode);
        if (item == null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _localStorageService.SetList(EnumService.Tbl_AdminUser.ToString(), lst);
        model.Response = new MessageResponseModel(true, "Admin has been removed.");
        return model;
    }
}
