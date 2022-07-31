using FluentValidation.Results;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain
{
    public class Request : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool HasVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total { get; private set; }
        public DateTime EntryDate { get; private set; }
        public RequestStatus Status { get; private set; }
        public virtual Voucher Voucher { get; private set; }
        
        private readonly List<RequestItem> _requestItems = new List<RequestItem>();
        public IReadOnlyCollection<RequestItem> RequestItems => _requestItems;

        public Request(Guid clientId, bool hasVoucher, decimal discount, decimal total)
        {
            ClientId = clientId;
            HasVoucher = hasVoucher;
            Discount = discount;
            Total = total;
            _requestItems = new List<RequestItem>();
        }

        protected Request()
        {
            _requestItems = new List<RequestItem>();
        }

        public void CalculateDiscount()
        {
            if (!HasVoucher) return;

            decimal discount = 0;
            var value = Total;

            if (Voucher.Type == VoucherType.Percentual)
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = (value * Voucher.Percentual.Value) / 100;
                    value -= discount;
                }
            }
            else if (Voucher.Type == VoucherType.Value)
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            Total = value < 0 ? 0 : value;
            Discount = discount;
        }

        public void CalculateTotal()
        {
            Total = RequestItems.Sum(ri => ri.CalculateValue());
            CalculateDiscount();
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validationResult = voucher.IsApplicable();
            if (validationResult.IsValid)
            {
                Voucher = voucher;
                HasVoucher = true;
                CalculateTotal();
            }

            return validationResult;
        }

        public bool HasRequestItem(RequestItem item) => _requestItems.Any(ri => ri.ProductId == item.ProductId);

        public void AddItem(RequestItem item)
        {
            if (!item.IsValid()) return;
            
            item.AsignRequest(Id);

            if (HasRequestItem(item))
            {
                var existingItem = _requestItems.FirstOrDefault(ri => ri.ProductId == item.ProductId);
                existingItem.AddQuantity(item.Quantity);
                item = existingItem;
                _requestItems.Remove(existingItem);
            }

            _requestItems.Add(item);

            CalculateTotal();
        }

        public void RemoveItem(RequestItem item)
        {
            if (!item.IsValid()) return;

            var existingItem = _requestItems.FirstOrDefault(ri => ri.ProductId == item.ProductId);
            _ = existingItem ?? throw new DomainException("Item not in request");
            _requestItems.Remove(existingItem);
            CalculateTotal();
        }

        public void UpdateItem(RequestItem item)
        {
            if (!item.IsValid()) return;

            item.AsignRequest(Id);
            var existingItem = _requestItems.FirstOrDefault(ri => ri.ProductId == item.ProductId);
            _ = existingItem ?? throw new DomainException("Item not in request");
            _requestItems.Remove(existingItem);
            _requestItems.Add(item);
            CalculateTotal();
        }

        public void UpdateItemQuantity(RequestItem item, int quantity)
        {
            item.updateQuantity(quantity);
            UpdateItem(item);
        }

        public void MakeDraft() => Status = RequestStatus.Draft;
        public void Initiate() => Status = RequestStatus.Initialized;
        public void MakeFinal() => Status = RequestStatus.Payed;
        public void Cancel() => Status = RequestStatus.Canceled;

        public static class RquestFactory
        {
            public static Request DraftRequest(Guid clientId)
            {
                var request = new Request { ClientId = clientId };
                request.MakeDraft();
                return request;
            } 
        }
    }
}
