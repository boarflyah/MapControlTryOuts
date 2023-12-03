using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Technical
{
    public class AmmoTechnicalData : MovingResourceResourceTechnicalData
    {
        public AmmoTechnicalData()
        {
        }

        public AmmoTechnicalData(bool isSelfPropelled) : base(isSelfPropelled)
        {
        }

        public AmmoTechnicalData(double fuelTank, double fuelLeft, double speed, bool canRefuel, double fuelConsumptionPerKm) : base(fuelTank, fuelLeft, speed, canRefuel, fuelConsumptionPerKm)
        {
        }

        private double _WarheadPower;

        public double WarheadPower
        {
            get => _WarheadPower;
            set
            {
                if (_WarheadPower != value)
                {
                    _WarheadPower = value;
                    RaisePropertyChanged(nameof(WarheadPower));
                }
            }
        }

        public override string FullDetails => IsSelfPropelled ? string.Format
                    ($"Zasięg: {FuelTank / FuelConsumptionPerKm:n2}km\r\n" +
                    $"Prędkość: {Speed}m/s") : "Brak napędu";
    }
}
