﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Industry_WF
{
    class TransportOrder
    {
        public int TransportCost { get; set; } = 1;
        public int Amount { get; set; }
        public Facility Sender { get; set; }
        public Facility Receiver { get; set; }
        public ProductType ProductType { get; set; }
        public int Capacity { get; set; }// = 20;

        private static Dictionary<Tuple<int, int, int, int, int>, TransportOrder> _transportOrders = new Dictionary<Tuple<int, int, int, int, int>, TransportOrder>();


        //przygotowanie delegata
        public delegate void FewProductsToSendDelegate(Facility c, ProductEventArgs e);
        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public event FewProductsToSendDelegate FewProductsToSend;

        public TransportOrder(Facility sender, Facility receiver, ProductType productType, int capacity)
        {
            Sender = sender;
            Receiver = receiver;
            ProductType = productType;
            Capacity = capacity;
            Add();
        }
        public void Add()
        {
            Tuple<int, int, int, int, int> tokey = new Tuple<int, int, int, int, int>(Sender.Type(), Sender.Id, Receiver.Type(), Receiver.Id, ProductType.Id);
            _transportOrders.Add(tokey, this);
        }
        public static TransportOrder GetOrder(Facility sender, Facility receiver, ProductType productType)
        {
            Tuple<int, int, int, int, int> tokey = new Tuple<int, int, int, int, int>(sender.Type(), sender.Id, receiver.Type(), receiver.Id, productType.Id);
            return _transportOrders[tokey];

        }
        public void Go()
        {
            Product productS = Product.GetProduct(ProductType, Sender);
            if (productS != null && productS.AmountOut > 0)
                //Sender.Products.Contains(ProductType.Id) && Sender.Products[ProductType.Id].AmountOut > 0)
            {
                if (productS.AmountOut < Capacity)
                {
                    //FewProductsToSend?.Invoke(Sender, new ProductEventArgs(Sender.Products[ProductType.Id]));
                }
                Amount = Math.Min(Capacity, productS.AmountOut);
                productS.AmountOut -= Amount;
                Product productIn;
                Product productR = Product.GetProduct(ProductType, Receiver);
                if (productR != null)
                {
                    productIn = productR;
                }
                else
                {
                    productIn = new Product(ProductType, Receiver, Amount);
                    //Receiver.Products.Add(new Product(ProductType, Receiver, Amount));
                }
                double productInCost = productIn.AmountIn * productIn.ProductCost;
                productIn.AmountIn += Amount;
                productInCost += Amount * productS.ProductCost + TransportCost;
                Program.Cost -= TransportCost;
                Program.Money -= TransportCost;
                productIn.ProductCost = productInCost / productIn.AmountIn;

                Console.WriteLine($"Transported {Capacity} {ProductType.Name}");
                Console.WriteLine($"In {Sender.Name} (origin) left {productS.AmountOut} {ProductType.Name}");
                Console.WriteLine($"In {Receiver.Name} (destination) there are {productIn.AmountIn} {productIn.Name}\n");
            }
            else
            {
                Console.WriteLine("no product to send");
                FewProductsToSend?.Invoke(Sender, new ProductEventArgs(productS));
            }
            }
        }
}