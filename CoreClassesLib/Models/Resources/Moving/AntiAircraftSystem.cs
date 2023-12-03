using CoreClassesLib.Models.Points;
using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class AntiAircraftSystem : RangedResourceModel
    {
        public AntiAircraftSystem()
        {
        }

        public AntiAircraftSystem(string name, Location location, double maxCeilingRange, double rangeRadius) : base(name, location, rangeRadius, maxCeilingRange)
        {
            //MaxAmmoCapacity = 8;
            //MultiTargetHandling = false;
        }

        public AntiAircraftSystem(string name, double latitude, double longitude, bool isHostileNeeded, double rangeRadius, double maxCeilingRange) : base(name, latitude, longitude, isHostileNeeded, rangeRadius, maxCeilingRange)
        {
            //MaxAmmoCapacity = 8;
            //MultiTargetHandling = false;
        }

        //public override void MainFunction()
        //{
        //    base.MainFunction();
        //    var d = Distance(SelectedHostileObjects[0]);
        //    if (RangeRadius > Distance(SelectedHostileObjects[0]) && AmmoContainer.Any())
        //    {
        //        FireCommand();
        //    }

        //}

        protected override ExplosionPointModel FireCommand(HostileObjectModel hostile, BaseAmmoModel fired)
        {
            if (fired != null && hostile != null)
            {
                fired.Point.Visible = true;
                double distance,
                    distanceTraveled = 0,
                    resourceRange = RangedResourceTechnicalData.RangeRadius;
                do
                {
                    Thread.Sleep(500);
                    fired.Point.Location = fired.MovePointTowards(hostile.Point, fired.AmmoTechnicalData.Speed, 500, ref distanceTraveled);
                    distance = fired.Point.Distance(hostile.Point);
                }
                while (distance > 50 && distanceTraveled < resourceRange);
                if (distance <= 50 && distanceTraveled < resourceRange)
                {
                    fired.Point.Visible = false;
                    hostile.AntiAircraftResistency = 0;
                    ExplosionPointModel tempExp = new();
                    tempExp.Name = "Hit";
                    tempExp.Point.Location = new Location(hostile.Point.Location.Latitude, hostile.Point.Location.Longitude);
                    tempExp.HealthTaken = new Random().Next(hostile.AntiAircraftResistency, (int)hostile.Health + hostile.AntiAircraftResistency) - hostile.AntiAircraftResistency;
                    if (hostile.Health - tempExp.HealthTaken <= 20)
                        tempExp.IsTargetDestroyed = true;
                    return tempExp;
                }
            }
            return null;
        }

    }
}
