using HotChocolate;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalist.Crm.GraphQL
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        private readonly ILogger<GraphQLErrorFilter> _logger;

        public GraphQLErrorFilter(ILogger<GraphQLErrorFilter> logger)
        {
            _logger = logger;
        }

        public IError OnError(IError error)
        {
            _logger.LogError(error.Exception, error.Exception?.Message ?? error.Message);

            return error;
        }
    }
}
