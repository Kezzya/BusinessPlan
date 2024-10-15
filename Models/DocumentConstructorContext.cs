using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace WebApplication2.Models
{
    public class DocumentConstructorContext : DbContext
    {
        public DocumentConstructorContext() : base("BusinessPlan")
        { }
        public DbSet<DocumentConstructorLeftData> DocumentConstructorLeftDatas { get; set; }
        public DbSet<DocumentConstructor> DocumentConstructors { get; set; }
        public DbSet<DocumentConstructorCenterData> DocumentConstructorCenterDatas { get; set; }
    }
}