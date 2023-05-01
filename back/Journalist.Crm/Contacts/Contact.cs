using System;
using Journalist.Crm.Domain.Common;
using Journalist.Crm.Domain.Contacts.Events;

namespace Journalist.Crm.Domain.Contacts
{
    public sealed class Contact : Aggregate
    {
        public Name Name { get; set; }

#pragma warning disable CS8618
        public Contact(Name name, OwnerId ownerId)   
#pragma warning restore CS8618
        {
            var id = EntityId.NewEntityId();

            var @event = new ContactCreated(id, name, ownerId);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private void Apply(ContactCreated @event)
        {
            SetId(@event.Id);
            Name = @event.Name;
            IncrementVersion();
        }
    }
}
