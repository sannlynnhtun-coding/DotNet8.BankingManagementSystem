namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class UserService
{
    private readonly LocalStorageService _localStorageService;

    public UserService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<UserListResponseModel> GetUserList(int pageNo = 1, int pageSize = 10)
    {
        UserListResponseModel model = new UserListResponseModel();
        var query = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.ToString());
        if (pageNo == 0)
        {
            model = new UserListResponseModel()
            {
                Data = query.Select(x => x.Change()).ToList(),
                Response = new MessageResponseModel(true, "Success")
            };
        }
        else
        {
            query ??= [];
            var result = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var count = query.Count();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;

            model = new UserListResponseModel()
            {
                Data = result.Select(x => x.Change()).ToList(),
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
        }
        return model;
    }

    public async Task<UserResponseModel> CreateUser(UserRequestModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        requestModel.UserCode = await GenerateUniqueUserCode();
        var userModel = new TblUser()
        {
            UserName = requestModel.UserName,
            FullName = requestModel.FullName,
            Email = requestModel.Email,
            Address = requestModel.Address,
            MobileNo = requestModel.MobileNo,
            Nrc = requestModel.Nrc,
            StateCode = requestModel.StateCode,
            TownshipCode = requestModel.TownshipCode
        };
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= [];
        lst.Add(userModel);
        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Response = new MessageResponseModel(true, "User has been registered successfully.");
        return model;
    }

    public async Task CreateUsers(List<UserRequestModel> lst)
    {
        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
    }

    public async Task<UserResponseModel> GetUserByCode(string userCode)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= [];
        var item = lst.FirstOrDefault(x => x.UserCode == userCode);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        model.Data = item.Change();
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<UserResponseModel> UpdateUser(UserRequestModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        var result = lst.FirstOrDefault(x => x.UserCode == requestModel.UserCode);
        var index = lst.FindIndex(x => result != null && x.UserCode == result.UserCode);
        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        result.UserCode = requestModel.UserCode;
        result.UserName = requestModel.UserName;
        result.FullName = requestModel.FullName;
        result.Email = requestModel.Email;
        result.Nrc = requestModel.Nrc;
        result.MobileNo = requestModel.MobileNo;
        result.Address = requestModel.Address;
        result.StateCode = requestModel.StateCode;
        result.TownshipCode = requestModel.TownshipCode;
        lst[index] = result;

        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Data = result.Change();
        model.Response = new MessageResponseModel(true, "User has been updated.");
        return model;
    }

    public async Task<UserResponseModel> DeleteUser(string userCode)
    {
        UserResponseModel model = new UserResponseModel();
        var lst = await _localStorageService.GetList<TblUser>(EnumService.Tbl_User.GetKeyName());
        lst ??= [];
        var item = lst.FirstOrDefault(x => x.UserCode == userCode);
        if (item == null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _localStorageService.SetList(EnumService.Tbl_User.GetKeyName(), lst);
        model.Response = new MessageResponseModel(true, "User has been removed.");
        return model;
    }

    private async Task<string> GenerateUniqueUserCode()
    {
        string latestUserCode = "AB000";
        // Simple generation logic for demo purposes
        return latestUserCode; 
    }
}
