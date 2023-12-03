using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Moving;
using MapControlTryOuts.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.ViewModels
{
    public class ResourcesViewModel: ViewModelBase
    {

        public ResourcesViewModel(IPointsContext _context): base(_context)
        {
            Resources = new ObservableCollection<ResourceObject>(_Context.Resources);
        }

        public ObservableCollection<ResourceObject> Resources { get; set; }
    }
}
