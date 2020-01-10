using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Industry_WF
{
    class ProductsDict : Dictionary<Tuple<int, int, int>, Product>
    {
        //protected override int GetKeyForItem(Product item)
        //{
        //    return item.Id;
        //}
    }
}