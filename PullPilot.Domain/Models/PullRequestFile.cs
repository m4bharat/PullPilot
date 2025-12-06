using System.Text.Json.Serialization;

namespace PullPilot.Domain.Models
{
    public class PullRequest
    {
        public int Id { get; set; }

        public string Url { get; set; }

        [JsonPropertyName("diff_url")]
        public string DiffUrl { get; set; }

        [JsonPropertyName("patch_url")]
        public string PatchUrl { get; set; }

        public string Title { get; set; }
    }
}
