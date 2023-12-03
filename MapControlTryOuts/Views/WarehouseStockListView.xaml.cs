using MapControlTryOuts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;


namespace MapControlTryOuts.Views;

public partial class WarehouseStockListView : UserControl
{
    public WarehouseStockListView()
    {
        InitializeComponent();

        Loaded += WarehouseStockListView_Loaded;
    }

    private void WarehouseStockListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is WarehouseStockListViewModel vm && warehousesComboBox.Items.Count == 0)
        {
            warehousesComboBox.Items.Add("Wszystko");
            foreach (var warehouse in vm.Warehouses)
                warehousesComboBox.Items.Add(warehouse.Name);
            warehousesComboBox.SelectedIndex = 0;
        }
    }

    private void warehousesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.AddedItems[0]?.ToString()) == false && DataContext is WarehouseStockListViewModel vm)
        {
            vm.FilterAssets(e.AddedItems[0].ToString());
        }
    }
}
