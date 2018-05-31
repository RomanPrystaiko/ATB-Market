using ATB_Market.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class RecipeCoverCollectionPage : Page
    {

        ObservableCollection<RecipeCover> collection = new ObservableCollection<RecipeCover>();
        ATBMarket market = new ATBMarket();

        public RecipeCoverCollectionPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += DiscountsCollectionPage_BackRequested;
            FillColection();
        }

        private void DiscountsCollectionPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }

            e.Handled = true;
        }

        private async void FillColection()
        {
            MyProgressRing.IsActive = true;
            var coll = await market.GetRecipeCoversCollectionAsync();

            foreach (var item in coll)
            {
                collection.Add(item);
            }

            MyProgressRing.IsActive = false;
        }

        private void MyASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var searchText = (sender as AutoSuggestBox).Text;
            MyListView.ItemsSource = this.collection.Where((item) => { return item.RecipeName.Contains(searchText); });
        }

        private void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var recipe = e.ClickedItem as RecipeCover;
            this.Frame.Navigate(typeof(RecipePage), recipe);

        }
    }
}
