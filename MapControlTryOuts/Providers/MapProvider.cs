using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Providers
{
    public class MapProvider : Map, INotifyPropertyChanged
    {
        private MapTileLayer _BaseLayer;

        public MapProvider()
        {
            ManipulationMode = System.Windows.Input.ManipulationModes.All;
            MinZoomLevel = 4;
            MaxZoomLevel = 15;
            ZoomLevel = 7;
            Center = new Location(52, 19);

            TileSource source = new TileSource();
            source.UriFormat = "https://tile.openstreetmap.org/{z}/{x}/{y}.png";

            _BaseLayer = new MapTileLayer();
            _BaseLayer.SourceName = "OpenStreetMap";
            _BaseLayer.Description = "© [OpenStreetMap contributors](http://www.openstreetmap.org/copyright)";
            _BaseLayer.TileSource = source;
            MapLayer = _BaseLayer;

        }


        public MapTileLayer BaseLayer
        {
            get => _BaseLayer;
            set
            {
                if (_BaseLayer != value)
                {
                    _BaseLayer = value;
                    RaisePropertyChanged(nameof(BaseLayer));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
