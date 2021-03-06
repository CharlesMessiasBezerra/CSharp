﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ModerStore.Domain.Enums;
using ModerStore.Shared.Entities;
using FluentValidator;

namespace ModerStore.Domain.Entities
{
    public class Order: Entity
    {
        
        private readonly IList<OrderItem> _items;


        public Order( Customer custumer, decimal deliveryFee, decimal discount)
        {
            Customer = custumer;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0,8).ToUpper();
            Status = EOrderStatus.Created;
            DeliveryFee = deliveryFee;
            Discount = discount;

            _items = new List<OrderItem>();

            new ValidationContract<Order>(this)
                .IsGreaterThan(x => x.DeliveryFee, 0)
                .IsGreaterThan(x => x.DeliveryFee, -1);
        }
        public Customer Customer { get; private set; }

        public DateTime CreateDate { get; private set; }
        public string Number { get; private set; }

        public EOrderStatus Status { get; private set; }

        public IReadOnlyCollection<OrderItem> Items {get { return _items.AsEnumerable().ToArray(); }}

        public decimal DeliveryFee { get; private set; }

        public decimal Discount { get; private set; }

        public decimal SubTotal() => Items.Sum(X => X.Total());

        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)           
        {
            if(item.IsValid())
                _items.Add(item);
        }

        public void place()
        {

        }

    }
}
