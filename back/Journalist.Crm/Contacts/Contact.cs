using System;
using Journalist.Crm.Domain.Contacts.Events;
using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Contacts
{
    public sealed class Contact : Aggregate
    {
        public Name Name { get; set; }

#pragma warning disable CS8618
        private Contact()   
#pragma warning restore CS8618
        {
        }

        public AggregateResult Create(Name name, OwnerId ownerId)
        {
            var result = AggregateResult.Create();
            var id = EntityId.NewEntityId();
            var @event = new ContactCreated(id, name, ownerId);
            Apply(@event);
            result.AddEvent(@event);
            return result;
        }

        private void Apply(ContactCreated @event)
        {
            SetId(@event.Id);
            Name = @event.Name;
            IncrementVersion();
        }
    }
}
