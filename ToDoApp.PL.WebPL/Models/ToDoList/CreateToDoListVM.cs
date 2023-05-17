using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.PL.WebPL.Models.ToDoList
{
    public class CreateToDoListVM
    {
        [Required]
        [StringLength(50)]
        [DisplayName("Title")]
        public string Name { get; set; } = null!;
    }
}
