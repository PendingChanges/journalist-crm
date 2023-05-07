﻿using Journalist.Crm.Domain.Pitches.Events;
using System;
using System.Collections.Generic;

namespace Journalist.Crm.Domain.Common;

public class AggregateResult
{
    private AggregateResult()
    {
        
    }
    private readonly ErrorCollection _errors = new();
    private readonly EventCollection _events = new();
    public bool HasErrors => _errors.HasErrors;

    public void AddEvent(object @event) => _events.Add(@event);
    public void AddError(string code) => _errors.AddError(code);

    public void CheckAndAddError(Func<bool> check, string code)
    {
        if (check())
        {
            AddError(code);
        }
    }

    public IEnumerable<Error> GetErrors() => _errors.GetErrors();

    public static AggregateResult Create() => new();

    public IEnumerable<object> GetEvents() => _events.GetEvents();
}