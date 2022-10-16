using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Conveters
{
    public class FromUserIdToHorizontalOptionConverter : IValueConverter
    {
        
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            string userId = SecureStorage.GetAsync("profile").Result;
            if (!(values.ToString() ==(userId)))
            {
                return LayoutOptions.Start;
            }

            return LayoutOptions.End;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
