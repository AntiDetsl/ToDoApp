using ToDoApp.Entities;

namespace ToDoApp.Tests.ToDoTaskTests
{
    [TestFixture]
    public class ToDoTaskTests
    {
        [Test]
        public void ToDoTask_DefaultValues_Success()
        {
            // Arrange
            var task = new ToDoTask();

            // Assert
            Assert.IsNotNull(task.Id);
            Assert.IsNull(task.Title);
            Assert.AreEqual(DateTime.Now.Date, task.CreationDate.Date);
            Assert.IsNull(task.DueDate);
            Assert.IsFalse(task.IsImportant);
            Assert.IsFalse(task.IsCompleted);
            Assert.IsNull(task.TaskReminderDateTime);
            Assert.AreEqual(Guid.Empty, task.ToDoListId);
            Assert.IsNull(task.ToDoList);
            Assert.IsNull(task.ToDoTaskSteps);
        }

        [Test]
        public void ToDoTask_SetValues_Success()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var title = "Sample Task";
            var creationDate = DateTime.Now;
            var dueDate = DateTime.Now.AddDays(7);
            var isImportant = true;
            var isCompleted = false;
            var reminderDateTime = DateTime.Now.AddHours(2);
            var listId = Guid.NewGuid();
            var list = new ToDoList();
            var steps = new List<ToDoTaskStep>();

            // Act
            var task = new ToDoTask
            {
                Id = taskId,
                Title = title,
                CreationDate = creationDate,
                DueDate = dueDate,
                IsImportant = isImportant,
                IsCompleted = isCompleted,
                TaskReminderDateTime = reminderDateTime,
                ToDoListId = listId,
                ToDoList = list,
                ToDoTaskSteps = steps
            };

            // Assert
            Assert.AreEqual(taskId, task.Id);
            Assert.AreEqual(title, task.Title);
            Assert.AreEqual(creationDate, task.CreationDate);
            Assert.AreEqual(dueDate, task.DueDate);
            Assert.AreEqual(isImportant, task.IsImportant);
            Assert.AreEqual(isCompleted, task.IsCompleted);
            Assert.AreEqual(reminderDateTime, task.TaskReminderDateTime);
            Assert.AreEqual(listId, task.ToDoListId);
            Assert.AreEqual(list, task.ToDoList);
            Assert.AreEqual(steps, task.ToDoTaskSteps);
        }
    }
}