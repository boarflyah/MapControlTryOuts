using MapControl;
using MapControlTryOuts.AbstractsAndInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class BasePointModel : BaseNotifyPropertyChanged
    {
        public BasePointModel(string? name, Location? location)
        {
            _Name = name;
            _Location = location;
            _Visible = true;
        }

        public BasePointModel(string? name, double latitude, double longitude)
        {
            _Name = name;
            _Location = new(latitude, longitude);
            _Visible = true;
        }

        public BasePointModel()
        {
            _Name = "";
            _Location = new();
            _Visible = true;
        }

        public int PointId { get; set; }

        private string? _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if(_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        private Location? _Location;
        public Location Location
        {
            get => _Location;
            set
            {
                if(_Location != value)
                {
                    _Location = value;
                    RaisePropertyChanged(nameof(Location));
                }
            }
        }

        private bool _Visible;

        public bool Visible
        {
            get { return _Visible; }
            set
            {
                if (value != _Visible)
                {
                    _Visible = value;
                    RaisePropertyChanged(nameof(Visible));
                }
            }
        }

        public string TextDetails => ShowInformation();

        public string FullInformation => ShowInformation();

        public virtual void MainFunction() { }

        public virtual string ShowInformation() { return ""; }

        public double Distance(BasePointModel point)
        {
            return Location.GetDistance(point.Location);
        }

        public Location MovePointTowards(BasePointModel targetPoint, double mps, double ms)
        {
            //double azimuth = Math.Atan2(targetPoint.Location.Longitude - Location.Longitude, targetPoint.Location.Latitude - Location.Latitude); 
            double azimuth = 30;
            return Location.GetLocation(azimuth, mps * ms / 1000);
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
