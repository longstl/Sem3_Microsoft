using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAnnotations1.Models;

namespace DataAnnotations1.Context
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
    }
}