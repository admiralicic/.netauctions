using BHao.Client.Mobile.DTO;
using BHao.Client.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BHao.Client.Mobile.Converters
{
    class AukcijaNajvecaPonudaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            AukcijaDTO aukcija = (AukcijaDTO)value;

            return aukcija.NajvecaPonuda != null 
                ? String.Format("Trenutna cijena: {0} KM", aukcija.NajvecaPonuda.Iznos.ToString()) 
                : String.Format("Trenutna cijena: {0} KM", aukcija.PocetnaCijena.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
