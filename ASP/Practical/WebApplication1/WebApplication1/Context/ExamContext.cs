using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class ExamContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
    }
}