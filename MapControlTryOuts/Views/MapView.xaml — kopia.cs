﻿using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Moving;
using MapControlTryOuts.Providers;
using MapControlTryOuts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapControlTryOuts.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        Cursor defaultCursor;

        public MapView()
        {
            InitializeComponent();
            defaultCursor = this.Cursor;

            mapLayer.MouseRightButtonUp += MapLayer_MouseRightButtonUp;
            mapLayer.MouseMove += MapLayer_MouseMove;
            mapLayer.MouseRightButtonDown += MapLayer_MouseRightButtonDown;
            //mapLayer.MouseLeftButtonDown += MapLayer_MouseLeftButtonDown;
        }

        //private void MapLayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2)
        //    {
        //        e.Handled = true;
        //        MessageBox.Show($"{e.GetPosition(mapLayer)}");
        //    }
        //}

        private void MapLayer_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && mapViewModel.SelectedResource is AreaCoverageResourceModel)
            {
                mapViewModel.SetIsMouseClickHold(true);
                mapViewModel.SetStartingLocation(e.GetPosition(mapLayer));
                mapViewModel.SetHeightAndWidth(e.GetPosition(mapLayer));
                mapViewModel.Rectangle.SetStartLocation(mapLayer.ViewToLocation(e.GetPosition(mapLayer)));
                CurrentCanvas.Children.Clear();
                CurrentCanvas.Children.Add(selectionRectangle);
            }
        }

        private void MapLayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && mapViewModel.SelectedResource is AreaCoverageResourceModel)
            {
                if (mapViewModel.Rectangle.IsMouseClickHold && e.RightButton == MouseButtonState.Pressed)
                    mapViewModel.SetHeightAndWidth(e.GetPosition(mapLayer));
            }
        }

        private void MapLayer_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && mapViewModel.SelectedResource is AreaCoverageResourceModel)
            {
                mapViewModel.SetIsMouseClickHold(false);
                mapViewModel.SetHeightAndWidth(e.GetPosition(mapLayer));
                mapViewModel.Rectangle.SetEndLocation(mapLayer.ViewToLocation(e.GetPosition(mapLayer)));
                //CurrentCanvas.Children.Clear();
            }
        }

        private void DataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dg)
            {
                if (dg.SelectedItem is BaseObject pm && pm.Point.Visible)
                {
                    mapLayer.Center = pm.Point.Location;
                    mapLayer.ZoomLevel = 10;
                }
            }
        }

        private void SelectedResourceRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is RadioButton radioButton && radioButton.DataContext is ResourceObject resource)
            {
                resource.Point.IsSelected = true;
                mapViewModel.SelectedResource = resource;
                mapViewModel.SelectedHostile = null;
            }
            //if (DataContext is MapViewModel mapViewModel && e.AddedItems[0] is ResourcePointModel resource && mapViewModel?.SelectedResource?.Id != resource.Id)
            //    mapViewModel.SelectedResource = e.AddedItems[0] as ResourcePointModel;
            //mapViewModel.SelectedResource = resource;
        }

        private void SelectedHostileRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is RadioButton radioButton && radioButton.DataContext is HostileObjectModel hostile)
            {
                hostile.Point.IsSelected = true;
                mapViewModel.SelectedHostile = hostile;
            }
        }

        private void SelectedResourceRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is RadioButton radioButton && radioButton.DataContext is ResourceObject resource)
            {
                resource.Point.IsSelected = false;
            }
        }

        private void SelectedHostileRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is RadioButton radioButton && radioButton.DataContext is HostileObjectModel hostile)
                hostile.Point.IsSelected = false;
        }

        private void SelectedHostileCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is CheckBox checkBox && checkBox.DataContext is HostileObjectModel hostile)
                hostile.Point.IsSelected = true;
        }

        private void SelectedHostileCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is MapViewModel mapViewModel && sender is CheckBox checkBox && checkBox.DataContext is HostileObjectModel hostile)
                hostile.Point.IsSelected = false;
        }

        private void HostileObjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MapViewModel viewModel && ResourcesDataGrid.SelectedItem != null && HostileObjectsDataGrid.SelectedItem != null
                && ResourcesDataGrid.SelectedItem is BaseObject resource && HostileObjectsDataGrid.SelectedItem is BaseObject hostile)
                viewModel.DistanceBetweenSelected = resource.Point.Distance(hostile.Point);
        }

        private void ResourcesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MapViewModel viewModel && ResourcesDataGrid.SelectedItem != null && HostileObjectsDataGrid.SelectedItem != null
                && ResourcesDataGrid.SelectedItem is BaseObject resource && HostileObjectsDataGrid.SelectedItem is BaseObject hostile)
                viewModel.DistanceBetweenSelected = resource.Point.Distance(hostile.Point);
            CurrentCanvas.Children.Clear();
        }

        private void FireButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentCanvas.Children.Clear();
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = defaultCursor;
        }
    }
}
