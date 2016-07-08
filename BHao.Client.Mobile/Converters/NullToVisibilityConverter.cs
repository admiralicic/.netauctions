using BHao.Client.Mobile.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;


namespace BHao.Client.Mobile.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value == null)
            {
                return Visibility.Collapsed; 
            }

            //if (System.Convert.ToInt32(value) == 0)
            //{
            //    return Visibility.Collapsed;
            //}

            return Visibility.Visible;
           
        }
		 

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
