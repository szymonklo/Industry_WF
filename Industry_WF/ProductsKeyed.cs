using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Industry_WF
{
    class ProductsKeyed : KeyedCollection<int, Product>
    {
        protected override int GetKeyForItem(Product item)
        {
            return item.Id;
        }
    }
}