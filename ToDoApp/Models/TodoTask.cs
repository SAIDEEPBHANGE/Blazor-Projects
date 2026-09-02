namespace ToDoApp.Models
{
    public class TodoTask
    {
        public Guid id = new Guid();
        public string text = string.Empty;
        public bool isEdit = false;
        public bool isDone = false;
    }
}
