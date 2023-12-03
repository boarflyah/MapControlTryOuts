using MapControl;
using CoreClassesLib.AbstractsAndInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using CoreClassesLib.Models.Technical;

namespace CoreClassesLib.Models.Points
{
    public class BasePointModel : BaseNotifyPropertyChanged
    {
        public BasePointModel(Location? location)
        {
            //_Name = name;
            _Location = location;
            _Visible = true;
            //_Speed = 0;
            //_Altitude = 0;
            //_IconInt = 0;
            _IconSize = 20;
            _Altitude = 0;
        }

        public BasePointModel(double latitude, double longitude)
        {
            //_Name = name;
            _Location = new(latitude, longitude);
            _Visible = true;
            //_Speed = 0;
            //_Altitude = 0;
            //_IconInt = 0;
            _IconSize = 20;
            _Altitude = 0;
        }

        public BasePointModel()
        {
            //_Name = "";
            _Location = new();
            _Visible = true;
            //_Speed = 0;
            //_Altitude = 0;
            //_IconInt = 0;
            _IconSize = 20;
            _Altitude = 0;
        }

        [ForeignKey(nameof(BaseObject))]
        public int BasePointModelId { get; set; }

        //private string? _Name;
        //public string Name
        //{
        //    get => _Name;
        //    set
        //    {
        //        if (_Name != value)
        //        {
        //            RaisePropertyChanged(nameof(Name), _Name, value);
        //            _Name = value;
        //        }
        //    }
        //}

        private Location? _Location;
        public Location Location
        {
            get => _Location;
            set
            {
                if (_Location != value)
                {
                    //if(TechnicalData != null && TechnicalData.IsSelfPropelled && TechnicalData.OnObjectMoved(Location, value) == false)
                    //    _Location = MovePointTowards(value, TechnicalData.RangeLeft);
                    //else
                    _Location = value;
                    RaisePropertyChanged(nameof(Location));
                }
            }
        }

        private double _Altitude;
        public double Altitude
        {
            get => _Altitude;
            set
            {
                if (Altitude != value)
                {
                    _Altitude = value;
                    RaisePropertyChanged(nameof(Altitude));
                }
            }
        }

        private bool _Visible;
        public bool Visible
        {
            get { return _Visible; }
            set
            {
                if (_Visible != value)
                {
                    _Visible = value;
                    RaisePropertyChanged(nameof(Visible));
                }
            }
        }

        private bool _IsSelected = false;
        [NotMapped]
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    RaisePropertyChanged(nameof(IsSelected));
                }
            }
        }


        private bool _IsVisibleSelected = false;
        [NotMapped]
        public bool IsVisibleSelected
        {
            get => _IsVisibleSelected;
            set
            {
                if (_IsVisibleSelected != value)
                {
                    _IsVisibleSelected = value;
                    RaisePropertyChanged(nameof(IsVisibleSelected));
                }
            }
        }

        //private double _Speed;
        //public double Speed
        //{
        //    get => _Speed;
        //    set
        //    {
        //        if (Speed != value)
        //        {
        //            _Speed = value;
        //            RaisePropertyChanged(nameof(Speed));
        //        }
        //    }
        //}

        //private double _Altitude;
        //public double Altitude
        //{
        //    get => _Altitude;
        //    set
        //    {
        //        if (Altitude != value)
        //        {
        //            _Altitude = value;
        //            RaisePropertyChanged(nameof(Altitude));
        //        }
        //    }
        //}

        //private int _IconInt;
        //public int IconInt
        //{
        //    get => _IconInt;
        //    set
        //    {
        //        if (IconInt != value)
        //        {
        //            _IconInt = value;
        //            RaisePropertyChanged(nameof(IconInt));
        //        }
        //    }
        //}

        private int _IconSize;
        public int IconSize
        {
            get => _IconSize;
            set
            {
                if (IconSize != value)
                {
                    _IconSize = value;
                    RaisePropertyChanged(nameof(IconSize));
                }
            }
        }

        //private BaseTechnicalData _BaseTechnicalData;

        //public BaseTechnicalData TechnicalData
        //{
        //    get => _BaseTechnicalData;
        //    set
        //    {
        //        if (_BaseTechnicalData != value)
        //        {
        //            _BaseTechnicalData = value;
        //            RaisePropertyChanged(nameof(_BaseTechnicalData));
        //        }
        //    }
        //}

        public virtual BaseObject BaseObject { get; set; }

        //public string TextDetails => ShowInformation();

        //public string FullInformation => ShowInformation();

        public virtual void MainFunction() { }

        //public virtual string ShowInformation() { return ""; }

        public double Distance(BasePointModel point) => Location.GetDistance(point.Location);

        public double Distance(Location location) => Location.GetDistance(location);

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (property == nameof(IsSelected) || property == nameof(Visible))
                IsVisibleSelected = IsSelected && Visible ? true : false;
        }

        public Location MovePointTowards(BasePointModel targetPoint, double mps, double ms, ref double distanceTravelled)
        {
            var lat1 = Location.Latitude * Math.PI / 180d;
            var lon1 = Location.Longitude * Math.PI / 180d;
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
            double betweenPoints = Location.GetDistance(targetPoint.Location);
            if (betweenPoints < mps * ms / 1000)
            {
                distanceTravelled += betweenPoints;
                return Location.GetLocation(azimuth, betweenPoints);
            }
            else
            {
                distanceTravelled += mps * ms / 1000;
                return Location.GetLocation(azimuth, mps * ms / 1000);
            }
        }

        public Location MovePointTowards(Location location, double distanceInMeters)
        {
            var lat1 = Location.Latitude * Math.PI / 180d;
            var lon1 = Location.Longitude * Math.PI / 180d;
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
            double betweenPoints = Location.GetDistance(location);
            if (betweenPoints < distanceInMeters)
                return Location.GetLocation(azimuth, betweenPoints);
            else
                return Location.GetLocation(azimuth, distanceInMeters);
        }

        public Location GetNewLocation(BasePointModel targetPoint, double mps, double ms)
        {
            Location pointC = new Location(Location.Latitude, targetPoint.Location.Longitude);
            double distance = mps * ms / 1000,
                lengthAB = Location.GetDistance(targetPoint.Location),
                lengthBC = targetPoint.Location.GetDistance(pointC),
                lengthAC = Location.GetDistance(pointC);

            return null;
        }
    }
}
