using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Points;
using MapControl;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class ErraticMiningVehicle : AreaCoverageResourceModel
    {
        public ErraticMiningVehicle() : base() { }

        public ErraticMiningVehicle(string name, Location location) : base(name, location)
        {
        }

        public ErraticMiningVehicle(string name, double latitude, double longitude, bool isHostileNeeded) : base(name, latitude, longitude, isHostileNeeded)
        {
            //MaxAmmoCapacity = 60;
            //MultiTargetHandling = false;
        }

        public override List<BaseAmmoModel> ChooseAmmo(object[] parameters)
        {
            var rectangle = parameters.FirstOrDefault(x => x is RectangleModel) as RectangleModel;
            if (rectangle != null)
            {
                DispersionArea = rectangle;
                rectangle = null;
            }
            List<BaseAmmoModel> list = new();
            double sideLength;
            int minesPerLength, minesPerHeight, ammoIndex = 0;
            GetRectangleSurfaceArea(out sideLength, out minesPerLength, out minesPerHeight);


            for (int i = 0; i < minesPerLength; i++)
                for (int j = 0; j < minesPerHeight; j++)
                {
                    if (ammoIndex < AmmoContainer.Count)
                    {
                        AmmoContainer[ammoIndex].Point.Location = DispersionArea.StartLocation
                            .GetLocation(180, sideLength / 2 + sideLength * j)
                            .GetLocation(90, sideLength / 2 + sideLength * i);
                        ammoIndex++;
                    }
                }

            foreach (BaseAmmoModel ammo in AmmoContainer)
            {
                //ammo.Fired = true;
                //ammo.Visible = true;
                list.Add(ammo);
            }
            return list;
        }

        protected override ExplosionPointModel FireCommand(BaseAmmoModel fired)
        {
            ExplosionPointModel result = null;

            if (fired != null)
            {
                Point.Location = MovePointTowards(new Location(DispersionArea.StartLocation.Latitude, DispersionArea.StartLocation.Longitude), 1000);
                //Point.Location = new(DispersionArea.StartLocation.Latitude, DispersionArea.StartLocation.Longitude);
                fired.Point.Visible = true;
                fired.Fired = true;
            }

            return result;
        }

        protected override void ResourceObject_OnBeforeFireCommandExecute(HostileObjectModel hostile, BaseAmmoModel fired)
        {
            Command firedCommand = new Command()
            {
                ExecutionDate = DateTime.Now,
                Executor = this,
                CommandText = "Komenda: minować"
            };

            Commands.Add(firedCommand);
        }
    }
}
