namespace PullPilot.Interface
{
    public interface IHmacValidator
    {
        bool IsSignatureValid(string payload, string signature, string secret);
    }
}
