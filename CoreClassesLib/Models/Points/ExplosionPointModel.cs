using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Points
{
    [NotMapped]
    public class ExplosionPointModel : BaseObject
    {
        public ExplosionPointModel() 
        {
            Point = new();
            ExtinctionDateTime = DateTime.Now.AddSeconds(5);
        }
        public ExplosionPointModel(string? name, Location? location)// : base(name, location)
        {
        }

        public ExplosionPointModel(string? name, Location? location, DateTime extinctionDateTime)// : base(name, location)
        {
            ExtinctionDateTime = extinctionDateTime;
        }

        private double _HealthTaken;

        public double HealthTaken
        {
            get => _HealthTaken;
            set
            {
                if (_HealthTaken != value)
                {
                    _HealthTaken = value;
                    RaisePropertyChanged(nameof(HealthTaken));
                }
            }
        }

        private bool _IsTargetDestroyed;

        public bool IsTargetDestroyed
        {
            get => _IsTargetDestroyed;
            set
            {
                if (_IsTargetDestroyed != value)
                {
                    _IsTargetDestroyed = value;
                    RaisePropertyChanged(nameof(IsTargetDestroyed));
                }
            }
        }

        private DateTime _ExtinctionDateTime;

        public DateTime ExtinctionDateTime
        {
            get { return _ExtinctionDateTime; }
            set
            {
                if (_ExtinctionDateTime != value)
                {
                    _ExtinctionDateTime = value;
                    RaisePropertyChanged(nameof(ExtinctionDateTime));
                }
            }
        }

    }
}
