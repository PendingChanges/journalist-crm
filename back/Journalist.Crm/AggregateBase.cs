using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Journalist.Crm.Domain
{
    public abstract class AggregateBase
    {
        public string Id { get; protected set; }
        public long Version { get; set; }
        public bool HasErrors => _uncommitedErrors.Count > 0;
        [JsonIgnore] private readonly List<object> _uncommitedEvents = new List<object>();
        [JsonIgnore] private readonly List<Error> _uncommitedErrors = new List<Error>();

        public IEnumerable<object> GetUncommitedEvents()
        {
            return _uncommitedEvents;
        }

        public void ClearUncommitedEvents()
        {
            _uncommitedEvents.Clear();
        }

        protected void AddUncommitedEvent(object @event)
        {
            _uncommitedEvents.Add(@event);
        }

        protected void AddUncommitedError(Error error)
        {
            _uncommitedErrors.Add(error);
        }

        public IEnumerable<Error> GetUncommitedErrors()
        {
            return _uncommitedErrors;
        }

        public void ClearUncommitedErrors()
        {
            _uncommitedErrors.Clear();
        }
    }
}
