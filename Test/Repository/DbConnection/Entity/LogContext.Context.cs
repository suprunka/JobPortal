﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.DbConnection.Entity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dmai0917_1067677Entities1 : IdentityDbContext<JobPortal.Model.ApplicationUser>
    {
        public dmai0917_1067677Entities1()
            : base("data source = kraka.ucn.dk; initial catalog = dmai0917_1067677; user id = dmai0917_1067677; password=Password1!;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static dmai0917_1067677Entities1 Create()
        {
            return new dmai0917_1067677Entities1();
        }

    }
}
