using C64.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    public class StatsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> UpdateProductionStats()
        {
            _unitOfWork.Productions.UpdateProductionStats();
            await _unitOfWork.Commit();
            return Content("Done");
        }

        public async Task<IActionResult> UpdateGroupStats()
        {
            _unitOfWork.Groups.UpdateGroupStats();
            await _unitOfWork.Commit();
            return Content("Done");
        }
    }
}