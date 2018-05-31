using ATB_Market.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ATB_Market
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiscountsCollectionPage : Page
    {
        private ObservableCollection<Discount> collection = new ObservableCollection<Discount>();

        public DiscountsCollectionPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += DiscountsCollectionPage_BackRequested;
        }

        private void DiscountsCollectionPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if(this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }

            e.Handled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            GetDiscounts();
        }

        private async void GetDiscounts()
        {
            MyProgressRing.IsActive = true;

            ATBMarket market = new ATBMarket();
            var Collection = await market.GetEconomyDiscounts();

            foreach (var item in Collection)
            {
                collection.Add(item);
            }

            MyProgressRing.IsActive = false;

        }
    }
}
