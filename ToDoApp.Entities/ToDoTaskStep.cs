using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Entities
{
    [Table("ToDoTaskSteps")]
    public class ToDoTaskStep
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public Guid ToDoTaskId { get; set; }
    }
}
