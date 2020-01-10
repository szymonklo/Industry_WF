﻿using System;
using System.Collections.Generic;
using System.Text;
using Industry_WF;

namespace Industry_WF
{
    class Factory : Facility
    {
        public int DefProduction { get; set; }
        public int BaseCost { get; set; } = 10;
        public ProductType ProductType { get; set; }
        public Product Product { get; set; }
        private static readonly int _defProduction = 1;
        public byte Tier { get; set; }
        public override int Id { get; set; }
        private static int lastId { get; set; }
        public new int Type { get; private set; }// = 1;
        public int AmountoSend { get; set; }

        public Factory(string factoryName, int factoryDefProduction, ProductType productType, byte tier)
        {
            Name = factoryName;
            DefProduction = factoryDefProduction;
            ProductType = productType;
            Tier = tier;
            Id = lastId;
            lastId++;
            Type = 1;

            Product = new Product(productType, this);
            //Product.Add(Product, this);
            if (productType.Components != null)
            {
                foreach (ProductType component in productType.Components)
                {
                    new Product(component, this);
                }
            }
        }

        //przygotowanie delegata
        public delegate void NoComponentsDelegate(Facility c, ProductEventArgs e);
        public delegate void TransactionDoneDelegate(Facility c, ProductEventArgs e);
        //przygotować deklarację zdarzenia na podstawie powyższego delagata:
        public event NoComponentsDelegate NoComponents;
        public event TransactionDoneDelegate TransactionDone;


        public void Produce(ProductType productType)
        {
            if (productType.Id != Product.Id)
            {
                Product = Product.GetProduct(productType, this);
            }
            bool AreComponents, IsComponent = false;

            if (productType.Components != null)
            {
                foreach (ProductType component in productType.Components)
                {
                    Product factoryComponent = Product.GetProduct(component, this);
                    if (factoryComponent.AmountIn >= ProductionAmount())
                    {
                        IsComponent = true;
                        continue;
                    }
                    else
                    {
                        IsComponent = false;
                        //activate event
                        NoComponents?.Invoke(this, new ProductEventArgs(factoryComponent));
                        break;
                    }
                }
                if (IsComponent)
                    AreComponents = true;
                else
                    AreComponents = false;      //Sprawdzić
            }
            else
                AreComponents = true;

            if (AreComponents)
            {
                double produktsOnStockCosts = 0;
                Product.AmountDone = ProductionAmount();

                if (productType.Components != null)
                {
                    foreach (ProductType component in productType.Components)
                    {
                        Product factoryComponent = Product.GetProduct(component, this);
                        produktsOnStockCosts += factoryComponent.ProductPrice * Product.AmountDone;
                        factoryComponent.AmountIn -= Product.AmountDone;
                        Console.WriteLine($"{Name} used: {Product.AmountDone} {factoryComponent.Name} (Components remained: {factoryComponent.AmountIn} {factoryComponent.Name})");
                    }
                }

                Product.AmountOut += Product.AmountDone;
                produktsOnStockCosts += Product.ProductionCost * Product.AmountDone + BaseCost;
                Program.Cost -= produktsOnStockCosts;
                Program.Money -= produktsOnStockCosts;
                Product.ProductCost = produktsOnStockCosts / Product.AmountOut;

                TransactionDone?.Invoke(this, new ProductEventArgs(Product.GetProduct(productType, this)));
                Console.WriteLine($"{Name} produced: {Product.AmountDone} {Product.Name} (On stock: {Product.AmountOut} {Product.Name})");
                Console.WriteLine($"{Product.Name} cost is {Product.ProductCost:c} per 1 pc.");
            }

            int ProductionAmount()
            {
                return productType.Group switch
                {
                    1 => _defProduction * DefProduction,
                    _ => 0,
                };
            }
        }
    }
}