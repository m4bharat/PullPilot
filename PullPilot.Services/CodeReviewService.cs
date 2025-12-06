using PullPilot.Interface;

namespace PullPilot.Services
{
    public class CodeReviewService : ICodeReviewService
    {
        public Task<string> GenerateReviewAsync(string diffText)
        {
            // TODO: Integrate OpenAI API here
            return Task.FromResult($"[Stub] Review for diff:\n{diffText}");
        }
    }
}
