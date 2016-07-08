using BHao.Client.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BHao.Client.Mobile.Converters
{
    public class AukcijaToCijenaKonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Aukcija aukcija = (Aukcija)value;
            var minimalnaCijena = String.Format("{0} KM", aukcija.MinimalnaCijena.ToString());
            var najvecaPonuda = String.Format("{0} KM", aukcija.NajvecaPonuda.ToString());

            return aukcija.NajvecaPonuda == null ? minimalnaCijena : najvecaPonuda;
        }
        
        

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
