using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.State
{
    public class StateController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public StateController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("stateList")]
        public IActionResult GetStateList()
        {
            try
            {
                var stateList = _appDbContext.TblPlaceStates.OrderByDescending(x => x.StateId).AsNoTracking().ToList();
                return Ok(new MessageResponseListModel()
                {
                    IsSuccess = true,
                    Message = "Success",
                    StateList = stateList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetStateByPagination(int pageNo, int pageSize)
        {
            try
            {
                List<TblPlaceState> stateList = _appDbContext
                    .TblPlaceStates
                    .OrderByDescending(a => a.StateId)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return Ok(new MessageResponseListModel
                {
                    IsSuccess = true,
                    Message = "Success",
                    PageNo = pageNo,
                    PageSize = pageSize,
                    StateList = stateList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{stateCode}")]
        public async Task<IActionResult> GetStateByCode(string stateCode)
        {
            try
            {
                var stateData = await _appDbContext.TblPlaceStates.FirstOrDefaultAsync(x => x.StateCode == stateCode);
                return Ok(new MessageResponseModel()
                {
                    IsSuccess = true,
                    Message = "Success",
                    State = stateData!
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createState")]
        public async Task<IActionResult> CreateState([FromBody] StateRequestModel requestModel)
        {
            try
            {
                var state = new TblPlaceState
                {
                    StateCode = requestModel.StateCode,
                    StateName = requestModel.StateName
                };
                await _appDbContext.TblPlaceStates.AddAsync(state);
                var result = await _appDbContext.SaveChangesAsync();
                return Ok(new MessageResponseModel()
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "State created successfully" : "State creating failed.",
                    State = state
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{stateCode}")]
        public async Task<IActionResult> UpdateState(string stateCode, [FromBody] StateRequestModel requestModel)
        {
            try
            {
                var existingState = await _appDbContext.TblPlaceStates.FirstOrDefaultAsync(s => s.StateCode == stateCode);
                if (existingState == null) return NotFound();

                existingState.StateCode = requestModel.StateCode;
                existingState.StateName = requestModel.StateName;

                _appDbContext.TblPlaceStates.Update(existingState);
                var result = await _appDbContext.SaveChangesAsync();
                return Ok(new MessageResponseModel()
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "State created successfully" : "State creating failed.",
                    State = existingState
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating the state: {ex.Message}");
            }
        }

        [HttpDelete("{stateCode}")]
        public async Task<IActionResult> DeleteState(string stateCode)
        {
            try
            {
                var existingState = await _appDbContext.TblPlaceStates.FirstOrDefaultAsync(s => s.StateCode == stateCode);
                if (existingState == null) return NotFound();

                _appDbContext.TblPlaceStates.Remove(existingState);
                var result = await _appDbContext.SaveChangesAsync();
                return Ok(new MessageResponseModel()
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "State deleted successfully" : "State deleting failed.",
                    State = existingState
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting the state: {ex.Message}");
            }
        }

    }
}
