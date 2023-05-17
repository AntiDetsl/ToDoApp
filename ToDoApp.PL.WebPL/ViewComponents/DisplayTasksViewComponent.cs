using Microsoft.AspNetCore.Mvc;
using ToDoApp.PL.WebPL.Models.ToDoTaskVM;

namespace ToDoApp.PL.WebPL.ViewComponents
{
    public class DisplayTasksViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<DisplayToDoTaskVM> tasksVM)
        {
            return View(tasksVM);
        }
    }
}
