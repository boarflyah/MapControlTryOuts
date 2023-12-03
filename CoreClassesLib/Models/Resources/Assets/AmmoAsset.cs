using CoreClassesLib.Models.Technical;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Assets
{
    public class AmmoAsset : BaseFixedAsset
    {
        public AmmoAsset() : base() { }

        [NotMapped]
        public AmmoTechnicalData AmmoTechnicalData => TechnicalData as AmmoTechnicalData;
    }
}
