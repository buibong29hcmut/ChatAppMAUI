using ChatApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Conveters
{
    public class GoToConversationDetailParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new GoToConversationDetailParam()
            {
                ConversationId = new Guid(values[0].ToString()),
                OtherUserId= new Guid(values[1].ToString())
            };
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
