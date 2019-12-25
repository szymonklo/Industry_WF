using System;
using System.Collections.Generic;
using System.Text;

namespace Industry_WF
{
    class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }

        public ProductEventArgs(Product product)
        {
            Product = product;
        }
    }
}