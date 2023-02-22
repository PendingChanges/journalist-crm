using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
