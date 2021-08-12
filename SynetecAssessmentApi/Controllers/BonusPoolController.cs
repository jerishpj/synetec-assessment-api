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
        private readonly IBonusPoolService bonusPoolService;

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            this.bonusPoolService = bonusPoolService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.bonusPoolService.GetEmployeesAsync());
        }

        [HttpPost("CalculateBonus")]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            return Ok(await this.bonusPoolService.CalculateAsync(
                request.TotalBonusPoolAmount,
                request.SelectedEmployeeId));
        }

    }
}
