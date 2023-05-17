using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.PL.WebPL.ViewComponents
{
    public class CreateStepViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
