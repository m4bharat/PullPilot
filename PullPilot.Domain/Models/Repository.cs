using System.Text.Json.Serialization;

namespace PullPilot.Domain.Models
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        public User Owner { get; set; }
    }
}
