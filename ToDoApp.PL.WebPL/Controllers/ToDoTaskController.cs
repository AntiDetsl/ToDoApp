using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ToDoApp.BLL.Interfaces;
using ToDoApp.PL.WebPL.Models.ToDoTaskVM;
using ToDoApp.Entities;
using ToDoApp.PL.WebPL.Models.ToDoList;
using System.Collections.Generic;
using ToDoApp.PL.WebPL.Models;

namespace ToDoApp.PL.WebPL.Controllers
{
    public class ToDoTaskController : Controller
    {
        private readonly IMapper _mapper;

        public ToDoTaskController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromRoute] Guid id,
            [FromServices] IToDoListLogic listLogic,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var list = await listLogic.GetByIdAsync(id);
            ViewData["list"] = list.Name;
            ViewBag.ListId = id;

            var lists = await listLogic.GetAllAsync();
            var listsVM = _mapper.Map<IEnumerable<DisplayToDoListVM>>(lists);

            ViewData["listsVM"] = listsVM;
            var tasks = await taskLogic.GetAllAsync(id);

            var tasksVM = _mapper.Map<IEnumerable<DisplayToDoTaskVM>>(tasks);

            return View(tasksVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid id,
            [FromForm] CreateToDoTaskVM taskVM,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            if (taskVM.DueDate != null && (taskVM.DueDate.Value.Date < DateTime.Now.Date
                || taskVM.DueDate.Value.Date > DateTime.Now.Date.AddYears(5)))
            {
                ModelState.AddModelError("DueDate", $"Due should be between {DateTime.Now.Date} " +
                    $"and {DateTime.Now.AddYears(5).Date}");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.taskVM = taskVM;
                return RedirectToAction("Index", "ToDoTask", new { id });
            }

            var task = _mapper.Map<ToDoTask>(taskVM);
            await taskLogic.AddAsync(id, task);

            return RedirectToAction("Index", "ToDoTask", new { id });
        }

        [HttpGet("{controller}/{action}/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic,
            [FromServices] IToDoTaskStepLogic stepLogic)
        {
            var task = await taskLogic.GetByIdAsync(id);
            var taskDetailsVM = _mapper.Map<ToDoTaskDetailsVM>(task);
            taskDetailsVM.steps = await stepLogic.GetAllAsync(id);
            var listId = task.ToDoListId;

            var tasks = await taskLogic.GetAllAsync(listId);
            var tasksVM = _mapper.Map<IEnumerable<DisplayToDoTaskVM>>(tasks);

            ViewData["tasksVM"] = tasksVM;
            ViewBag.TaskId = id;
            ViewBag.ListId = listId;
            return View(taskDetailsVM);
        }

        public async Task<IActionResult> Delete(Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var task = await taskLogic.GetByIdAsync(id);
            Guid listId = task.ToDoListId;
            await taskLogic.DeleteAsync(id);
            return RedirectToAction("Index", "ToDoTask", new {id = listId });
        }

        [HttpGet]
        public async Task<IActionResult> EditGoalDate([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var task = await taskLogic.GetByIdAsync(id);
            DateTime? date = task.DueDate;
            return View(date);
        }

        [HttpPost]
        public async Task<IActionResult> EditGoalDate([FromRoute] Guid id,
            [FromForm] DateTime? taskGoalDate,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            if (taskGoalDate != null && (taskGoalDate.Value.Date < DateTime.Now.Date
                || taskGoalDate.Value.Date > DateTime.Now.Date.AddYears(5)))
            {
                ModelState.AddModelError("DueDate", $"Due date should be between {DateTime.Now.Date} " +
                    $"and {DateTime.Now.AddYears(5).Date}");
            }

            if (!ModelState.IsValid)
            {
                return View(taskGoalDate);
            }

            await taskLogic.UpdateDueDateAsync(id, taskGoalDate);

            Guid listId = (await taskLogic.GetByIdAsync(id)).ToDoListId;
            return RedirectToAction("Index", "ToDoTask", new {id = listId});
        }

        [HttpGet]
        public async Task<IActionResult> EditReminder([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var task = await taskLogic.GetByIdAsync(id);
            DateTime? date = task.TaskReminderDateTime;
            return View(date);
        }

        [HttpPost]
        public async Task<IActionResult> EditReminder([FromRoute] Guid id,
            [FromForm] DateTime? reminder,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            if (reminder != null && (reminder.Value.Date < DateTime.Now.Date
                || reminder.Value.Date > DateTime.Now.Date.AddYears(5)))
            {
                ModelState.AddModelError("Reminder", $"Reminder date should be between {DateTime.Now.Date} " +
                    $"and {DateTime.Now.AddYears(5).Date}");
            }

            if (!ModelState.IsValid)
            {
                return View(reminder);
            }

            await taskLogic.UpdateReminderAsync(id, reminder);

            Guid listId = (await taskLogic.GetByIdAsync(id)).ToDoListId;
            return RedirectToAction("Index", "ToDoTask", new { id = listId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkCompleted([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            await taskLogic.ChangeCompletedStatusAsync(id);

            Guid listId = (await taskLogic.GetByIdAsync(id)).ToDoListId;
            return RedirectToAction("Index", "ToDoTask", new { id = listId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkImportant([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            await taskLogic.ChangeImportantStatusAsync(id);

            Guid listId = (await taskLogic.GetByIdAsync(id)).ToDoListId;
            return RedirectToAction("Index", "ToDoTask", new { id = listId });
        }

        [HttpGet]
        public async Task<IActionResult> EditTitle([FromRoute] Guid id,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var task = await taskLogic.GetByIdAsync(id);
            EditNameVM VM = new EditNameVM { id = id, Title = task.Title };
            return View(VM);
        }

        [HttpPost]
        public async Task<IActionResult> EditTitle([FromRoute] Guid id,
            [FromForm] EditNameVM vm,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            if (string.IsNullOrEmpty(vm.Title))
            {
                ModelState.AddModelError("Title", $"Title should not be empty");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await taskLogic.UpdateTitleAsync(id, vm.Title);

            Guid listId = (await taskLogic.GetByIdAsync(id)).ToDoListId;
            return RedirectToAction("Index", "ToDoTask", new { id = listId });
        }
    }
}
