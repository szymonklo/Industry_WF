using System;
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
        public int Id { get; set; }
        private static int lastId { get; set; }

        public Factory(string factoryName, int factoryDefProduction, ProductType productType, byte tier)
        {
            Name = factoryName;
            DefProduction = factoryDefProduction;
            ProductType = productType;
            Tier = tier;
            Id = lastId;
            lastId++;

            Product = new Product(productType);
            Products.Add(Product);
            if (productType.Components != null)
            {
                foreach (ProductType component in productType.Components)
                {
                    Products.Add(new Product(component));
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
                Product = Products[productType.Id];
            }
            bool AreComponents, IsComponent = false;

            if (productType.Components != null)
            {
                foreach (ProductType component in productType.Components)
                {
                    Product factoryComponent = Products[component.Id];
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

                if (productType.Components != null)
                {
                    foreach (ProductType component in productType.Components)
                    {
                        Product factoryComponent = Products[component.Id];
                        produktsOnStockCosts += factoryComponent.ProductPrice * ProductionAmount();
                        factoryComponent.AmountIn -= ProductionAmount();
                        Console.WriteLine($"{Name} used: {ProductionAmount()} {factoryComponent.Name} (Components remained: {factoryComponent.AmountIn} {factoryComponent.Name})");
                    }
                }

                Product.AmountOut += ProductionAmount();
                produktsOnStockCosts += Product.ProductCost * Product.AmountOut + BaseCost;
                Program.Money -= Product.ProductCost * Product.AmountOut + BaseCost;
                Product.ProductCost = produktsOnStockCosts / Product.AmountOut;

                TransactionDone?.Invoke(this, new ProductEventArgs(new Product(productType)));
                Console.WriteLine($"{Name} produced: {ProductionAmount()} {Product.Name} (On stock: {Product.AmountOut} {Product.Name})");
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