using System;
using System.Collections.Generic;
using System.Text;

namespace Industry_WF
{
    class Facility
    {
        public string Name { get; set; }
        public virtual ProductsKeyed Products { get; set; } = new ProductsKeyed();

    }
}
