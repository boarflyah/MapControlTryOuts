using CoreClassesLib.Contexts;
using MapControlTryOuts.Commands;
using MapControlTryOuts.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapControlTryOuts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _NavigationStore;

        public ViewModelBase CurrentViewModel => _NavigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore, IPointsContext context) : base(context)
        {
            _NavigationStore = navigationStore;
            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            
            NavigateToMapCommand = new(OnExecuteNavigateToMapCommand, CanExecuteNavigateToMapCommand);
            NavigateToWarehouseCommand = new(OnExecuteNavigateToWarehouseCommand, CanExecuteNavigateToWarehouseCommand);
            NavigateToAddResourceCommand = new(OnExecuteNavigateToAddResourceCommand, CanExecuteNavigateToAddResourceCommand);
            CloseCommand = new(OnExecuteCloseCommand, CanExecuteCloseCommand);

        }

        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
            NavigateToMapCommand.RaiseCanExecuteChanged();
            NavigateToWarehouseCommand.RaiseCanExecuteChanged();
        }

        #region Properties

        private bool _IsNavigationPopupOpen;

        public bool IsNavigationPopupOpen
        {
            get => _IsNavigationPopupOpen;
            set
            {
                if (_IsNavigationPopupOpen != value)
                {
                    _IsNavigationPopupOpen = value;
                    RaisePropertyChanged(nameof(IsNavigationPopupOpen));
                }
            }
        }


        #endregion

        public CommandBase NavigateToMapCommand { get; set; }

        public CommandBase NavigateToWarehouseCommand { get; set; }

        public CommandBase NavigateToAddResourceCommand { get; set; }

        public CommandBase CloseCommand { get; set; }

        private bool CanExecuteNavigateToMapCommand() => CurrentViewModel is MapViewModel ? false : true;

        private void OnExecuteNavigateToMapCommand() => _NavigationStore.CurrentViewModel = new MapViewModel(_Context);

        private bool CanExecuteNavigateToWarehouseCommand() => CurrentViewModel is WarehouseStockListViewModel ? false : true;

        private void OnExecuteNavigateToWarehouseCommand() => _NavigationStore.CurrentViewModel = new WarehouseStockListViewModel(_Context);

        private bool CanExecuteNavigateToAddResourceCommand() => true;

        private void OnExecuteNavigateToAddResourceCommand() => _NavigationStore.CurrentViewModel = new AddResourceViewModel(_Context);

        private bool CanExecuteCloseCommand() => true;

        private void OnExecuteCloseCommand()
        {
            _Context.CommitChanges();
            App.Current.Dispatcher.InvokeShutdown();
            App.Current.MainWindow.Close();
        }
    }
}
