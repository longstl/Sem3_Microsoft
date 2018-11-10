using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class Product1Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
