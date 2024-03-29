﻿using Mervi.SeedWork;
using Provider.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Provider.Domain.AggregatesModels.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime GetOrderDate() => _orderDate;
        private readonly DateTime _orderDate;

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public OrderStatus Status { get; private set; }
        private int _orderStatusId;

        public string GetCustomerOrderId() => _customerOrderId;
        private readonly string _customerOrderId; 

        public string GetSellerId() => _sellerId;
        private readonly string _sellerId;

        private Order()
        {
            this._orderItems = new List<OrderItem>();
        }

        public Order(string sellerId, string customerOrderId, DateTime orderDate) : base()
        {
            _customerOrderId = customerOrderId;
            this._orderDate = orderDate;
            this._sellerId = sellerId;
            _orderStatusId = OrderStatus.Submitted.Id;

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddOrderStartedDomainEvent();
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, string pictureUrl, int units)
        {
            var existingOrderForProduct = _orderItems.Where(i => i.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.AddUnits(units);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, unitPrice, pictureUrl, units);
                _orderItems.Add(orderItem);
            }
        }

        public void SetConfirmedStatus()
        {
            this._orderStatusId = OrderStatus.StockConfirmed.Id;
            this.Status = OrderStatus.StockConfirmed;

            AddOrderConfirmedDomainEvent();
        }

        private void AddOrderStartedDomainEvent()
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this);

            this.AddDomainEvent(orderStartedDomainEvent);
        }

        private void AddOrderConfirmedDomainEvent()
        {
            var orderConfirmedDomainEvent = new OrderConfirmedDomainEvent(this);

            this.AddDomainEvent(orderConfirmedDomainEvent);
        }
    }
}
