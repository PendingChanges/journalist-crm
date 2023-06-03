using System.Collections.Generic;

namespace Journalist.Crm.Domain.CQRS
{
    public class ErrorCollection
    {
        public static class WellKnownErrors
        {
            public const string NotPitchOwner = "NOT_PITCH_OWNER";
            public const string PitchNotCancellable = "PITCH_NOT_CANCELLABLE";
            public const string PitchNotModifiable = "PITCH_NOT_MODIFIABLE";
            public const string NotClientOwner = "NOT_CLIENT_OWNER";
            public const string NotIdeaOwner = "NOT_IDEA_OWNER";
            public const string PitchNotSendable = "PITCH_NOT_SENDABLE";
            public const string PitchNotValidatable = "PITCH_NOT_VALIDATABLE";
            public const string PitchNotAcceptable = "PITCH_NOT_ACCEPTABLE";
            public const string PitchNotRefusable = "PITCH_NOT_REFUSABLE";

            internal static readonly Dictionary<string, string> Messages = new()
            {
                { NotPitchOwner, "The user is not the owner of this pitch"},
                { PitchNotCancellable, "The pitch is not cancellable"},
                { PitchNotModifiable, "The pitch is not modifiable"},
                { NotClientOwner, "The user is not the owner of this client"},
                { NotIdeaOwner, "The user is not the owner of this idea"},
                { PitchNotSendable, "The pitch is not sendable"},
                { PitchNotValidatable, "The pitch is not validatable"},
                { PitchNotAcceptable, "The pitch is not acceptable"},
                { PitchNotRefusable, "The pitch is not refusable"}
            };
        }

        private readonly List<Error> _errors = new();

        public bool HasErrors => _errors.Count > 0;

        public void Add(Error error) => _errors.Add(error);

        public void AddError(string code) => Add(new Error(code,
            WellKnownErrors.Messages.TryGetValue(code, out var message) ? message : "Unknown Message"));

        public IEnumerable<Error> GetErrors() => _errors;
    }
}
