using System.Text.Json.Serialization;

namespace ToDoAppAPI.Models
{
    public class Status
    {
        [JsonIgnore]
        public uint Id { get; set; }
        public string Label { get; set; }
        public string Code { get; set; }
    }
}
