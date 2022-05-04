using System;
using System.Windows;
using System.Windows.Media;

namespace CV19.Infrastructure.Extensions
{
    public static class DependencyObjectExtension
    {
        public static DependencyObject FindRoot(this DependencyObject obj, Type helperType)
        {
            if (helperType is null || (helperType != typeof(VisualTreeHelper) && 
                                       helperType != typeof(LogicalTreeHelper)))
            {
                throw new ArgumentException($@"The {nameof(helperType)} parameter must be 
                    of type VisualTreeHelper or LogicalTreeHelper.");
            }

            do
            {
                DependencyObject parent = helperType == typeof(VisualTreeHelper) 
                    ? VisualTreeHelper.GetParent(obj) 
                    : LogicalTreeHelper.GetParent(obj);

                if (parent is null)
                {
                    return obj;
                }

                obj = parent;
            }
            while (true);
        }
    }
}
