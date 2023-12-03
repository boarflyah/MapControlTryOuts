using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class HostileObjectModel : BasePointModel
    {
        public HostileObjectModel()
        {
        }

        public HostileObjectModel(string? name, Location? location) : base(name, location)
        {
        }

        public HostileObjectModel(string? name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            AntiAircraftResistency = 30;
            Health = 100;
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


    }
}
