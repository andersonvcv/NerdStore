insert into Vouchers (Id, Code, Percentual, DiscountValue, Quantity, Type, EntryDate, UsageDate, ExpirationDate, IsActive, WasUsed)
values (NEWID(), '15OFF', NULL, 15, 0, 1, GETDATE(), null, GETDATE() + 1, 1, 0)

insert into Vouchers (Id, Code, Percentual, DiscountValue, Quantity, Type, EntryDate, UsageDate, ExpirationDate, IsActive, WasUsed)
values (NEWID(), '10OFF', 10, null, 50, 0, GETDATE(), null, GETDATE() + 90, 1, 0)
