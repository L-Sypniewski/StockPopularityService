namespace Core.Utils.Logging
{
    public interface ICorrelationIdProvider
    {
        string GetCorrelationId();
        void ResetId();
    }
}