using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.Interfaces;
using ToDoApp.PL.WebPL.Models;

namespace ToDoApp.PL.WebPL.Controllers
{
    public class ToDoTaskStepController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromRoute] Guid id,
            [FromForm] string desc,
            [FromServices] IToDoTaskStepLogic stepLogic)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "ToDoTask", new { id });
            }

            await stepLogic.AddAsync(id, desc);

            return RedirectToAction("Details", "ToDoTask", new { id });
        }

        [HttpGet("{controller}/{action}/{id}/{taskId}")]
        public async Task<IActionResult> Delete(Guid id, Guid taskId,
           [FromServices] IToDoTaskStepLogic stepLogic,
           [FromServices] IToDoTaskLogic taskLogic)
        {
            await stepLogic.DeleteAsync(taskId, id);

            return RedirectToAction("Details", "ToDoTask", new { id = taskId });
        }

        [HttpGet("{controller}/{action}/{id}/{taskId}")]
        public async Task<IActionResult> Edit([FromRoute]Guid id, 
            [FromRoute] Guid taskId,
            [FromServices] IToDoTaskStepLogic stepLogic)
        {
            var step = await stepLogic.GetByIdAsync(id);
            EditNameVM VM = new EditNameVM { id = id, Title = step.Description };

            ViewBag.TaskId = taskId;
            return View(VM);
        }

        [HttpPost("{controller}/{action}/{id}/{taskId}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id,
            [FromRoute] Guid taskId,
            [FromForm] string desc,
            [FromServices] IToDoTaskStepLogic stepLogic)
        {
            await stepLogic.UpdateDescAsync(id, desc);

            return RedirectToAction("Details", "ToDoTask", new {id = taskId });
        }

        [HttpPost("{controller}/{action}/{id}/{taskId}")]
        public async Task<IActionResult> MarkCompleted([FromRoute] Guid id,
            [FromRoute] Guid taskId,
            [FromServices] IToDoTaskStepLogic stepLogic)
        {
            await stepLogic.ChangeCompletedStatusAsync(id);

            return RedirectToAction("Details", "ToDoTask", new { id = taskId });
        }
    }
}
