using System.Text.Json.Serialization;

namespace ToDoAppAPI.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        [JsonIgnore]
        public uint StatusId { get; set; }
        public Status Status { get; set; }
        public Guid BoardId { get; set; }
        [JsonIgnore]
        public Board? Board { get; set; }
    }
}
