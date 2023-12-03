using CoreClassesLib.AbstractsAndInterfaces;
using CoreClassesLib.Models.Resources.Fixed;
using CoreClassesLib.Models.Technical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Assets
{
    public abstract class BaseFixedAsset : BaseNotifyPropertyChanged
    {
        public BaseFixedAsset() : base() { }

        public int Id { get; set; }

        private BaseTechnicalData _TechnicalData;
        public BaseTechnicalData TechnicalData
        {
            get => _TechnicalData;
            set
            {
                if (_TechnicalData != value)
                {
                    _TechnicalData = value;
                    RaisePropertyChanged(nameof(TechnicalData));
                }
            }
        }

        private double _Quantity;

        public double Quantity
        {
            get => _Quantity;
            set
            {
                if (value != _Quantity)
                {
                    _Quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public virtual WarehouseObject Warehouse { get; set; }

    }
}
