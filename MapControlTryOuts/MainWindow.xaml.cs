using System.Threading.Tasks;
using System.Windows;
using CoreClassesLib.Contexts;
using MapControl;
using MapControl.Caching;
using MapControlTryOuts.Stores;
using MapControlTryOuts.ViewModels;

namespace MapControlTryOuts;
public partial class MainWindow : Window
{
    private readonly IPointsContext _dataContext;
    private readonly NavigationStore _navigationStore;

    public MainWindow(IPointsContext dataContext, NavigationStore navigationStore, MainWindowViewModel viewModel)
    {
        InitializeComponent();
        _dataContext = dataContext;
        _navigationStore = navigationStore;
        DataContext = viewModel;
        
        _navigationStore.CurrentViewModel = new MapViewModel(_dataContext);

        ImageLoader.HttpClient.DefaultRequestHeaders.Add("User-Agent", "MapControlBordersApp");

        TileImageLoader.Cache = new ImageFileCache(TileImageLoader.DefaultCacheFolder);

        if (TileImageLoader.Cache is ImageFileCache cache)
        {
            Loaded += async (s, e) =>
            {
                await Task.Delay(2000);
                await cache.Clean();
            };
        }

        //XamlTestViewModel testViewModelObject = new(_dataContext);
        //XamlTestViewControl.DataContext = testViewModelObject;

        //WarehouseStockListViewModel warehouseStockListViewModel = new(_dataContext);
        //WarehouseStockListViewControl.DataContext = warehouseStockListViewModel;

        //WarehouseStockListViewTab.GotFocus += WarehouseStockListViewTab_GotFocus;
        //WarehouseStockListViewTab.LostFocus += WarehouseStockListViewTab_LostFocus;




        //mapBase = new();
        //BasePointModel point1 = new("Punkt 1", 52, 19);
        //BasePointModel point2 = new("Punkt 2", 51.333, 17.88);

        //MapItem MapItem1 = new();
        //MapItem1.Location = new Location(50.333, 19.777);
        //MapItem1.Height = 60;
        //MapItem1.Width = 60;
        //MapItem1.ToolTip = "Punkt";
        //mapItemsControl.Items.Add(point1);
        //mapItemsControl.Items.Add(point2);

    }

    private void WarehouseStockListViewTab_LostFocus(object sender, RoutedEventArgs e)
    {
        //(WarehouseStockListViewControl.DataContext as WarehouseStockListViewModel)?.AmmoStock.Clear();
        //WarehouseStockListViewControl.DataContext = null;
    }

    private void WarehouseStockListViewTab_GotFocus(object sender, RoutedEventArgs e)
    {
        //WarehouseStockListViewModel warehouseStockListViewModel = new();
        //WarehouseStockListViewControl.DataContext = warehouseStockListViewModel;
    }

    private void MapViewControl_Loaded(object sender, RoutedEventArgs e)
    {
        //MapViewControl.DataContext = mapViewModelObject;
    }

    private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.SystemKey == System.Windows.Input.Key.F10)
            this.Close();
    }

    private void ExpandButton_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel).IsNavigationPopupOpen = true;
    }
}
