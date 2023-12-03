using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class AntiAircraftSystem : ResourcePointModel
    {
        public AntiAircraftSystem()
        {
            MaxAmmoCapacity = 8;
            AmmoLeft = MaxAmmoCapacity;
            RangeRadius = 5500;
            MaxCeilingRange = 3500;
        }

        public AntiAircraftSystem(string name, Location location) : base(name, location)
        {
            MaxAmmoCapacity = 8;
            AmmoLeft = MaxAmmoCapacity;
            RangeRadius = 5500;
            MaxCeilingRange = 3500;
        }

        public AntiAircraftSystem(string name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            RangeRadius = 5500;
            MaxCeilingRange = 3500;
            MaxAmmoCapacity = 8;
            AmmoLeft = MaxAmmoCapacity;
        }

        private double _RangeRadius;
        public double RangeRadius
        {
            get { return _RangeRadius; }
            set
            {
                if(_RangeRadius != value)
                {
                    _RangeRadius = value;
                    RaisePropertyChanged(nameof(RangeRadius));
                }
            }

        }

        private double _MaxCeilingRange;

        public double MaxCeilingRange
        {
            get { return _MaxCeilingRange; }
            set
            {
                if(value != _MaxCeilingRange)
                {
                    _MaxCeilingRange = value;
                    RaisePropertyChanged(nameof(MaxCeilingRange));
                }
            }
        }

        public override string ShowInformation()
        {
            return string.Format($"Zasięg systemu: {RangeRadius}m\r\n" +
                $"Max pułap celu: {MaxCeilingRange}m");
        }

        public override void MainFunction()
        {
            base.MainFunction();
            var d = Distance(SelectedHostileObjects[0]);
            if (RangeRadius > Distance(SelectedHostileObjects[0]) && AmmoContainer.Any())
            {
                FireCommand(0);
            }

        }

    }
}
