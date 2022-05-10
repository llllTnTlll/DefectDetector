using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DefectDetector.Helper
{
    // 值转换器
    // 在底部条颜色发生变化时返回True
    public class BottomColor2BoolConverter : IValueConverter
    {
        private static SolidColorBrush previousBrush = App.Current.Resources["PrimaryHueMidBrush"] as SolidColorBrush;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush newBrush = value as SolidColorBrush; 
            if (newBrush.Equals(previousBrush))
            {
                previousBrush = newBrush;
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
