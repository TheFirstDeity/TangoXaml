using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TangoXaml
{
    [ValueConversion(typeof(Lazy<DrawingGroup>), typeof(DrawingGroup))]
    [ValueConversion(typeof(Lazy<DrawingGroup>), typeof(ImageSource))]
    public class LazyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsAssignableFrom(typeof(Lazy<DrawingGroup>)))
            {
                var lazy = (Lazy<DrawingGroup>)value;

                if (targetType.IsAssignableFrom(typeof(DrawingGroup)))
                {
                    return lazy.Value;
                }

                if (targetType.Equals(typeof(ImageSource)))
                {
                    var img = new DrawingImage();
                    img.Drawing = lazy.Value;
                    return img;
                }
            }
            
            throw new ArgumentException(string.Format("Cannot convert type {0} to type {1}", value.GetType().FullName, targetType.FullName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Equals(typeof(Lazy<DrawingGroup>)))
            {
                if (value.GetType().Equals(typeof(DrawingGroup)))
                {
                    var draw = (DrawingGroup)value;
                    return new Lazy<DrawingGroup>(() => draw);
                }

                if (value.GetType().Equals(typeof(DrawingImage)))
                {
                    var img = (DrawingImage)value; // Do the casting here so it throws an exception right away if it's the wrong type
                    var draw = (DrawingGroup)img.Drawing;
                    return new Lazy<DrawingGroup>(() => draw);
                }
            }

            throw new ArgumentException(string.Format("Cannot convert type {0} to type {1}", value.GetType().FullName, targetType.FullName));
        }
    }
}
