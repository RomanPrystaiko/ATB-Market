using ATB_Market.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ATB_Market
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiscountsPage : Page
    {
        ATBMarket _atb = new ATBMarket();

        public DiscountsPage()
        {
            this.InitializeComponent();
            setImages();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public async void setImages()
        {
            MyRing.IsActive = true;
            await _atb.GetDiscountsImages();

            ImageDiscount7days.Source = new BitmapImage(new Uri(_atb.Image7daysDiscount, UriKind.Absolute));
            ImageDiscountEconomy.Source = new BitmapImage(new Uri(_atb.ImageEconomuDiscount, UriKind.Absolute));

            MyRing.IsActive = false;
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            (sender as Grid).Opacity = 1.0;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            (sender as Grid).Opacity = 0.5;
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(DiscountsCollectionPage));
        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(_7DaysDiscountCollectionPage));
        }
    }
}
