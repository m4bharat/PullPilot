using System.Text.Json.Serialization;

namespace PullPilot.Domain.Models
{
    public class PullRequestPayload
    {
        public string Action { get; set; }

        [JsonPropertyName("pull_request")]
        public PullRequest PullRequest { get; set; }

        public Repository Repository { get; set; }

        public User Sender { get; set; }
    }
}
