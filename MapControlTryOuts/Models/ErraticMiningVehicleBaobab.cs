using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class ErraticMiningVehicleBaobab : ResourcePointModel
    {
        public ErraticMiningVehicleBaobab() : base() { }

        public ErraticMiningVehicleBaobab(string name, Location location) : base(name, location)
        {
        }

        public ErraticMiningVehicleBaobab(string name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            MaxAmmoCapacity = 60;
            AmmoLeft = MaxAmmoCapacity;
        }
    }
}
