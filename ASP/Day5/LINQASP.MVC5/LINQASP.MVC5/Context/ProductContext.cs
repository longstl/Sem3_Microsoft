using LINQASP.MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LINQASP.MVC5.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> products { get; set; }
    }
}