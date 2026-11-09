namespace ToDoApp.Models
{
    public class TodoTask
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string text { get; set; } = string.Empty;
        public bool isDone { get; set; } = false;
        public bool isEdit { get; set; } = false;
    }
}
