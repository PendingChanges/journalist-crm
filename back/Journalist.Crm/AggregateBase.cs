using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Journalist.Crm.Domain
{
    public abstract class AggregateBase
    {
        [JsonIgnore] private readonly List<object> _uncommitedEvents = new List<object>();
        [JsonIgnore] private readonly List<Error> _uncommitedErrors = new List<Error>();

        public AggregateBase()
        {
            Id = string.Empty;
            Version = 0;
            State = AggregateState.NotSet;
        }

        public string Id { get; private set; }
        public AggregateState State { get; private set; }
        public long Version { get; private set; }
        public bool HasErrors => _uncommitedErrors.Count > 0;

        public IEnumerable<object> GetUncommitedEvents() => _uncommitedEvents;

        public void ClearUncommitedEvents() => _uncommitedEvents.Clear();

        protected void AddUncommitedEvent(object @event) => _uncommitedEvents.Add(@event);

        protected void AddUncommitedError(Error error) => _uncommitedErrors.Add(error);

        public IEnumerable<Error> GetUncommitedErrors() => _uncommitedErrors;

        public void ClearUncommitedErrors() => _uncommitedErrors.Clear();

        protected void SetId(string id) => Id = id;

        protected void Activate() => State = AggregateState.Set;

        protected void IncrementVersion() => Version++;
    }
}
