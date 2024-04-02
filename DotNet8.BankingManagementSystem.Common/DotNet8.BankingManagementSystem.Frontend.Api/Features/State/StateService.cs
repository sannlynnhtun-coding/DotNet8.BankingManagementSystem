
namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.State;

public class StateService
{
    private readonly LocalStorageService _localStorageService;
    public StateService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    public async Task<StateListResponseModel> GetStates()
    {
        var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
        var result = query
            .OrderBy(x => x.StateName).ToList();

        var lst = result.Select(x => x.Change()).ToList();
        StateListResponseModel model = new StateListResponseModel()
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #region GetStateList

    public async Task<StateListResponseModel> GetStateList(int pageNo, int pageSize)
    {
        var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
        var result = query
            .OrderByDescending(x => x.StateId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var count = query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change()).ToList();

        StateListResponseModel model = new StateListResponseModel()
        {
            Data = lst,
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region GetStateByCode

    public async Task<StateResponseModel> GetStateByCode(string stateCode)
    {
        var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
        var item = query
            .FirstOrDefault(x => x.StateCode == stateCode);

        StateResponseModel model = new StateResponseModel()
        {
            Data = item!.Change(),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region Create State

    public async Task<StateResponseModel> CreateState(StateRequestModel requestModel)
    {
        StateResponseModel model = new StateResponseModel();
        try
        {
            var item = requestModel.Change();
            var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
            query ??= new List<TblPlaceState>();
            query.Add(item);
            await _localStorageService.SetList(EnumService.Tbl_State.GetKeyName(), query);
            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "State has created successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }
        return model;
    }

    #endregion

    #region Update State

    public async Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel requestModel)
    {
        StateResponseModel model = new StateResponseModel();
        try
        {
            var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
            var item = query
                .FirstOrDefault(x => x.StateCode == stateCode);
            if (item is null) throw new Exception("State is null.");
            var index = query.FindIndex(x => x.StateId == item.StateId);
            query[index] = item;
            await _localStorageService.SetList(EnumService.Tbl_State.GetKeyName(), query);
            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "State has updated successfully.");
        }
        catch (Exception ex)
        {
            model.Data = new();
            model.Response = new MessageResponseModel(false, ex);
        }
        return model;
    }

    #endregion

    #region Delete State

    public async Task<StateResponseModel> DeleteState(string stateCode)
    {
        StateResponseModel model = new StateResponseModel();
        try
        {
            var query = await _localStorageService.GetList<TblPlaceState>(EnumService.Tbl_State.GetKeyName());
            var item = query
               .FirstOrDefault(x => x.StateCode == stateCode);
            if (item is null) throw new Exception("State is null.");
            query.Remove(item);
            await _localStorageService.SetList(EnumService.Tbl_State.GetKeyName(), query);
            model.Response = new MessageResponseModel(true, "State has deleted successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }
        return model;
    }

    #endregion
}
