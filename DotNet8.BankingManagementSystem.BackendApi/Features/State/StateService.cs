using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.State;
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

            var lst = result.Select(x => new StateModel()
            {
                StateCode = x.StateCode,
                StateName = x.StateName,
                StateId = x.StateId,
            }).ToList();

            StateListResponseModel model = new StateListResponseModel()
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
    }
}
