using PullPilot.Domain.Models;
using PullPilot.Interface;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace PullPilot.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IHmacValidator _validator;
        private readonly ICodeReviewService _reviewer;
        private readonly IConfiguration _config;

        public GitHubService(
            IHmacValidator validator,
            ICodeReviewService reviewer,
            IConfiguration config)
        {
            _validator = validator;
            _reviewer = reviewer;
            _config = config;
        }

        public Task<bool> ValidateWebhookSignatureAsync(string payload, string signature)
        {
            var secret = _config["GitHub:WebhookSecret"];
            return Task.FromResult(_validator.IsSignatureValid(payload, signature, secret));
        }

        public async Task ProcessPullRequestAsync(string payload)
        {
            var data = JsonSerializer.Deserialize<PullRequestPayload>(payload);

            if (data?.PullRequest == null)
                return;

            // (Later) fetch PR diff via GitHub API
            string fakeDiff = "sample diff here...";

            var review = await _reviewer.GenerateReviewAsync(fakeDiff);

            Console.WriteLine("Generated Review: ");
            Console.WriteLine(review);
        }
    }

}
