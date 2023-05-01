using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record DeletePitch(EntityId Id): ICommand;
}
