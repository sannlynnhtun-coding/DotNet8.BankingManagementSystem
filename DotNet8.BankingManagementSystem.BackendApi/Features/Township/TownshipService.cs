using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using DotNet8.BankingManagementSystem.Models.TownShip;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Township
{
    public class TownshipService
    {
        private readonly AppDbContext _appDbContext;
        //private readonly TownshipService _townshipService;
        public TownshipService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TownShipListResponceModel> GetTownShipList(int pageNo, int pageSize)
        {
            var query = _appDbContext.TblPlaceTownships
                .AsNoTracking();
            var result = await query
                .OrderByDescending(x => x.TownshipId)
                .ToListAsync();

            var count = await query.CountAsync();

            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;

            var lst = result.Select(x => x.Change())
                .ToList();

            TownShipListResponceModel model = new TownShipListResponceModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
    }
}
