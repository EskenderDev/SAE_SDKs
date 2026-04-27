using System;
using System.Collections.Generic;

namespace SAE.Sdk.Models;

/// <summary>DTO para representar una aprobación pendiente en el sistema</summary>
public class PendingApprovalDto
{
    public Guid Id { get; set; }
    public ApprovalType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TenantId { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "CRC";
    public string ReferenceNumber { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public List<PendingApprovalItemDto> Items { get; set; } = new();
}

/// <summary>Detalle de un ítem dentro de una aprobación pendiente</summary>
public class PendingApprovalItemDto
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}
