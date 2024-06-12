namespace ToDoAppAPI.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public Guid UserId { get; set; }
        public User? Author { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}
