using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources.Moving;
using CoreClassesLib.Models.Technical;
using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources
{
    public class ResourceObject : BaseObject
    {

        public ResourceObject(string name, double latitude, double longitude) : base(name, latitude, longitude)
        {
            TechnicalData = new();
            SelectedHostileObjects = new();
            AmmoContainer = new();
            OnBeforeFireCommandExecute += ResourceObject_OnBeforeFireCommandExecute;
        }

        public ResourceObject()
        {
            TechnicalData = new();
            SelectedHostileObjects = new();
            AmmoContainer = new();
            OnBeforeFireCommandExecute += ResourceObject_OnBeforeFireCommandExecute;
        }

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

        public ICollection<Command> Commands { get; set; }

        public ObservableCollection<HostileObjectModel> SelectedHostileObjects { get; set; }
        public ObservableCollection<BaseAmmoModel> AmmoContainer { get; set; }

        protected override void OnPropertyChanged(string property, object oldValue = null, object newValue = null)
        {
            base.OnPropertyChanged(property, oldValue, newValue);
            if (property == nameof(Location))
            {
                foreach (var ammo in AmmoContainer.Where(x => x.Fired == false))
                    ammo.Point.Location = Point.Location;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>When not overriden returns first BaseAmmoModel assigned to resource in a list</returns>
        public virtual List<BaseAmmoModel> ChooseAmmo(object[] parameters)
        {
            List<BaseAmmoModel> fired = new();
            if (AmmoContainer.Any())
            {
                fired.Add(AmmoContainer.FirstOrDefault());
                foreach (var a in fired)
                {
                    a.Point.Visible = true;
                    a.Fired = true;
                }
                return fired;
            }
            return null;
        }

        protected virtual ExplosionPointModel FireCommand(HostileObjectModel hostile, BaseAmmoModel fired) { return null; }
        protected virtual ExplosionPointModel FireCommand(BaseAmmoModel fired) { return null; }

        public ExplosionPointModel FireCommandExecute(HostileObjectModel hostile, BaseAmmoModel fired)
        {
            OnBeforeFireCommandExecute?.Invoke(hostile, fired);
            return FireCommand(hostile, fired);
        }

        public ExplosionPointModel FireCommandExecute(BaseAmmoModel fired)
        {
            OnBeforeFireCommandExecute?.Invoke(null, fired);
            return FireCommand(fired);
        }

        public event Action<HostileObjectModel, BaseAmmoModel> OnBeforeFireCommandExecute;

        protected virtual void ResourceObject_OnBeforeFireCommandExecute(HostileObjectModel hostile, BaseAmmoModel fired)
        {
            Command firedCommand = new Command()
            {
                ExecutionDate = DateTime.Now,
                Executor = this,
                CommandText = "Komenda: otworzyć ogień"
            };

            Commands.Add(firedCommand);
        }


    }
}
