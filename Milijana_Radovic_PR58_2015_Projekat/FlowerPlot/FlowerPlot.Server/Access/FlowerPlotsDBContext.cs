using FlowerPlot.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server.Access
{
    class FlowerPlotsDBContext : DbContext
    {
        public FlowerPlotsDBContext() : base("dbConnection2016")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FlowerPlotsDBContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plot> FlowerPlots { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<SoilType> SoilTypes { get; set; }

        //public void FunctionCheck()
        //{
        //    foreach (var item in Users.ToList())
        //    {
        //        if (item.Username == "admin")
        //        {
        //            return;
        //        }
        //    }
        //    Users.Add(new User
        //    {
        //        Name = "admin",
        //        Password = "admin",
        //        Role = "admin",
        //        Username = "admin",
        //        Surname = "admin"
        //    });
        //    SaveChanges();
        //}
    }
}
