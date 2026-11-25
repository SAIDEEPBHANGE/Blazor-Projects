namespace ToDoApp.Models
{
    public class ToDoTasks
    {
        public List<TodoTask> Tasks { get; set; } = new();
        public List<TodoTask> tasks
        {
            get => Tasks;
            set => Tasks = value;
        }
    }
}
