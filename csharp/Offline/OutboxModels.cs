using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE.Sdk.Offline;

public class SaeOutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Endpoint { get; set; } = default!;
    public string Method { get; set; } = "POST";
    public string PayloadJson { get; set; } = default!;

    public string Status { get; set; } = "PENDING"; 
    // PENDING | PROCESSING | SENT | FAILED | DEAD

    public int RetryCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastAttemptAt { get; set; }
    public DateTime NextAttemptAt { get; set; } = DateTime.UtcNow;

    public string? LastError { get; set; }

    public string IdempotencyKey { get; set; } = Guid.NewGuid().ToString("N");

    public int Priority { get; set; } = 0; // Facturas = 10, otros = 1
}

public interface ISaeOutboxRepository
{
    Task AddAsync(SaeOutboxMessage message);
    Task<List<SaeOutboxMessage>> GetBatchAsync(int size);
    Task MarkProcessingAsync(Guid id);
    Task MarkAsSentAsync(Guid id);
    Task MarkAsFailedAsync(Guid id, string error);
    Task MarkAsDeadAsync(Guid id, string error);
    Task UpdateRetryAsync(SaeOutboxMessage message);
    Task<int> CountAsync(string status);
}

public static class BackoffStrategy
{
    public static DateTime CalculateNext(int retryCount)
    {
        var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount)); 
        // 2s, 4s, 8s, 16s...

        // jitter (evita tormenta)
        var jitter = TimeSpan.FromMilliseconds(Random.Shared.Next(100, 1000));

        return DateTime.UtcNow + delay + jitter;
    }
}
