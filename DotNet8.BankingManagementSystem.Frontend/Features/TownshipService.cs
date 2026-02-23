namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class TownshipService
{
    private readonly LocalStorageService _localStorageService;

    public TownshipService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<TownshipListResponceModel> GetTownShipList(int pageNo, int pageSize)
    {
        var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
        var result = query
            .OrderByDescending(x => x.TownshipId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var count = query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change())
            .ToList();

        TownshipListResponceModel model = new TownshipListResponceModel()
        {
            Data = lst,
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    public async Task<TownshipResponseModel> GetTownShipByCode(string townshipCode)
    {
        TownshipResponseModel model = new TownshipResponseModel();
        try
        {
            var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
            var item = query
                .FirstOrDefault(x => x.TownshipCode == townshipCode);
            if (item == null)
            {
                throw new Exception("TownshipCode doesn't exist.");
            }
            model.Data = item!.Change();
            model.Response = new MessageResponseModel(true, "Success");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }

        return model;
    }

    public async Task<TownshipResponseModel> CreateTownship(TownshipRequestModel requestModel)
    {
        TownshipResponseModel model = new TownshipResponseModel();
        try
        {
            var item = requestModel.Change();
            var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
            query ??= [];
            query.Add(item);
            await _localStorageService.SetList(EnumService.Tbl_Township.GetKeyName(), query);

            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "Township has created successfully");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }

        return model;
    }

    public async Task CreateTownships(List<TownshipRequestModel> lst)
    {
        await _localStorageService.SetList(EnumService.Tbl_Township.GetKeyName(), lst);
    }

    public async Task<TownshipResponseModel> UpdateTownship(string townshipCode, TownshipRequestModel requestModel)
    {
        TownshipResponseModel model = new TownshipResponseModel();
        try
        {
            var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
            var item = query
                .FirstOrDefault(x => x.TownshipCode == townshipCode);
            if (item == null)
            {
                throw new Exception("TownshipCode doesn't exist.");
            }
            var index = query.FindIndex(x => x.StateCode == townshipCode);
            query[index] = item;
            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "Township has updated successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }

        return model;
    }

    public async Task<TownshipResponseModel> DeleteTownship(string townShipCode)
    {
        TownshipResponseModel model = new TownshipResponseModel();
        try
        {
            var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
            var item = query.FirstOrDefault(x => x.TownshipCode == townShipCode);
            if (item == null)
            {
                throw new Exception("TownshipCode doesn't exist.");
            }
            query.Remove(item);
            await _localStorageService.SetList(EnumService.Tbl_Township.GetKeyName(), query);
            model.Response = new MessageResponseModel(true, "Township has deleted successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }
        return model;
    }

    public async Task<TownshipListResponceModel> GetTownShipByStateCode(string stateCode)
    {
        var query = await _localStorageService.GetList<TblPlaceTownship>(EnumService.Tbl_Township.GetKeyName());
        var lst = query
            .Where(x => x.StateCode == stateCode).ToList();
        TownshipListResponceModel model = new TownshipListResponceModel()
        {
            Data = lst.Select(x => x.Change()).ToList(),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }
}
