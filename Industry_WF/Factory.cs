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

        public Factory(string factoryName, int factoryDefProduction, ProductType productType, byte tier)
        {
            Name = factoryName;
            DefProduction = factoryDefProduction;
            ProductType = productType;
            Tier = tier;
            
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