using CoreClassesLib.AbstractsAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Points
{
    public class BaseObject: BaseNotifyPropertyChanged
    {
        public BaseObject(string name, double latitude, double longitude)
        {
            Name = name;
            Point = new BasePointModel(latitude, longitude);
        }

        public BaseObject()
        {
        }

        public int BaseObjectId { get; set; }

        private BasePointModel _Point;
        public BasePointModel Point
        {
            get => _Point;
            set
            {
                if (_Point != value)
                {
                    _Point = value;
                    RaisePropertyChanged(nameof(Point));
                }
            }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }

        private double _Health;
        public double Health
        {
            get { return _Health; }
            set
            {
                if (Health != value)
                {
                    _Health = value;
                    RaisePropertyChanged(nameof(Health));
                }
            }
        }

        public string TextDetails => ShowInformation();

        public virtual string ShowInformation() { return ""; }

    }
}
