using AutoMapper;
using ToDoApp.Entities;
using ToDoApp.PL.WebPL.Models.ToDoList;

namespace ToDoApp.PL.WebPL.Infrastructure.Profiles
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile() 
        {
            CreateMap<ToDoList, DisplayToDoListVM>();
            CreateMap<DisplayToDoListVM, ToDoList>();
            CreateMap<CreateToDoListVM, ToDoList>();
            CreateMap<ToDoList, CreateToDoListVM>();
        }
    }
}
