using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Journalist.Crm.Domain.Common
{
    public record OwnerId
    {
        private readonly string _value = string.Empty;

        public OwnerId(string value) => _value = value;
        public static implicit operator string(OwnerId id) => id._value;
        public static explicit operator OwnerId(string value) => new(value);
        public static readonly OwnerId Empty = new(string.Empty);
    }
}
