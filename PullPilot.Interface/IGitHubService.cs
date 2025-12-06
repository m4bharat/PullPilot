namespace PullPilot.Interface
{
    public interface IGitHubService
    {
        Task<bool> ValidateWebhookSignatureAsync(string payload, string signature);
        Task ProcessPullRequestAsync(string payload);
    }
}
