using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class BaseAmmoModel : BasePointModel
    {
        public BaseAmmoModel(ResourcePointModel resource)
        {
            Resource = resource;
            Location = resource.Location;
            Speed = 500;
            Visible = false;
        }

        public BaseAmmoModel(string? name, Location? location) : base(name, location)
        {
            Visible = false;
        }

        public BaseAmmoModel(string? name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            Visible = false;
        }

        private double _Speed;

        public double Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }


        private ResourcePointModel? _Resource;

        public ResourcePointModel? Resource
        {
            get { return _Resource; }
            set { _Resource = value; }
        }
    }
}
