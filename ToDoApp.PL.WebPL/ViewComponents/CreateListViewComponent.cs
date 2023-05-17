using Microsoft.AspNetCore.Mvc;
using ToDoApp.PL.WebPL.Models.ToDoList;

namespace ToDoApp.PL.WebPL.ViewComponents
{
    public class CreateListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new CreateToDoListVM());
        }
    }
}
