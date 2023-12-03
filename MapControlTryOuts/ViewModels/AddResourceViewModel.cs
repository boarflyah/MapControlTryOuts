using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Resources;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.ViewModels
{
    public class AddResourceViewModel : ViewModelBase
    {
        public AddResourceViewModel(IPointsContext context) : base(context) { }

        private ResourceObject _AddedResource;
        public ResourceObject AddedResource
        {
            get => _AddedResource;
            set
            {
                if (_AddedResource != value)
                {
                    _AddedResource = value;
                    RaisePropertyChanged(nameof(AddedResource));
                }
            }
        }

        private PackIconKind _SelectedIcon;

        public PackIconKind SelectedIcon
        {
            get => _SelectedIcon; 
            set 
            {
                if (value != _SelectedIcon)
                {
                    _SelectedIcon = value;
                    RaisePropertyChanged(nameof(SelectedIcon));
                }
            }
        }


        public IEnumerable<PackIconKind> Icons => Enum.GetValues(typeof(PackIconKind)).Cast<PackIconKind>()
            .Where(x => x.ToString().Contains("Truck") || x.ToString().Contains("Car"));





    }
}
