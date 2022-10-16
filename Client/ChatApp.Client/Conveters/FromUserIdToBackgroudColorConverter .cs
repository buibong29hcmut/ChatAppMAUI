using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Conveters
{

    public class FromUserIdToBackgroudColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string userId = SecureStorage.GetAsync("profile").Result;

            if (!(value.ToString()==userId))
                return Color.FromArgb("#3b6af9");


            return Color.FromArgb("#d1d8e0");
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
