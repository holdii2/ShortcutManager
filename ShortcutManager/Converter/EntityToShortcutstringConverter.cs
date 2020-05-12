using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using ShortcutManager.Database;

namespace ShortcutManager.Converter
{
    public class EntityToShortcutStringConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ShortcutItemEntity item))
                return null;

            return $"{item.ModifierKeys} - {item.Key}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}