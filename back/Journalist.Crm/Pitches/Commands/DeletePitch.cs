using Journalist.Crm.Domain.CQRS;
using Journalist.Crm.Domain.ValueObjects;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record DeletePitch(EntityId Id): ICommand;
}
