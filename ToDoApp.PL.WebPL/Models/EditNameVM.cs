using System.ComponentModel.DataAnnotations;

namespace ToDoApp.PL.WebPL.Models
{
    public class EditNameVM
    {
        public Guid id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;
    }
}
