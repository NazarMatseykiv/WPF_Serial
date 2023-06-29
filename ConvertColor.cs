using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_Serial
{
    public class ConvertColorBush : IValueConverter // Конвертація колорів при False буде колір Black, а при True буде Gray
    {
        public Brush ColorFalse { get; set; } = Brushes.Black;
        public Brush ColorTrue { get; set; } = Brushes.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool c = (bool)value;
            return !c ? ColorFalse : ColorTrue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ReviewConvert : IMultiValueConverter // Збирає дані з Форми і створює обєкт рецензії
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string Descr = (string)values[0];
            DateTime DateCreate = DateTime.Now;
            DateTime? DateUpdate = (DateTime?)values[1];
            Model.PriorityModer priority = (Model.PriorityModer)(int)values[2];
            if (!string.IsNullOrWhiteSpace(Descr) && DateUpdate.HasValue)
                return new ModelView.ReviewMV(Descr, DateCreate, DateUpdate.Value, priority, false);
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
