using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain
{
    public abstract class Aggregate
    {
        [JsonIgnore] private readonly List<object> _uncommittedEvents = new();
        [JsonIgnore] private readonly List<Error> _uncommittedErrors = new();

        /// <summary>
        /// Gets or sets the id
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public EntityId Id { get; protected set; } = EntityId.Empty;

        /// <summary>
        /// Gets or sets the version
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public long Version { get; set; }
   
        public bool HasErrors => _uncommittedErrors.Count > 0;

        public IEnumerable<object> GetUncommittedEvents() => _uncommittedEvents;

        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

        protected void AddUncommittedEvent(object @event) => _uncommittedEvents.Add(@event);

        protected void AddUncommittedError(Error error) => _uncommittedErrors.Add(error);

        public IEnumerable<Error> GetUncommittedErrors() => _uncommittedErrors;

        protected void SetId(EntityId id) => Id = id;

        protected void IncrementVersion() => Version++;
    }
}
