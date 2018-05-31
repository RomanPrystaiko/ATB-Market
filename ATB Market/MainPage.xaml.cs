using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ATB_Market
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            HideBare();
            MyListBox.SelectedIndex = 0;
        }

        private async void HideBare()
        {
            var bar = StatusBar.GetForCurrentView();
            await bar.HideAsync();
        }


        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemName = ((((sender as ListBox).SelectedItem as ListBoxItem).Content as StackPanel).Children[1] as TextBlock).Text;
            SelItemPaneToTextBlock.Text = itemName;

            switch (MyListBox.SelectedIndex)
            {
                case 0:
                    MyFrame.Navigate(typeof(DiscountsPage));
                    break;
                case 1:
                    MyFrame.Navigate(typeof(RecipeCoverCollectionPage));
                    break;
                case 2:
                    MyFrame.Navigate(typeof(MapPage));
                    break;
                case 3:
                    MyFrame.Navigate(typeof(ShoppingListsPage));
                    break;
                default:
                    break;
            }

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MysplitView.IsPaneOpen = !MysplitView.IsPaneOpen;
        }
    }
}
