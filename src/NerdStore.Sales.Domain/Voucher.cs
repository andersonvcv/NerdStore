﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain;

public class Voucher : Entity
{
    public string Code { get; private set; }
    public decimal? Percentual { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherType Type { get; private set; }
    public DateTime EntryDate { get; private set; }
    public DateTime? UsageDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool IsActive { get; private set; }
    public bool WasUsed { get; private set; }

    public ICollection<Request> Requests { get; private set; }
}