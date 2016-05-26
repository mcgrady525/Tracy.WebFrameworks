﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Tracy.WebFrameworks.Entity;


namespace Tracy.WebFrameworks.Data
{
    public partial class WebFrameworksDB : DbContext
    {
      public WebFrameworksDB()
            : base(string.Format(@"metadata=res://*/WebFrameworksDB.csdl|res://*/WebFrameworksDB.ssdl|res://*/WebFrameworksDB.msl;provider=System.Data.SqlClient;provider connection string='{0};multipleactiveresultsets=True;App=EntityFramework'",System.Configuration.ConfigurationManager.ConnectionStrings["WebFrameworksDB"].ConnectionString))
        {
    		this.Configuration.ProxyCreationEnabled = false; //默认关闭代理类
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Corporation> Corporation { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Function> Function { get; set; }
        public DbSet<RoleFunction> RoleFunction { get; set; }
    }
}
