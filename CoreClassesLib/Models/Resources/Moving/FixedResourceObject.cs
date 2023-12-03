using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    internal class FixedResourceObject : ResourceObject
    {
        public FixedResourceObject()
        {
        }

        public FixedResourceObject(string name, double latitude, double longitude) : base(name, latitude, longitude)
        {
        }
    }
}
