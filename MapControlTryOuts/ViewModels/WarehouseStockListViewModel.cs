using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Assets;
using CoreClassesLib.Models.Resources.Fixed;
using MapControlTryOuts.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MapControlTryOuts.ViewModels
{
    public class WarehouseStockListViewModel : ViewModelBase
    {

        public WarehouseStockListViewModel(IPointsContext _context): base(_context)
        {
            Assets = new(_Context.Assets.Include(nameof(BaseFixedAsset.TechnicalData)).Where(x => x.Quantity > 0));
            Warehouses = new(_Context.Resources.Include(nameof(ResourceObject.Point)).Where(x => x is WarehouseObject));
            Commands = new(_Context.Commands.Include(nameof(Command.Executor)));
            FilteredAssets = CollectionViewSource.GetDefaultView(Assets);
            FilteredAssets.Refresh();
        }

        public ObservableCollection<BaseFixedAsset> Assets { get; set; }

        public ObservableCollection<ResourceObject> Warehouses { get; set; }

        public virtual ObservableCollection<Command> Commands { get; set; }

        public ICollectionView FilteredAssets { get; private set; }

        public CommandBase ShowOnMapCommand { get; set; }

        private bool CanExecuteShowOnMapCommand()
        {
            return true;
        }

        private void OnExecuteShowOnMapCommand()
        {

        }

        public void FilterAssets(string warehouseName)
        {
            if (warehouseName.Equals("Wszystko"))
            {
                FilteredAssets.Filter = null;
                FilteredAssets.Refresh();

            }
            else
            {
                FilteredAssets.Filter = x => (x as BaseFixedAsset).Warehouse.Name == warehouseName;
                FilteredAssets.Refresh();
            }
        }
    }
}
