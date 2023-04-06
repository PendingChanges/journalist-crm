using MediatR;

namespace Journalist.Crm.Domain.Pitches.Commands
{
    public record DeletePitch(string Id): ICommand;
}
