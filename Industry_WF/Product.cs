using System;
using System.Collections.Generic;
using System.Text;
using Industry_WF;

namespace Industry_WF
{
    class Product : ProductType
    {
        public int AmountIn { get; set; }
        public int AmountOut { get; set; }
        public int AmountDone { get; set; }
        public double ProductPrice { get; set; }
        public double ProductCost { get; set; }
        public double ProductionCost { get; set; }

        public double ProductProfit { get; set; }

        public Product(int id, byte group, string productName, double defPrice, List<ProductType> components, int amount = 0)
            : base(id, group, productName, defPrice, components) { }

        public Product(ProductType productType, int amount = 0)
            : this(productType.Id, productType.Group, productType.Name, productType.DefPrice, productType.Components)
        {
            ProductionCost = productType.Group;
            AmountIn = amount;
        }
    }
}