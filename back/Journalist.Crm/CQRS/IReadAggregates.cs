﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.CQRS
{
    public interface IReadAggregates
    {
        Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken ct = default) where T : Aggregate;
    }
}