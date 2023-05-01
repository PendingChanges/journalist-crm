using System;

namespace Journalist.Crm.Domain.Common
{
    public readonly struct EntityId
    {
        private readonly Guid _value;

        public EntityId(string value) : this(Guid.Parse(value)) { }
        private EntityId(Guid value) => _value = value;

        public static implicit operator string(EntityId id) => id._value.ToString();
        public static readonly EntityId Empty = new();

        public static EntityId NewEntityId() => new(Guid.NewGuid());
    }
}
