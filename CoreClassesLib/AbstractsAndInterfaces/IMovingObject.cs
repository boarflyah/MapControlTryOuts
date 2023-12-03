using CoreClassesLib.Models.Points;
using MapControl;

namespace CoreClassesLib.AbstractsAndInterfaces
{
    public interface IMovingObject
    {
        public Location MovePointTowards(BasePointModel targetPoint, double mps, double ms, ref double distanceTravelled);

        public Location MovePointTowards(Location location, double distanceInMeters);
    }
}
