using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Contacts.Events;

public record ContactCreated(EntityId Id, Name Name, OwnerId OwnerId);