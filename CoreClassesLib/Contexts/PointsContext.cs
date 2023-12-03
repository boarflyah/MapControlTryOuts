using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Assets;
using CoreClassesLib.Models.Resources.Moving;
using CoreClassesLib.Models.Technical;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Contexts
{
    public class PointsContext : DbContext, IPointsContext
    {
        public PointsContext() { }

        public DbSet<BasePointModel> Points { get; set; }
        public DbSet<ResourceObject> Resources { get; set; }
        public DbSet<BaseAmmoModel> Ammunition { get; set; }
        public DbSet<HostileObjectModel> Hostiles { get; set; }
        public DbSet<BaseTechnicalData> TechnicalDatas { get; set; }
        public DbSet<BaseFixedAsset> Assets { get; set; }
        public DbSet<Command> Commands { get; set; }

        public int CommitChanges()
        {
            return SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Command>()
                .HasRequired<ResourceObject>(s => s.Executor)
                .WithMany(g => g.Commands)
                .WillCascadeOnDelete();
        }
    }
}
