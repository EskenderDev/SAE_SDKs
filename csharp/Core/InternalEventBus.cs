using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE.Sdk.Core;

public class InternalEventBus
{
    private readonly Dictionary<string, List<Func<object, Task>>> _handlers = new();

    public void Subscribe<T>(string eventName, Func<T, Task> handler)
    {
        if (!_handlers.ContainsKey(eventName))
            _handlers[eventName] = new();

        _handlers[eventName].Add(async e => await handler((T)e));
    }

    public async Task PublishAsync(string eventName, object data)
    {
        if (!_handlers.TryGetValue(eventName, out var handlers))
            return;

        foreach (var handler in handlers)
        {
            await handler(data);
        }
    }
}
