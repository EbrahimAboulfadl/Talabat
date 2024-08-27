﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductsSpecsParams
    {
        public string? Sort {  get; set; }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public int? Page {  get; set; }
        public int? PageSize { get; set; }

    }
}
