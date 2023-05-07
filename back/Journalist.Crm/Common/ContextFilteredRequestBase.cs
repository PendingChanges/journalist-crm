namespace Journalist.Crm.Domain.Common
{
    public abstract class ContextFilteredRequestBase
    {
        protected ContextFilteredRequestBase(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
