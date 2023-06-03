namespace Journalist.Crm.Domain.Pitches
{
    public static class PitchStates
    {
        public const string Draft = "DRAFT";
        public const string ReadyToSend = "READY_TO_SEND";
        public const string Sent = "SENT";
        public const string Accepted = "ACCEPTED";
        public const string Refused = "REFUSED";
        public const string Cancelled = "CANCELLED";
    }
}
