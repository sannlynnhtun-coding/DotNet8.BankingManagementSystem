using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.State
{
    public class StateController : BaseController
    {
        private readonly StateService _stateService;

        public StateController(StateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetStateByPagination(int pageNo, int pageSize)
        {
            try
            {
                var model = await _stateService.GetStateList(pageNo, pageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet("{stateCode}")]
        //public async Task<IActionResult> GetStateByCode(string stateCode)
        //{
        //    try
        //    {
        //        var stateData = await _appDbContext.TblPlaceStates
        //            .FirstOrDefaultAsync(x => x.StateCode == stateCode);
        //        return Ok(new MessageResponseModel()
        //        {
        //            IsSuccess = true,
        //            Message = "Success",
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //[HttpPost("createState")]
        //public async Task<IActionResult> CreateState([FromBody] StateRequestModel requestModel)
        //{
        //    try
        //    {
        //        var state = new TblPlaceState
        //        {
        //            StateCode = requestModel.StateCode,
        //            StateName = requestModel.StateName
        //        };
        //        await _appDbContext.TblPlaceStates.AddAsync(state);
        //        var result = await _appDbContext.SaveChangesAsync();
        //        return Ok(new MessageResponseModel()
        //        {
        //            IsSuccess = result > 0,
        //            Message = result > 0 ? "State created successfully" : "State creating failed.",
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //[HttpPut("{stateCode}")]
        //public async Task<IActionResult> UpdateState(string stateCode, [FromBody] StateRequestModel requestModel)
        //{
        //    try
        //    {
        //        var item = await _appDbContext.TblPlaceStates
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(s => s.StateCode == stateCode);
        //        if (item == null) return NotFound();

        //        item.StateCode = requestModel.StateCode;
        //        item.StateName = requestModel.StateName;

        //        _appDbContext.Entry(item).State = EntityState.Modified;
        //        _appDbContext.TblPlaceStates.Update(item);
        //        var result = await _appDbContext.SaveChangesAsync();
        //        return Ok(new MessageResponseModel()
        //        {
        //            IsSuccess = result > 0,
        //            Message = result > 0 ? "State created successfully" : "State creating failed.",
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //[HttpDelete("{stateCode}")]
        //public async Task<IActionResult> DeleteState(string stateCode)
        //{
        //    try
        //    {
        //        var item = await _appDbContext.TblPlaceStates
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(s => s.StateCode == stateCode);
        //        if (item == null) return NotFound();

        //        _appDbContext.Entry(item).State = EntityState.Deleted;
        //        _appDbContext.TblPlaceStates.Remove(item);
        //        var result = await _appDbContext.SaveChangesAsync();
        //        return Ok(new MessageResponseModel()
        //        {
        //            IsSuccess = result > 0,
        //            Message = result > 0 ? "State deleted successfully" : "State deleting failed.",
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
