using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Contacts.Events;

public record ContactCreated(EntityId Id, Name Name, OwnerId OwnerId);