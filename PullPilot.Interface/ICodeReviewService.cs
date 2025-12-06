namespace PullPilot.Interface
{
    public interface ICodeReviewService
    {
        Task<string> GenerateReviewAsync(string diffText);
    }
}
