using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.State
{
    public class StateService
    {
        private readonly AppDbContext _appDbContext;

        public StateService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region GetStateList
        public async Task<StateListResponseModel> GetStateList(int pageNo, int pageSize)
        {
            var query = _appDbContext.TblPlaceStates
                .AsNoTracking();
            var result = await query
                .OrderByDescending(x => x.StateId)
                .ToListAsync();
            var count = await query.CountAsync();
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
            var query = _appDbContext.TblPlaceStates.AsNoTracking();
            var item = await query
                .FirstOrDefaultAsync(x => x.StateCode == stateCode);

            StateResponseModel model = new StateResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion

        #region CreateState
        public async Task<StateResponseModel> CreateState([FromBody] StateRequestModel requestModel)
        {
            var item = new TblPlaceState()
            {
                StateCode = requestModel.StateCode,
                StateName = requestModel.StateName,
            };
            await _appDbContext.TblPlaceStates.AddAsync(item);
            var result = await _appDbContext.SaveChangesAsync();

            StateResponseModel model = new StateResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "State has created successfully.")
            };
            return model;
        }
        #endregion

        #region UpdateState
        public async Task<StateResponseModel> UpdateState(string stateCode, [FromBody] StateRequestModel requestModel)
        {
            var query = _appDbContext.TblPlaceStates.AsNoTracking();
            var item = await query
                .FirstOrDefaultAsync(x => x.StateCode == stateCode);
            item!.StateCode = requestModel.StateCode;
            item.StateName = requestModel.StateName;
            _appDbContext.Entry(item).State = EntityState.Modified;
            _appDbContext.TblPlaceStates.Update(item);
            var result = await _appDbContext.SaveChangesAsync();

            StateResponseModel model = new StateResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "State has updated successfully.")
            };
            return model;
        }
        #endregion

        #region DeleteState
        public async Task<StateResponseModel> DeleteState(string stateCode)
        {
            var query = _appDbContext.TblPlaceStates.AsNoTracking();
            var item = await query
                .FirstOrDefaultAsync(x => x.StateCode == stateCode);
            _appDbContext.Entry(item!).State = EntityState.Deleted;
            _appDbContext.TblPlaceStates.Remove(item!);
            var result = await _appDbContext.SaveChangesAsync();

            StateResponseModel model = new StateResponseModel()
            {
                Response = new MessageResponseModel(true, "State has deleted successfully.")
            };
            return model;
        }
        #endregion
    }
}