using DotNet8.BankingManagementSystem.BackendApi.Features.State;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Township
{
    public class TownshipController : BaseController
    {
        private readonly TownshipService _townshipService;

        public TownshipController(TownshipService townshipService)
        {
            _townshipService = townshipService;
        }

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
    }
}
