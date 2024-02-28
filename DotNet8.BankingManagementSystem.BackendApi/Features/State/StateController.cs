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

        [HttpGet("{stateCode}")]
        public async Task<IActionResult> GetStateByCode(string stateCode)
        {
            try
            {
                var model = await _stateService.GetStateByCode(stateCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost("createState")]
        public async Task<IActionResult> CreateState([FromBody] StateRequestModel requestModel)
        {
            try
            {
                var model = await _stateService.CreateState(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut("{stateCode}")]
        public async Task<IActionResult> UpdateState(string stateCode, [FromBody] StateRequestModel requestModel)
        {
            try
            {
                var model = await _stateService.UpdateState(stateCode, requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete("{stateCode}")]
        public async Task<IActionResult> DeleteState(string stateCode)
        {
            try
            {
                var model = await _stateService.DeleteState(stateCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
