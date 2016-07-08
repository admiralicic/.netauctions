using BHao.Client.Mobile.Common;
using BHao.Client.Mobile.DataModel;
using BHao.Client.Mobile.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BHao.Client.Mobile.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class AukcijaDetailPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private int aukcijaId;

        public AukcijaDetailPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            DetaljiPanel.Visibility = Visibility.Collapsed;
            aukcijaId = (int)e.NavigationParameter;
            await UcitajPodatke(aukcijaId);

            Ponudite.Visibility = ViewModelHelper.prijavljeniKorisnikId == ((AukcijaDTO)defaultViewModel["Aukcija"]).ProdavacId 
                || ((AukcijaDTO)defaultViewModel["Aukcija"]).Zavrsena == true
                ? Visibility.Collapsed 
                : Visibility.Visible;

            DetaljiPanel.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AukcijePage));
        }

        private async void Osvjezi_Click(object sender, RoutedEventArgs e)
        {
            await UcitajPodatke(aukcijaId);
        }

        private async Task UcitajPodatke(int aukcijaId)
        {
           
            myProgressRing.IsActive = true;
            var dataSource = new BHaoDataSource();
            try
            {
                this.defaultViewModel["Aukcija"] = await dataSource.GetAukcija(aukcijaId);
                myProgressRing.IsActive = false;
                myProgressRing.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                new MessageDialog("Provjerite konekciju na internet.").ShowAsync();
            }
           
        }

        private async void PonudaButton_Click(object sender, RoutedEventArgs e)
        {
            var dataSource = new BHaoDataSource();
            decimal iznosPonude;
            Ponudite.Flyout.Hide();
            if (decimal.TryParse(IznosPonude.Text, out iznosPonude))
            {
                try
                {
                    
                    if(await dataSource.KreirajPonudu(iznosPonude, aukcijaId) == true )
                    {
                        await UcitajPodatke(aukcijaId);
                        await new MessageDialog("Vaša ponuda je uspješno evidentirana.\n\nHvala!").ShowAsync();
                    }
                }
                catch (Exception ex)
                {

                    new MessageDialog(ex.Message).ShowAsync();
                }
                
            }
            else
            {
                await new MessageDialog("Iznos ponude mora biti broj.").ShowAsync();
            }

        }

        private async void KupiOdmahButton_Click(object sender, RoutedEventArgs e)
        {
            var dataSource = new BHaoDataSource();
            Ponudite.Flyout.Hide();
            try
            {
                if (await dataSource.KupiOdmah(aukcijaId)== true)
                {
                    await new MessageDialog("Uspješno ste kupili artikal. \n\nHvala!").ShowAsync() ;
                }
            }
            catch (Exception ex)
            {

                new MessageDialog(ex.Message).ShowAsync();
            }
        }
    }
}
