using CoreClassesLib.Contexts;
using CoreClassesLib.Models;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources.Assets;
using CoreClassesLib.Models.Resources.Fixed;
using CoreClassesLib.Models.Resources.Moving;
using CoreClassesLib.Models.Technical;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlConsole
{
    public static class Updater
    {
        public static int InitializeResources(PointsContext context)
        {
            if (context.Resources.Any(x => x.Name.Equals("Pojazd minowania Baobab")) == false)
            {
                var td = new MovingResourceResourceTechnicalData(200, 200, 60000 / 3600, true, 0.5);
                td.Name = "Pojazd minowania Baobab";
                td.MaxAmmoCapacity = 60;
                td.IsSelfPropelled = true;
                td.IsHostileNeeded = false;
                td.MultiTargetHandling = false;
                td.MaxTargetsHandling = 0;
                td.CanRefuel = true;
                td.FuelTank = 200;
                td.FuelConsumptionPerKm = 0.5;
                td.Speed = 60000 / 3600;
                ErraticMiningVehicle baobab1 = new ErraticMiningVehicle("Pojazd minowania Baobab", 53.6, 14.9, false);
                baobab1.TechnicalData = context.TechnicalDatas.Add(td);
                baobab1.FuelLeft = td.FuelTank;
                context.Resources.Add(baobab1);
            }
            if (context.Resources.Any(x => x.Name.Equals("System przeciwlotniczy Poprad")) == false)
            {
                var td = new RangedResourceTechnicalData();
                td.Name = "System przeciwlotniczy Poprad";
                td.CanRefuel = true;
                td.FuelTank = 130;
                td.RangeRadius = 5500;
                td.MaxCeilingRange = 3500;
                td.FuelConsumptionPerKm = 0.25;
                td.IsHostileNeeded = true;
                td.IsSelfPropelled = true;
                td.MaxAmmoCapacity = 8;
                td.MaxTargetsHandling = 1;
                td.MultiTargetHandling = false;
                td.Speed = 80000 / 3600;
                AntiAircraftSystem poprad1 = new AntiAircraftSystem("System przeciwlotniczy Poprad", 50.055, 22.01, true, 5500, 3500);
                poprad1.TechnicalData = context.TechnicalDatas.Add(td);
                poprad1.FuelLeft = td.FuelTank;
                context.Resources.Add(poprad1);
            }
            if (context.Resources.Any(x => x.Name.Equals("ZDPSR Soła")) == false)
            {
                var td = new RangedResourceTechnicalData();
                td.Name = "ZDPSR Soła";
                td.CanRefuel = true;
                td.FuelTank = 100;
                td.RangeRadius = 40000;
                td.MaxCeilingRange = 8000;
                td.FuelConsumptionPerKm = 0.16;
                td.IsHostileNeeded = false;
                td.IsSelfPropelled = true;
                td.MaxAmmoCapacity = 0;
                td.MaxTargetsHandling = 4;
                td.MultiTargetHandling = true;
                td.Speed = 80000 / 3600;
                RadarSystem sola1 = new RadarSystem("ZDPSR Soła", 50.06, 22.01, false, 40000, 8000);
                sola1.TechnicalData = context.TechnicalDatas.Add(td);
                sola1.FuelLeft = td.FuelTank;
                context.Resources.Add(sola1);
            }
            if(context.Resources.Any(x => x.Name.Equals("Magazyn Ammunicyjny Północ")) == false)
            {
                var td = new WarehouseTechnicalData();
                td.MaxAmmoCapacity = 100000;
                td.MaxFuelCapacity = 0;
                //td.MaxAmmoCapacity = 100000;
                td.Name = "Magazyn Amunicyjny";
                WarehouseObject warehouseSouth = new WarehouseObject("Magazyn Ammunicyjny Północ", 53.48, 15.7);
                warehouseSouth.TechnicalData = td;
                context.Resources.Add(warehouseSouth);
            }
            if (context.Resources.Any(x => x.Name.Equals("Magazyn Ammunicyjny Południe")) == false)
            {
                var td = new WarehouseTechnicalData();
                td.MaxAmmoCapacity = 100000;
                td.MaxFuelCapacity = 0;
                //td.MaxAmmoCapacity = 100000;
                td.Name = "Magazyn Amunicyjny";
                WarehouseObject warehouseSouth = new WarehouseObject("Magazyn Ammunicyjny Południe", 50.4, 22.03);
                warehouseSouth.TechnicalData = td;
                context.Resources.Add(warehouseSouth);
            }
            if (context.Hostiles.Any(x => x.Name.Equals("Obcy 1")) == false)
            {
                context.Hostiles.Add(new HostileObjectModel("Obcy 1", 50.07, 22.03, 500, 2000));
            }
            if (context.Hostiles.Any(x => x.Name.Equals("Obcy 2")) == false)
            {
                context.Hostiles.Add(new HostileObjectModel("Obcy 2", 50.15, 22, 300, 3200));
            }
            if (context.Hostiles.Any(x => x.Name.Equals("Obcy 3")) == false)
            {
                context.Hostiles.Add(new HostileObjectModel("Obcy 3", 50.03, 22.02, 700, 7800));
            }
            if (context.Hostiles.Any(x => x.Name.Equals("Obcy 4")) == false)
            {
                context.Hostiles.Add(new HostileObjectModel("Obcy 4", 52.5, 21.03, 380, 9000));
            }
            return context.SaveChanges();
        }

        public static int InitializeAmmoContainers(PointsContext context)
        {
            var td = new AmmoTechnicalData();
            td.Name = "Miny do PMN Baobab";
            td.IsSelfPropelled = false;
            td.CanRefuel = false;
            td.FuelTank = 0;
            td.WarheadPower = 60;
            td.IconInt = 101;
            BaseTechnicalData erratic = context.TechnicalDatas.Add(td);

            var aat = new AmmoTechnicalData();
            aat.Name = "PPZR Grom";
            aat.CanRefuel = false;
            aat.FuelTank = 5.5;
            aat.FuelConsumptionPerKm = 1;
            aat.IsSelfPropelled = true;
            aat.Speed = 500;
            aat.WarheadPower = 100;
            aat.IconInt = 1;
            BaseTechnicalData aatData = context.TechnicalDatas.Add(aat);

            foreach (var item in context.Resources.Where(x => x is WarehouseObject == false && x.TechnicalData != null && x.TechnicalData.MaxAmmoCapacity > 0 && x.AmmoContainer.Count == 0))
                for (int i = 0; i < item.TechnicalData.MaxAmmoCapacity; i++)
                {
                    BaseAmmoModel bam = new(item);
                    //bam.Point = new BasePointModel();
                    if (item is ErraticMiningVehicle)
                    {
                        //bam.Point.IconInt = 101;
                        bam.Point.IconSize = 10;
                        bam.TechnicalData = erratic;
                    }
                    if (item is AntiAircraftSystem)
                    {
                        //bam.Point.IconInt = 1;
                        bam.TechnicalData = aat;
                    }
                    item.AmmoContainer.Add(bam);
                    context.Ammunition.Add(bam);
                }

            var warehouse = context.Resources.FirstOrDefault(x => x.Name == "Magazyn Ammunicyjny Północ") as WarehouseObject;
            if (warehouse != null)
            {
                AmmoAsset aa = new ();
                aa.TechnicalData = aat;
                aa.Quantity = 200;
                AmmoAsset am = new();
                am.TechnicalData = td;
                am.Quantity = 4000;
                warehouse.Assets.Add(aa);
                warehouse.Assets.Add(am);
            }

            var warehouse2 = context.Resources.FirstOrDefault(x => x.Name == "Magazyn Ammunicyjny Południe") as WarehouseObject;
            if (warehouse2 != null)
            {
                AmmoAsset aa = new();
                aa.TechnicalData = aat;
                aa.Quantity = 100;
                AmmoAsset am = new();
                am.TechnicalData = td;
                am.Quantity = 1000;
                warehouse2.Assets.Add(aa);
                warehouse2.Assets.Add(am);
            }


            //var warehouse = context.Resources.FirstOrDefault(x => x is WarehouseObject && x.TechnicalData != null && x.TechnicalData.MaxAmmoCapacity > 0 && x.AmmoContainer.Count == 0);
            //if (warehouse != null)
            //{
            //    for (int i = 0; i < 600; i++)
            //    {
            //        BaseAmmoModel bam = new(warehouse);
            //        bam.TechnicalData = erratic;
            //        bam.Point.IconSize = 10;
            //        warehouse.AmmoContainer.Add(bam);
            //        context.Ammunition.Add(bam);
            //    }
            //    for (int i = 0; i < 400; i++)
            //    {
            //        BaseAmmoModel bam = new(warehouse);
            //        bam.TechnicalData = aat;
            //        warehouse.AmmoContainer.Add(bam);
            //        context.Ammunition.Add(bam);
            //    }
            //}

            return context.SaveChanges();
        }
    }
}
