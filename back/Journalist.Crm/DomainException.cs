using System;
using System.Collections.Generic;

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
