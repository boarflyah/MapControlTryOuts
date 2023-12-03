using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Technical;
using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class MovingResourceObject : ResourceObject
    {
        public MovingResourceObject() : base()
        {
            RangeLeft = GetRangeLeft();
        }

        public MovingResourceObject(string name, Location location)// : base(name, location)
        {
        }

        public MovingResourceObject(string name, double latitude, double longitude, bool isHostileNeeded) : base(name, latitude, longitude)
        {
            //_IsHostileNeeded = isHostileNeeded;
        }

        private double _FuelLeft;
        public double FuelLeft
        {
            get => _FuelLeft;
            set
            {
                if (_FuelLeft != value)
                {
                    _FuelLeft = value;
                    RaisePropertyChanged(nameof(FuelLeft));
                }
            }
        }

        private double _RangeLeft;
        /// <summary>
        /// Range, according to current fuel level, in meters
        /// </summary>
        [NotMapped]
        public double RangeLeft
        {
            get => _RangeLeft;
            set
            {
                if (value != _RangeLeft)
                {
                    _RangeLeft = value;
                    RaisePropertyChanged(nameof(RangeLeft));
                }
            }
        }

        protected override void OnPropertyChanged(string property, object oldValue = null, object newValue = null)
        {
            if (property == nameof(FuelLeft) || property == nameof(TechnicalData))
                RangeLeft = GetRangeLeft();
            if (property == nameof(Point))
                ;
        }

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

        private double GetRangeLeft() => MovingResourceTechnicalData?.FuelConsumptionPerKm != 0 && TechnicalData.IsSelfPropelled ?
                                            FuelLeft / MovingResourceTechnicalData.FuelConsumptionPerKm * 1000 : 0;

        [NotMapped]
        public MovingResourceResourceTechnicalData MovingResourceTechnicalData => TechnicalData as MovingResourceResourceTechnicalData;
    }
}

