using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Moving;
using MapControlTryOuts.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MapControlTryOuts.ViewModels
{
    public class XamlTestViewModel: ViewModelBase
    {
        public XamlTestViewModel(IPointsContext _context): base(_context)
        {
                Resources = new ObservableCollection<ResourceObject>(_Context.Resources.Include(nameof(ResourceObject.Point)));
                HostileObjects = new ObservableCollection<HostileObjectModel>(_Context.Hostiles);
                AmmoContainers = new ObservableCollection<BaseAmmoModel>(_Context.Ammunition);
                _SelectedResource = Resources.FirstOrDefault(); 
            
        }

        private ResourceObject _SelectedResource;
        public ResourceObject SelectedResource
        {
            get
            {
                return _SelectedResource;
            }
            set
            {
                _SelectedResource = value;
                //if (value != null)
                //{
                //    SelectedResource.PropertyChanged += SelectedResource_PropertyChanged;
                //    FireCommand.RaiseCanExecuteChanged();
                //    DistanceBetweenSelected = SelectedResource != null && SelectedHostile != null ? SelectedResource.Distance(SelectedHostile) : 0;
                //}
            }
        }


        public ObservableCollection<ResourceObject> Resources { get; set; }
        public ObservableCollection<HostileObjectModel> HostileObjects { get; set; }
        public ObservableCollection<BaseAmmoModel> AmmoContainers { get; set; }

    }
}
