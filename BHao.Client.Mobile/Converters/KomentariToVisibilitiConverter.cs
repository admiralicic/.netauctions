﻿using BHao.Client.Mobile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BHao.Client.Mobile.Converters
{
    class KomentariToVisibilitiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var komentari = (List<KomentarDTO>)value;

            return komentari.Count() > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
