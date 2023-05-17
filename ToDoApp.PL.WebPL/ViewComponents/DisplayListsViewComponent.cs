using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.Interfaces;
using ToDoApp.Entities;
using ToDoApp.PL.WebPL.Models.ToDoList;

namespace ToDoApp.PL.WebPL.ViewComponents
{
    public class DisplayListsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<DisplayToDoListVM> listsVM)
        {
            return View(listsVM);
        }
    }
}
