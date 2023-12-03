using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Technical
{
    public class RangedResourceTechnicalData : MovingResourceResourceTechnicalData
    {
        public RangedResourceTechnicalData() { }

        private double _RangeRadius;
        public double RangeRadius
        {
            get => _RangeRadius;
            set
            {
                if (_RangeRadius != value)
                {
                    _RangeRadius = value;
                    RaisePropertyChanged(nameof(RangeRadius));
                }
            }
        }

        private double _MaxCeilingRange;
        public double MaxCeilingRange
        {
            get => _MaxCeilingRange;
            set
            {
                if (_MaxCeilingRange != value)
                {
                    _MaxCeilingRange = value;
                    RaisePropertyChanged(nameof(MaxCeilingRange));
                }
            }
        }
    }
}
