using Microsoft.AspNetCore.Mvc;
using ToDoApp.PL.WebPL.Models.ToDoTaskVM;

namespace ToDoApp.PL.WebPL.ViewComponents
{
    public class CreateTaskViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CreateToDoTaskVM? vm)
        {
            if(vm == null)
            {
                vm = new CreateToDoTaskVM();
            }
            return View(vm);
        }
    }
}
