using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Services.Dtos;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPoolService;

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bonusPoolService.GetEmployeesAsync());
        }

        [HttpPost("CalculateBonus")]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            return Ok(await _bonusPoolService.CalculateAsync(
                request.TotalBonusPoolAmount,
                request.SelectedEmployeeId));
        }

    }
}
