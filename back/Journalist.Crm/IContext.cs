﻿using Journalist.Crm.Domain.Common;

namespace Journalist.Crm.Domain
{
    public interface IContext
    {
        OwnerId UserId { get; }
    }
}
