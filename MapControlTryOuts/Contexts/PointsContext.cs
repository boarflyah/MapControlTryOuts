using CoreClassesLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Contexts
{
    public class PointsContext : DbContext
    {
        public PointsContext() { }

        public DbSet<BasePointModel> Points { get; set; }
        public DbSet<ResourcePointModel> Resources { get; set; }
        public DbSet<BaseAmmoModel> Ammunition { get; set; }
        public DbSet<AntiAircraftSystem> AntiAircraftSystems { get; set; }
        public DbSet<ErraticMiningVehicleBaobab> Vehicles { get; set; }
        public DbSet<HostileObjectModel> Hostiles { get; set; }
    }
}
