using MapControl;
using CoreClassesLib.AbstractsAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Points
{
    public class RectangleModel : BaseNotifyPropertyChanged
    {
        private int _Height;
        public int Height
        {
            get => _Height;
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    RaisePropertyChanged(nameof(Height));
                }
            }
        }

        private int _Width;
        public int Width
        {
            get => _Width;
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    RaisePropertyChanged(nameof(Width));
                }
            }
        }

        private bool _IsMouseClickHold;
        public bool IsMouseClickHold
        {
            get => _IsMouseClickHold;
            set
            {
                if (_IsMouseClickHold != value)
                {
                    _IsMouseClickHold = value;
                    RaisePropertyChanged(nameof(IsMouseClickHold));
                }
            }
        }

        private double _X;
        public double X
        {
            get => _X;
            set
            {
                if (_X != value)
                {
                    _X = value;
                    RaisePropertyChanged(nameof(X));
                }
            }
        }

        private double _Y;
        public double Y
        {
            get => _Y;
            set
            {
                if (_Y != value)
                {
                    _Y = value;
                    RaisePropertyChanged(nameof(Y));
                }
            }
        }

        private Location _StartLocation;
        public Location StartLocation
        {
            get => _StartLocation;
            set
            {
                if (_StartLocation != value)
                {
                    _StartLocation = value;
                    RaisePropertyChanged(nameof(StartLocation));
                }
            }
        }

        private Location _EndLocation;
        public Location EndLocation
        {
            get => _EndLocation;
            set
            {
                if (_EndLocation != value)
                {
                    _EndLocation = value;
                    RaisePropertyChanged(nameof(EndLocation));
                }
            }
        }

        private bool _Visible;

        public bool Visible
        {
            get => _Visible; 
            set
            {
                if (_Visible != value)
                {
                    _Visible = value;
                    RaisePropertyChanged(nameof(Visible));
                }
            }
        }


        public void SetStartLocation(Location location) => StartLocation = location;

        public void SetEndLocation(Location location) => EndLocation = location;

    }
}
