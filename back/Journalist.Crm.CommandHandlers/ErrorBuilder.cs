using Journalist.Crm.Domain;

namespace Journalist.Crm.CommandHandlers
{
    public static class ErrorBuilder
    {
        public static Error AggregateNotFound() => new(Errors.AGGREGATE_NOT_FOUND.CODE, Errors.AGGREGATE_NOT_FOUND.MESSAGE);
    }
}
