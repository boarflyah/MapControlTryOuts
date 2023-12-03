using CoreClassesLib.Models.Resources.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Fixed
{
    public class WarehouseObject : ResourceObject
    {
        public WarehouseObject()
        {
            Assets = new();
        }

        public WarehouseObject(string name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            Assets = new();
        }

        public ObservableCollection<BaseFixedAsset> Assets { get; set; }

    }
}
