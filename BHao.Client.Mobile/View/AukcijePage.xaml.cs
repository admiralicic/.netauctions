using BHao.Client.Mobile.Common;
using BHao.Client.Mobile.DataModel;
using BHao.Client.Mobile.DTO;
using BHao.Client.Mobile.Exceptions;
using BHao.Client.Mobile.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Security.Credentials;
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
    public sealed partial class AukcijePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();



        public AukcijePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            //this.Pretraga.Visibility = Visibility.Collapsed;


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

            if (Frame.ForwardStack.Count < 1)
            {
                await UcitajPodatke();
            }


            if (Frame.BackStack.Count > 0)
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
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

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (AukcijaDetailDTO)e.ClickedItem;

            //try
            //{
            Frame.Navigate(typeof(AukcijaDetailPage), item.Aukcija.Id);
            //}
            //catch (Exception)
            //{

            //    new MessageDialog("Provjerite konekciju na internet.").ShowAsync();
            //}

        }


        private async Task UcitajPodatke(string pretraga = "")
        {

            if (ViewModelHelper.InternetKonekcija() == false)
            {
                MessageDialog message = new MessageDialog("Provjerite konekciju na internet.");
                await message.ShowAsync();
                return;
            }

            myProgressRing.IsActive = true;
            var dataSource = new BHaoDataSource();

            try
            {
                var aktivneAukcije = await dataSource.GetAktivneAukcije(pretraga);
                var mojeProdaje = await dataSource.GetAukcijeKorisnika("prodavac");
                var mojePonude = await dataSource.GetAukcijeKorisnika("kupac");
                myProgressRing.IsActive = false;

                this.defaultViewModel["AktivneAukcije"] = aktivneAukcije;
                this.defaultViewModel["MojeProdaje"] = mojeProdaje;
                this.defaultViewModel["MojePonude"] = mojePonude;
            }
            catch (AccessDeniedException)
            {

                Frame.Navigate(typeof(LoginPage));
            }
            catch (Exception)
            {
                new MessageDialog("Provjerite konekciju na internet.").ShowAsync();
            }

        }

        private async void Osvjezi_Click(object sender, RoutedEventArgs e)
        {
            await UcitajPodatke();
        }

        private async void Pretrazi_Click(object sender, RoutedEventArgs e)
        {
            await UcitajPodatke(Pretraga.Text);
            Trazi.Flyout.Hide();
        }

        private async void Pretraga_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Accept)
            {
                await UcitajPodatke(Pretraga.Text);
                Trazi.Flyout.Hide();
            }
        }

        private void OdjavaAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModelHelper.accessTokenString = "";
            ViewModelHelper.prijavljeniKorisnikId = 0;

            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            try
            {
                var credentialList = vault.FindAllByResource("bauk");
                if (credentialList.Count > 0)
                {
                    credential = credentialList[0];
                    vault.Remove(credential);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            Frame.Navigate(typeof(LoginPage));
        }




    }
}
