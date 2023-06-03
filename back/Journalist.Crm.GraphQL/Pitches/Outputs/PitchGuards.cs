namespace Journalist.Crm.GraphQL.Pitches.Outputs
{
    public record PitchGuards(bool CanModify, bool CanValidate, bool CanCancel, bool CanSend, bool CanAccept, bool CanRefuse);
}
