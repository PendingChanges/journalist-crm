using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Journalist.Crm.Domain
{
    public abstract class AggregateBase
    {
        [JsonIgnore] private readonly List<object> _uncommitedEvents = new List<object>();
        [JsonIgnore] private readonly List<Error> _uncommitedErrors = new List<Error>();

        /// <summary>
        /// Gets or sets the id
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public string Id { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public long Version { get; set; }
        public AggregateState State { get; private set; }
   
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
