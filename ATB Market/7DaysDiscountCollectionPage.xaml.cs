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
using Windows.UI;
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
    public sealed partial class _7DaysDiscountCollectionPage : Page
    {

        ObservableCollection<Discount> collection1 = new ObservableCollection<Discount>();
        ObservableCollection<Discount> collection2 = new ObservableCollection<Discount>();
        ObservableCollection<Discount> collection3 = new ObservableCollection<Discount>();
        ObservableCollection<Discount> collection4 = new ObservableCollection<Discount>();
      //  ObservableCollection<Discount> collection7 = new ObservableCollection<Discount>();

        ATBMarket market = new ATBMarket();

        public _7DaysDiscountCollectionPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += DiscountsCollectionPage_BackRequested;


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            FillPivotItemsByData();
       
        }

        private void DiscountsCollectionPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }

            e.Handled = true;
        }


        private async void FillPivotItemsByData()
        {
            MyProgressRing1.IsActive = true;
            AddElementsToCollection(collection1, await market.Get7daysDiscountsCollection(1));
            AddElementsToCollection(collection2, await market.Get7daysDiscountsCollection(2));
            AddElementsToCollection(collection3, await market.Get7daysDiscountsCollection(3));
            AddElementsToCollection(collection4, await market.Get7daysDiscountsCollection(4));
          //  AddElementsToCollection(collection7, await market.Get7daysDiscountsCollection(7));
            MyProgressRing1.IsActive = false;
        }


        private void AddElementsToCollection(ObservableCollection<Discount> collectionToAdd, ObservableCollection<Discount> collectionTakeFrom)
        {
            foreach (var item in collectionTakeFrom)
            {
                collectionToAdd.Add(item);
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangePivotItemHeaderColor();
        }


        private void ChangePivotItemHeaderColor()
        {
            foreach (PivotItem pivotItem in Pivot.Items)
            {
                if (pivotItem == Pivot.Items[Pivot.SelectedIndex])
                {
                    // Header of the selected item to white
                    ((TextBlock)pivotItem.Header).Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                }
                else
                {
                    // Headers of other items to slightly darker
                    ((TextBlock)pivotItem.Header).Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 62, 120, 175));
                }
            }
        }
    }
}
