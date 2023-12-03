using CoreClassesLib.AbstractsAndInterfaces;
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
    public class BaseAmmoModel : ResourceObject, IMovingObject
    {
        public BaseAmmoModel() { }

        public BaseAmmoModel(ResourceObject resource)
        {
            Point = new();
            Point.Location = resource.Point.Location;
            Point.Visible = false;
            Resource = resource;
            //Speed = 500;
        }

        public BaseAmmoModel(string? name, Location? location)// : base(name, location)
        {
            Point = new();
            Point.Visible = false;
        }

        public BaseAmmoModel(string? name, double latitude, double longitude)// : base(name, latitude, longitude)
        {
            Point = new();
            Point.Visible = false;
        }

        private bool _Fired = false;
        [NotMapped]
        public bool Fired
        {
            get { return _Fired; }
            set
            {
                if (value != _Fired)
                {
                    _Fired = value;
                    RaisePropertyChanged(nameof(Fired));
                }
            }
        }

        public virtual ResourceObject Resource { get; set; }

        [NotMapped]
        public AmmoTechnicalData AmmoTechnicalData => TechnicalData as AmmoTechnicalData;

        #region IMovingObject implementation

        public Location MovePointTowards(BasePointModel targetPoint, double mps, double ms, ref double distanceTravelled)
        {
            var lat1 = Point.Location.Latitude * Math.PI / 180d;
            var lon1 = Point.Location.Longitude * Math.PI / 180d;
            var lat2 = targetPoint.Location.Latitude * Math.PI / 180d;
            var lon2 = targetPoint.Location.Longitude * Math.PI / 180d;
            var cosLat1 = Math.Cos(lat1);
            var sinLat1 = Math.Sin(lat1);
            var cosLat2 = Math.Cos(lat2);
            var sinLat2 = Math.Sin(lat2);
            var cosLon12 = Math.Cos(lon2 - lon1);
            var sinLon12 = Math.Sin(lon2 - lon1);
            double azimuth = Math.Atan2(sinLon12, cosLat1 * sinLat2 / cosLat2 - sinLat1 * cosLon12);

            azimuth = azimuth * (180 / Math.PI);
            double betweenPoints = Point.Location.GetDistance(targetPoint.Location);
            if (betweenPoints < mps * ms / 1000)
            {
                distanceTravelled += betweenPoints;
                return Point.Location.GetLocation(azimuth, betweenPoints);
            }
            else
            {
                distanceTravelled += mps * ms / 1000;
                return Point.Location.GetLocation(azimuth, mps * ms / 1000);
            }
        }

        public Location MovePointTowards(Location location, double distanceInMeters)
        {
            var lat1 = Point.Location.Latitude * Math.PI / 180d;
            var lon1 = Point.Location.Longitude * Math.PI / 180d;
            var lat2 = location.Latitude * Math.PI / 180d;
            var lon2 = location.Longitude * Math.PI / 180d;
            var cosLat1 = Math.Cos(lat1);
            var sinLat1 = Math.Sin(lat1);
            var cosLat2 = Math.Cos(lat2);
            var sinLat2 = Math.Sin(lat2);
            var cosLon12 = Math.Cos(lon2 - lon1);
            var sinLon12 = Math.Sin(lon2 - lon1);
            double azimuth = Math.Atan2(sinLon12, cosLat1 * sinLat2 / cosLat2 - sinLat1 * cosLon12);

            azimuth = azimuth * (180 / Math.PI);
            double betweenPoints = Point.Location.GetDistance(location);
            if (betweenPoints < distanceInMeters)
                return Point.Location.GetLocation(azimuth, betweenPoints);
            else
                return Point.Location.GetLocation(azimuth, distanceInMeters);
        }

        #endregion 

        public override string ShowInformation()
        {
            if (AmmoTechnicalData != null && AmmoTechnicalData.IsSelfPropelled)
                return string.Format($"Zasięg: {AmmoTechnicalData.FuelTank / AmmoTechnicalData.FuelConsumptionPerKm:n2}km\r\n" +
                    $"Prędkość: {AmmoTechnicalData.Speed}m/s");
            else
                return "Brak napędu";
        }
    }
}
