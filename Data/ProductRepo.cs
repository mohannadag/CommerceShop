using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ProductRepo : EntityRepo<Product>, IProductRepo
    {
        private readonly ApplicationDbContext context;

        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
