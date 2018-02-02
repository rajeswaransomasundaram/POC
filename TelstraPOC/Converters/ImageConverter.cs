using System;
using System.Globalization;
using Xamarin.Forms;
namespace TelstraPOC.Converters
{
    public class ImageConverter : IValueConverter
    {
         
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return "placeholderProfileImage.png";
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
