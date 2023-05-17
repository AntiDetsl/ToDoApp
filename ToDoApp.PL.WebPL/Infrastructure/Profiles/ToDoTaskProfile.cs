using AutoMapper;
using ToDoApp.Entities;
using ToDoApp.PL.WebPL.Models.ToDoTaskVM;

namespace ToDoApp.PL.WebPL.Infrastructure.Profiles
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile() 
        {
            CreateMap<CreateToDoTaskVM, ToDoTask>();
            CreateMap<ToDoTask, CreateToDoTaskVM>();
            CreateMap<ToDoTask, DisplayToDoTaskVM>();
            CreateMap<DisplayToDoTaskVM, ToDoTask>();
            CreateMap<ToDoTask, ToDoTaskDetailsVM>();
        }
    }
}
