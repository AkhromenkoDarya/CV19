using System;
using System.Linq;
using System.Windows.Markup;

namespace CV19.Infrastructure.Common
{
    /// <summary>
    /// Класс-функция для разметки XAML.
    /// </summary>
    [MarkupExtensionReturnType(typeof(int[]))]
    internal class StringToIntArray : MarkupExtension
    {
        [ConstructorArgument(nameof(InputString))]
        public string InputString { get; set; }

        public char Separator => ';';

        public StringToIntArray()
        {
        }

        public StringToIntArray(string inputString) => InputString = inputString;

        public override object ProvideValue(IServiceProvider serviceProvider)
            => InputString.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)
            .DefaultIfEmpty()
            .Select(int.Parse)
            .ToArray();
    }
}
