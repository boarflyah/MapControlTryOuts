using CoreClassesLib.Models.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Technical
{
    public class MovingResourceResourceTechnicalData : BaseTechnicalData
    {
        public MovingResourceResourceTechnicalData()
        {
        }

        public MovingResourceResourceTechnicalData(bool isSelfPropelled) : base(isSelfPropelled)
        {
            IsSelfPropelled = isSelfPropelled;
        }

        public MovingResourceResourceTechnicalData(double fuelTank, double fuelLeft, double speed, bool canRefuel, double fuelConsumptionPerKm) : base()
        {
            _CanRefuel = false;
            _FuelTank = 0;
            //_FuelLeft = 0;
            _Speed = 0;
            _FuelConsumptionPerKm = 0;
            IsSelfPropelled = true;
        }

        private double _FuelTank;
        public double FuelTank
        {
            get => _FuelTank;
            set
            {
                if (_FuelTank != value)
                {
                    _FuelTank = value;
                    RaisePropertyChanged(nameof(FuelTank));
                }
            }
        }

        private double _FuelConsumptionPerKm;
        public double FuelConsumptionPerKm
        {
            get => _FuelConsumptionPerKm;
            set
            {
                if (value != _FuelConsumptionPerKm)
                {
                    _FuelConsumptionPerKm = value;
                    RaisePropertyChanged(nameof(FuelConsumptionPerKm));
                }
            }
        }

        private double _Speed;
        /// <summary>
        /// Speed in mps
        /// </summary>
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

        private bool _CanRefuel;
        public bool CanRefuel
        {
            get => _CanRefuel;
            set
            {
                if (_CanRefuel != value)
                {
                    _CanRefuel = value;
                    RaisePropertyChanged(nameof(CanRefuel));
                }
            }
        }
    }
}
