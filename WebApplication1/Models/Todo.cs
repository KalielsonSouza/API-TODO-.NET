namespace WebApplication1.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
