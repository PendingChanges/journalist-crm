using System.Collections.Generic;
using System.Linq;

namespace Journalist.Crm.Domain
{
    public class AggregateResult<T> where T : AggregateBase
    {
        private readonly List<Error> _errors = new List<Error>();


        public AggregateResult(T? data)
            : this(data, new Error[] { }) { }

        public AggregateResult(T? data, IEnumerable<Error> errors)
        {
            Data = errors.Any() ? null : data;
            _errors.AddRange(errors);
        }

        public T? Data { get; private set; }

        public bool IsSuccess => _errors.Count == 0;

        public IEnumerable<Error> Errors => _errors;

        public void AddErrors(params Error[] errors) => _errors.AddRange(errors);
    }
}
