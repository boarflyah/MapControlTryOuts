using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Assets;
using CoreClassesLib.Models.Resources.Moving;
using CoreClassesLib.Models.Technical;
using System.Data.Entity;

namespace CoreClassesLib.Contexts
{
    public interface IPointsContext
    {
        DbSet<BaseAmmoModel> Ammunition { get; set; }
        DbSet<BaseFixedAsset> Assets { get; set; }
        DbSet<HostileObjectModel> Hostiles { get; set; }
        DbSet<BasePointModel> Points { get; set; }
        DbSet<ResourceObject> Resources { get; set; }
        DbSet<BaseTechnicalData> TechnicalDatas { get; set; }
        DbSet<Command> Commands { get; set; }

        public int CommitChanges();
    }
}