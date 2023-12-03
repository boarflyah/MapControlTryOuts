using CoreClassesLib.AbstractsAndInterfaces;
using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Points
{
    public class HostileObjectModel : BaseObject, IMovingObject
    {
        public HostileObjectModel()
        {
            LatestTrackTime = DateTime.Now;
        }

        public HostileObjectModel(string? name, Location? location) //: base(name, location)
        {
            LatestTrackTime = DateTime.Now;
        }

        public HostileObjectModel(string? name, double latitude, double longitude, double speed, double altitude) : base(name, latitude, longitude)
        {
            AntiAircraftResistency = 30;
            Health = 100;
            Speed = speed;
            Point.Altitude = altitude;
            Point.Visible = false;
            LatestTrackTime = DateTime.Now;
            //TechnicalData.Speed = speed;
        }

        private int _AntiAircraftResistency;

        public int AntiAircraftResistency
        {
            get { return _AntiAircraftResistency; }
            set
            {
                if (_AntiAircraftResistency != value)
                {
                    _AntiAircraftResistency = value;
                    RaisePropertyChanged(nameof(AntiAircraftResistency));
                }
            }
        }

        private double _Health;
        public double Health
        {
            get { return _Health; }
            set
            {
                if (Health != value)
                {
                    _Health = value;
                    RaisePropertyChanged(nameof(Health));
                }
            }
        }

        private double _Speed;

        public double Speed
        {
            get => _Speed;
            set
            {
                if (Speed != value)
                {
                    _Speed = value;
                    RaisePropertyChanged(nameof(Speed));
                }
            }
        }

        private DateTime _LatestTrackTime;
        [NotMapped]
        public DateTime LatestTrackTime
        {
            get => _LatestTrackTime;
            set
            {
                if (_LatestTrackTime != value)
                {
                    _LatestTrackTime = value;
                    RaisePropertyChanged(nameof(LatestTrackTime));
                }
            }
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

    }
}
