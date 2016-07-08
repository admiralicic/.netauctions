using BHao.Client.Mobile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BHao.Client.Mobile.Converters
{
    public class AukcijaOmoguciKupiOdmahConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            AukcijaDTO aukcija = (AukcijaDTO)value;

            var najvecaPonuda = (decimal)0;
            if (aukcija.NajvecaPonuda != null)
                najvecaPonuda = aukcija.NajvecaPonuda.Iznos;

            return aukcija.KupiOdmahCijena > najvecaPonuda && aukcija.Zavrsena == false ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
