using System;
using System.Collections.Generic;
using Journalist.Crm.Domain.CQRS;

namespace Journalist.Crm.Domain
{
    public class DomainException : Exception
    {
        public DomainException(IEnumerable<Error>? errors)
        {
            DomainErrors = errors ?? new List<Error>();
        }

        public IEnumerable<Error> DomainErrors { get; }
    }
}
