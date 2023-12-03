using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapControlTryOuts.Models
{
    public class ResourcePointModel : BasePointModel
    {
        public ResourcePointModel() :base() 
        {
            SelectedHostileObjects = new();
            AmmoContainer = new();
        }

        public ResourcePointModel(string name, Location location) : base(name, location)
        {
            SelectedHostileObjects = new();
            AmmoContainer = new();
        }

        public ResourcePointModel(string name, double latitude, double longitude) : base(name, latitude, longitude) 
        {
            SelectedHostileObjects = new();
            AmmoContainer = new();
        }

        private int _MaxAmmoCapacity;
        public int MaxAmmoCapacity
        {
            get { return _MaxAmmoCapacity; }
            set
            {
                if (_MaxAmmoCapacity != value)
                {
                    _MaxAmmoCapacity = value;
                    RaisePropertyChanged(nameof(MaxAmmoCapacity));
                }
            }
        }

        private int _AmmoLeft;
        public int AmmoLeft
        {
            get { return _AmmoLeft; }
            set
            {
                if (_AmmoLeft != value)
                {
                    _AmmoLeft = value;
                    RaisePropertyChanged(nameof(AmmoLeft));
                }
            }
        }

        public ObservableCollection<HostileObjectModel> SelectedHostileObjects { get; set; }
        public ObservableCollection<BaseAmmoModel> AmmoContainer { get; set; }

        public virtual void FireCommand(int objIndex)
        {
            do
            {
                Thread.Sleep(500);
                AmmoContainer[0].Location = AmmoContainer[0].MovePointTowards(SelectedHostileObjects[0], AmmoContainer[0].Speed, 500);

            } while (Distance(SelectedHostileObjects[objIndex]) > 0);
            SelectedHostileObjects[0].Health -= new Random().Next(SelectedHostileObjects[objIndex].AntiAircraftResistency, 100) - SelectedHostileObjects[objIndex].AntiAircraftResistency;
            AmmoContainer.RemoveAt(0);
        }

    }
}
