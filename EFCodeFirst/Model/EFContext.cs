﻿using System.Data.Entity;

namespace EFCodeFirst.Model
{
    // Steps to create Code First Entity Framework
    // (1) Right Click on Project Name-> Manage NuGet Packages -> Search for "EntityFramework" -> Install
    // (2) Add ConnectionString in web.config file
    // (3) Create Model Classes with get set properties
    // (4) Create Context Class -> Inherit with DbContext class -> Write Constructor and DbSet<> for all Model classes
    // (5) Add Connection String Name in Constructor of DbContext Class
    // (6) Open Tools -> NuGet Package Manager -> Package Manager Console, Run this Command "enable-migrations –EnableAutomaticMigration:$true"
    // (7) Open Package Manager Console -> Run this Command "add-migration 'First'"
    // (8) To Create Database -> Open Package Manager Console -> Run this Command "update-database -verbose"
    // (9) To Rollback Migration -> update-database -TargetMigration:"Migration File Name"

    #region Configuration File's Code
    //protected override void Seed(EFCodeFirst.Model.EFContext context)
    //{
    //    context.Customers.AddOrUpdate(x => x.CustomerID,
    //    new Model.Customer() { CustomerID = 1, FirstName = "Chris", LastName = "Lee", BirthDate = Convert.ToDateTime("1987-12-16"), Email = "clee0@mail.ru", Address = "76 Green Ridge Junction" },
    //    new Model.Customer() { CustomerID = 2, FirstName = "Rachel", LastName = "Snyder", BirthDate = Convert.ToDateTime("1952-05-06"), Email = "rsnyder1@dot.gov", Address = "073 Armistice Plaza" },
    //    new Model.Customer() { CustomerID = 3, FirstName = "Charles", LastName = "Fernandez", BirthDate = Convert.ToDateTime("1968-09-11"), Email = "cfernandez2@taobao.com", Address = "11 Oak Junction" },
    //    new Model.Customer() { CustomerID = 4, FirstName = "Cheryl", LastName = "Black", BirthDate = Convert.ToDateTime("1968-10-01"), Email = "cblack3@newyorker.com", Address = "8 Katie Parkway" },
    //    new Model.Customer() { CustomerID = 5, FirstName = "Adam", LastName = "Johnson", BirthDate = Convert.ToDateTime("1945-09-25"), Email = "ajohnson4@vinaora.com", Address = "528 Glendale Park" },
    //    new Model.Customer() { CustomerID = 6, FirstName = "Jane", LastName = "Turner", BirthDate = Convert.ToDateTime("1960-04-18"), Email = "jturner5@illinois.edu", Address = "1 Parkside Street" },
    //    new Model.Customer() { CustomerID = 7, FirstName = "Joshua", LastName = "Griffin", BirthDate = Convert.ToDateTime("1951-12-17"), Email = "jgriffin6@senate.gov", Address = "576 Melody Trail" },
    //    new Model.Customer() { CustomerID = 8, FirstName = "Doris", LastName = "Hanson", BirthDate = Convert.ToDateTime("1926-08-05"), Email = "dhanson7@skyrock.com", Address = "69 Harper Circle" },
    //    new Model.Customer() { CustomerID = 9, FirstName = "Betty", LastName = "Carter", BirthDate = Convert.ToDateTime("1935-06-09"), Email = "bcarter8@odnoklassniki.ru", Address = "896 Thackeray Circle" },
    //    new Model.Customer() { CustomerID = 10, FirstName = "First Name", LastName = "Last Name", BirthDate = Convert.ToDateTime("1962-05-09"), Email = "Test@gmail.com", Address = "45 Berry street " }
    //        );
    //    base.Seed(context);
    //}
    #endregion

    public class EFContext : DbContext
    {
        public EFContext()
            : base("name=cnStr")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Example of using FluentAPI
            //modelBuilder.Entity<Customer>()
            //     .HasMany<Customer>(c => c.Customer)
            //     .WithRequired(cus => cus.Country)
            //     .HasForeignKey(cus => cus.CountryID)
            //     .WillCascadeOnDelete(false);

        }

        public virtual DbSet<Customer> Customers { get; set; }
    }

}