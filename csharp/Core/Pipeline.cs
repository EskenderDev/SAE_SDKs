using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SAE.Sdk.Core;

public class SaeContext
{
    public HttpRequestMessage Request { get; set; } = default!;
    public HttpResponseMessage? Response { get; set; }

    public Dictionary<string, object> Items { get; } = new();

    public CancellationToken CancellationToken { get; set; }
}

public interface ISaeMiddleware
{
    Task InvokeAsync(SaeContext context, Func<Task> next);
}

public class SaePipeline
{
    private readonly IList<Func<Func<Task>, Func<Task>>> _components 
        = new List<Func<Func<Task>, Func<Task>>>();

    private SaeContext? _context;

    public SaePipeline Use(Func<SaeContext, Func<Task>, Task> middleware)
    {
        _components.Add(next => () => middleware(_context!, next));
        return this;
    }

    public async Task ExecuteAsync(SaeContext context, Func<Task> terminal)
    {
        _context = context;

        Func<Task> pipeline = terminal;

        foreach (var component in _components.Reverse())
        {
            pipeline = component(pipeline);
        }

        await pipeline();
    }
}
