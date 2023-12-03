using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MapControlTryOuts.Extensions.Converters
{
    public class IntegerToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0: return PackIconKind.Truck;
                case 1: return PackIconKind.RocketLaunchOutline;
                case 101: return PackIconKind.Mine;
                case 201: return PackIconKind.Warehouse;
                default: return PackIconKind.VectorPoint;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PackIconKind)value == PackIconKind.RocketLaunchOutline) return 1;
            if ((PackIconKind)value == PackIconKind.Mine) return 101;
            else return 0;
        }
    }
}
