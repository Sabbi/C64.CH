using C64.Data;
using C64.Data.Entities;
using C64.Data.History;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    public class TempController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TempController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> AddHistory()
        {
            var production = unitOfWork.Productions.Find(p => p.ProductionId == 1).First();

            var handler = new ProductionHistoryHandler(unitOfWork, production, "1");

            handler.AddHistory(ProductionEditProperty.Name, "NewName2", HistoryStatus.ToApply);
            handler.AddHistory(ProductionEditProperty.Aka, "NewAka", HistoryStatus.ToApply);
            handler.Apply();

            await unitOfWork.Commit();
            return Content("Added");
        }

        public async Task<IActionResult> Undo()
        {
            var production = unitOfWork.Productions.Find(p => p.ProductionId == 993).First();
            var handler = new ProductionHistoryHandler(unitOfWork, production, "1");

            var toUndo = await unitOfWork.Productions.GetHistory(993);

            foreach (var item in toUndo.Where(p => p.HistoryProductionId >= 16).OrderByDescending(p => p.Applied))
            {
                handler.Undo(item);
            }

            await unitOfWork.Commit();

            return Content("undid");
        }
    }
}