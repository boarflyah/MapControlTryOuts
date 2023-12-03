using CoreClassesLib.Models.Points;
using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class AreaCoverageResourceModel : MovingResourceObject
    {
        public AreaCoverageResourceModel()
        {
        }

        public AreaCoverageResourceModel(string name, Location location) : base(name, location)
        {
        }

        public AreaCoverageResourceModel(string name, double latitude, double longitude, bool isHostileNeeded) : base(name, latitude, longitude, isHostileNeeded)
        {
        }

        private RectangleModel _DispersionArea;
        [NotMapped]
        public RectangleModel DispersionArea
        {
            get => _DispersionArea;
            set
            {
                if (_DispersionArea != value)
                {
                    _DispersionArea = value;
                    RaisePropertyChanged(nameof(DispersionArea));
                }
            }
        }

        protected double GetRectangleSurfaceArea(out double squareSideLength, out int minesPerLength, out int minesPerHeight)
        {
            Location A = DispersionArea.StartLocation,
                D = DispersionArea.EndLocation,
                B = new Location(A.Latitude, D.Longitude),
                C = new Location(D.Latitude, A.Longitude);

            double length = A.GetDistance(B),
                height = A.GetDistance(C);

            double area = length * height,
                surfacePerAmmo = area / MovingResourceTechnicalData.MaxAmmoCapacity;

            squareSideLength = Math.Sqrt(surfacePerAmmo);
            minesPerLength = (int)Math.Round(length / squareSideLength, MidpointRounding.AwayFromZero);
            minesPerHeight = (int)Math.Round(height / squareSideLength, MidpointRounding.AwayFromZero);


            return length * height;
        }
    }
}
