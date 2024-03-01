using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using DotNet8.BankingManagementSystem.Models.TownShip;
using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DotNet8.BankingManagementSystem.Models.Township;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Township
{
    public class TownshipService
    {
        private readonly AppDbContext _appDbContext;
        public TownshipService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Get TownShips

        public async Task<TownshipListResponceModel> GetTownShipList(int pageNo, int pageSize)
        {
            var query = _appDbContext.TblPlaceTownships
                .AsNoTracking();
            var result = await query
                .OrderByDescending(x => x.TownshipId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await query.CountAsync();

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

        #endregion

        #region Get Township 

        public async Task<TownshipResponseModel> GetTownShipByCode(string townshipCode)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item = await query
                .FirstOrDefaultAsync(x => x.TownshipCode == townshipCode);
            TownshipResponseModel model = new TownshipResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }

        #endregion

        #region Create Township

        public async Task<TownshipResponseModel> CreateTownShip(TownshipRequestModel requestModel)
        {
            var item = new TblPlaceTownship
            {
                TownshipCode = requestModel.TownshipCode,
                TownshipName = requestModel.TownshipName,
                StateCode = requestModel.StateCode,
            };
            await _appDbContext.TblPlaceTownships.AddAsync(item);
            var result = await _appDbContext.SaveChangesAsync();
            TownshipResponseModel model = new TownshipResponseModel
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Township has created successfully")
            };
            return model;
        }

        #endregion

        #region Update Township

        public async Task<TownshipResponseModel> UpdateTownship(string townshipCode, [FromBody] TownshipRequestModel requestModel)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item = await query
                .FirstOrDefaultAsync(x => x.TownshipCode == townshipCode);
            item!.TownshipCode = requestModel.TownshipCode;
            item.TownshipName = requestModel.TownshipName;
            item.StateCode = requestModel.StateCode;
            _appDbContext.Entry(item).State = EntityState.Modified;
            _appDbContext.TblPlaceTownships.Update(item);
            var result = await _appDbContext.SaveChangesAsync();

            TownshipResponseModel model = new TownshipResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Township has updated successfully.")
            };
            return model;
        }

        #endregion

        #region Delete Township

        public async Task<TownshipResponseModel> DeleteTownShip(string townShipCode)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.TownshipCode == townShipCode);
            _appDbContext.Entry(item!).State = EntityState.Deleted;
            _appDbContext.TblPlaceTownships.Remove(item!);
            var result = await _appDbContext.SaveChangesAsync();

            TownshipResponseModel model = new TownshipResponseModel
            {
                Response = new MessageResponseModel(true, "Township has deleted successfully.")
            };
            return model;
        }

        #endregion
    }
}