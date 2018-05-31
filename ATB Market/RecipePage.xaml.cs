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
    public sealed partial class RecipePage : Page
    {
        ObservableCollection<string> ingridients = new ObservableCollection<string>();
        ObservableCollection<Step> steps = new ObservableCollection<Step>();
        ATBMarket market = new ATBMarket();
        Recipe _recipe;
        RecipeCover _recipeCover;
     

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _recipeCover = e.Parameter as RecipeCover;
            SetRecipe();

        }

        public RecipePage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += DiscountsCollectionPage_BackRequested;


        }

        private void DiscountsCollectionPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecipeCoverCollectionPage));
            e.Handled = true;
        }


        private async void SetRecipe()
        {
            ProgressRing.IsActive = true;
            _recipe = await market.GetRecipeAsync(_recipeCover.RecipeLink);
            copyValues(ingridients, _recipe.Ingredients);
            copyValues1(steps, _recipe.InstuctionSteps);
            this.RecipeImage.Source = _recipeCover.ImageCover;
            this.RecipeNameTextBlock.Text = _recipeCover.RecipeName;
            this.CountryFalgImage.Source = _recipe.CountryFlag;
            this.CountryTextBlock.Text = _recipe.Country;
            this.PreparingTimeTextBlock.Text = _recipe.PreparingTime;
            this.KcalTextBlock.Text = _recipe.KCal;
            this.ProteinsTextBLock.Text = _recipe.Proteins;
            this.FatsTextBlock.Text = _recipe.Fats;
            this.CarbonHydratesTextBlock.Text = _recipe.Сarbohydrates;
            ProgressRing.IsActive = false;

        }

        private void copyValues(ObservableCollection<string> c, List<string> l)
        {
            foreach (var item in l)
            {
                c.Add(item);
            }
        }


        private void copyValues1(ObservableCollection<Step> c, List<Step> l)
        {
            foreach (var item in l)
            {
                c.Add(item);
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
                    ((StackPanel)pivotItem.Header).Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 87, 148, 212));
                    (((StackPanel)pivotItem.Header).Children[0] as TextBlock).Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));

                }
                else
                {
                    // Headers of other items to slightly darker
                    ((StackPanel)pivotItem.Header).Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                    (((StackPanel)pivotItem.Header).Children[0] as TextBlock).Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 87, 148, 212));

                }
            }
        }

    }
}
