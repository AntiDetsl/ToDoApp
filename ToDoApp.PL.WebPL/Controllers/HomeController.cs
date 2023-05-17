using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoApp.BLL.Interfaces;
using ToDoApp.Entities;
using ToDoApp.PL.WebPL.Models;
using ToDoApp.PL.WebPL.Models.ToDoList;
using ToDoApp.PL.WebPL.Models.ToDoTaskVM;

namespace ToDoApp.PL.WebPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromServices] IToDoListLogic listLogic,
            [FromServices] IToDoTaskLogic taskLogic)
        {
            var lists = await listLogic.GetAllWithTaskAsync();

            var tasksVM = (await taskLogic.GetAllForToday())
                .Select(i => new DisplayTodayToDoTaskVM
                 {
                     Title = i.Item2.Title,
                     ListName = i.Item1.Name,
                     Id = i.Item2.Id,
                     ListId = i.Item1.Id,
                     DueDate = i.Item2.DueDate!.Value,
                     Reminder = i.Item2.TaskReminderDateTime
                 });

            var listsVM = _mapper.Map<IEnumerable<DisplayToDoListVM>>(lists);
            var vm = (listsVM, tasksVM);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateToDoListVM listVM,
            [FromServices] IToDoListLogic listLogic)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var list =_mapper.Map<ToDoList>(listVM);

            await listLogic.AddAsync(list);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete([FromRoute] Guid id,
            [FromServices] IToDoListLogic listLogic)
        {
            await listLogic.DeleteAsync(id);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id,
            [FromServices] IToDoListLogic listLogic)
        {
            var list = await listLogic.GetByIdAsync(id);
            return View(_mapper.Map<CreateToDoListVM>(list));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id,
        [FromForm] CreateToDoListVM listVM,
            [FromServices] IToDoListLogic listLogic)
        {
            await listLogic.UpdateAsync(id, listVM.Name);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}