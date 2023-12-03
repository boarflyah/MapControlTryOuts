using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Technical
{
    public class WarehouseTechnicalData : BaseTechnicalData
    {
        public WarehouseTechnicalData()
        {
            IsSelfPropelled = false;
            IsHostileNeeded = false;
            MultiTargetHandling = false;
            MaxTargetsHandling = 0;
            IconInt = 201;
        }

        private int _MaxFuelCapacity;
        public int MaxFuelCapacity
        {
            get => _MaxFuelCapacity;
            set
            {
                if (_MaxFuelCapacity != value)
                {
                    _MaxFuelCapacity = value;
                    RaisePropertyChanged(nameof(MaxFuelCapacity));
                }
            }
        }


    }
}
