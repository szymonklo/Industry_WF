using System;
using System.Collections.Generic;
using System.Text;

namespace Industry
{
    class Facility
    {
        public string Name { get; set; }
        public virtual ProductsKeyed Products { get; set; } = new ProductsKeyed();
    }
}
