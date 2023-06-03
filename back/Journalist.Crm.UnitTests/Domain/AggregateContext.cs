using System.Collections.Generic;
using System.Linq;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.UnitTests.Domain
{
    public class AggregateContext
    {
        public Aggregate? Aggregate { get; set; }

        public AggregateResult? Result { get; set; }

        public List<object> GetEvents() => Result?.GetEvents().ToList() ?? new List<object>();

        public List<Error> GetErrors() => Result?.GetErrors().ToList() ?? new List<Error>();
    }
}
