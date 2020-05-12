using System;
using System.Linq;
using System.Windows.Markup;

namespace ShortcutManager.MarkupExtensions
{
    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type type;

        public EnumToItemsSource(Type type)
        {
            this.type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return System.Enum.GetValues(type)
                .Cast<object>()
                .Select(e => e);
        }
    }
}