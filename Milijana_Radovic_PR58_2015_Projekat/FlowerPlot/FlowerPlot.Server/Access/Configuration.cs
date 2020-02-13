using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server.Access
{
    class Configuration : DbMigrationsConfiguration<FlowerPlotsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FlowerPlotsDBContext";
        }
    }
}
