using System.ComponentModel;

namespace ToDoApp.PL.WebPL.Models.ToDoList
{
    public class DisplayToDoListVM
    {
        public Guid Id { get; set; }

        [DisplayName("Title")]
        public string Name { get; set; } = null!;
    }
}
