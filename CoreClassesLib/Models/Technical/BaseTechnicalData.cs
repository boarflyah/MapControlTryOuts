using CoreClassesLib.AbstractsAndInterfaces;
using CoreClassesLib.Models.Points;
using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Technical
{
    public class BaseTechnicalData : BaseNotifyPropertyChanged
    {
        public BaseTechnicalData() { }

        public BaseTechnicalData(bool isSelfPropelled)
        {
            _IsSelfPropelled = IsSelfPropelled;

        }

        //[ForeignKey(nameof(BaseMovingObject))]
        public int BaseTechnicalDataId { get; set; }

        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        private int _MaxAmmoCapacity;
        public int MaxAmmoCapacity
        {
            get { return _MaxAmmoCapacity; }
            set
            {
                if (_MaxAmmoCapacity != value)
                {
                    _MaxAmmoCapacity = value;
                    RaisePropertyChanged(nameof(MaxAmmoCapacity));
                }
            }
        }

        private bool _IsHostileNeeded;
        public bool IsHostileNeeded
        {
            get => _IsHostileNeeded;
            set
            {
                if (_IsHostileNeeded != value)
                {
                    _IsHostileNeeded = value;
                    RaisePropertyChanged(nameof(IsHostileNeeded));
                }
            }
        }

        private bool _MultiTargetHandling;
        public bool MultiTargetHandling
        {
            get => _MultiTargetHandling;
            set
            {
                if (_MultiTargetHandling != value)
                {
                    _MultiTargetHandling = value;
                    RaisePropertyChanged(nameof(MultiTargetHandling));
                }
            }
        }

        private int _MaxTargetsHandling;

        public int MaxTargetsHandling
        {
            get => _MaxTargetsHandling;
            set
            {
                if (_MaxTargetsHandling != value)
                {
                    _MaxTargetsHandling = value;
                    RaisePropertyChanged(nameof(MaxTargetsHandling));
                }
            }
        }

        private bool _IsSelfPropelled;

        public bool IsSelfPropelled
        {
            get => _IsSelfPropelled;
            set
            {
                if (value != _IsSelfPropelled)
                {
                    _IsSelfPropelled = value;
                    RaisePropertyChanged(nameof(IsSelfPropelled));
                }
            }
        }

        private int _IconInt;

        public int IconInt
        {
            get => _IconInt;
            set
            {
                if (value != _IconInt)
                {
                    _IconInt = value;
                    RaisePropertyChanged(nameof(IconInt));
                }
            }
        }

        [NotMapped]
        public virtual string FullDetails => string.Empty;

        //public virtual BaseMovingObject BaseMovingObject { get; set; }

        //public virtual double OnObjectMoved(Location previousLocation, Location newLocation)
        //{
        //    double FuelLeft = 0;
        //    double distanceTravelled = previousLocation.GetDistance(newLocation);
        //    if (distanceTravelled > RangeLeft)
        //    {
        //        FuelLeft = 0;
        //        return false;
        //    }
        //    FuelLeft -= (distanceTravelled / 1000) * FuelConsumptionPerKm;
        //    return true;
        //}

    }
}
