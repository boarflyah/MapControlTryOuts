using MapControlTryOuts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                _CurrentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;
        
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }


    }
}
