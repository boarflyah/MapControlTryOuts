using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Technical;
using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class RangedResourceModel : MovingResourceObject
    {
        public RangedResourceModel() { }

        public RangedResourceModel(string name, Location location, double rangeRadius, double maxCeilingRange) : base(name, location)
        {
            //RangeRadius = rangeRadius;
            //MaxCeilingRange = maxCeilingRange;
        }

        public RangedResourceModel(string name, double latitude, double longitude, bool isHostileNeeded, double rangeRadius, double maxCeilingRange) : base(name, latitude, longitude, isHostileNeeded)
        {
            //RangeRadius = rangeRadius;
            //MaxCeilingRange = maxCeilingRange;
        }

        [NotMapped]
        public RangedResourceTechnicalData RangedResourceTechnicalData => TechnicalData as RangedResourceTechnicalData;

        protected bool PointInTriangle(Location p, Location p0, Location p1, Location p2)
        {
            //X - Latitude, Y - Longitude

            var s = p0.Longitude * p2.Latitude - p0.Latitude * p2.Longitude + (p2.Longitude - p0.Longitude) * p.Latitude + (p0.Latitude - p2.Latitude) * p.Longitude;
            var t = p0.Latitude * p1.Longitude - p0.Longitude * p1.Latitude + (p0.Longitude - p1.Longitude) * p.Latitude + (p1.Latitude - p0.Latitude) * p.Longitude;

            if (s < 0 != t < 0)
                return false;

            var A = -p1.Longitude * p2.Latitude + p0.Longitude * (p2.Latitude - p1.Latitude) + p0.Latitude * (p1.Longitude - p2.Longitude) + p1.Latitude * p2.Longitude;

            return A < 0 ?
                    s <= 0 && s + t >= A :
                    s >= 0 && s + t <= A;
        }

        public override string ShowInformation()
        {
            return string.Format($"Zasięg systemu: {RangedResourceTechnicalData.RangeRadius}m\r\n" +
                $"Max pułap celu: {RangedResourceTechnicalData.MaxCeilingRange}m\r\n" +
                $"Zasięg na lądzie: {RangeLeft / 1000}km\r\n" +
                $"Prędkość: {RangedResourceTechnicalData.Speed} m/s");
        }
    }
}
