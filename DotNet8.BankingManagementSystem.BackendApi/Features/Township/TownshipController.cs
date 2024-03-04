using DotNet8.BankingManagementSystem.Models.TownShip;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Township
{
    [ApiController]
    [Route("api/[controller]")]
    public class TownshipController : BaseController
    {
        private readonly TownshipService _townshipService;
        public TownshipController(TownshipService townshipService)
        {
            _townshipService = townshipService;
        }

        #region Get Townships

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetTownShipByPagination(int pageNo, int pageSize)
        {
            try
            {
                var model = await _townshipService.GetTownShipList(pageNo, pageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion]

        #region Get Township

        [HttpGet("{townshipCode}")]
        public async Task<IActionResult> GetTownShipByCode(string townshipCode)
        {
            try
            {
                var model = await _townshipService.GetTownShipByCode(townshipCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Create Township

        [HttpPost]
        public async Task<IActionResult> CreateTownShip([FromBody] TownshipRequestModel requestModel)
        {
            try
            {
                var model = await _townshipService.CreateTownShip(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Update Township

        [HttpPut("{townshipCode}")]
        public async Task<IActionResult> UpdateTownship(string townshipCode, [FromBody] TownshipRequestModel requestModel)
        {
            try
            {
                var model = await _townshipService.UpdateTownship(townshipCode, requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Delete TownShip

        [HttpDelete("{townShipCode}")]
        public async Task<IActionResult> DeleteTownShip(string townShipCode)
        {
            try
            {
                var model = await _townshipService.DeleteTownShip(townShipCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        [HttpGet("StateCode/{stateCode}")]
        public async Task<IActionResult> GetTownShipByTownshipCode(string stateCode)
        {
            try
            {
                var model = await _townshipService.GetTownShipByStateCode(stateCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}