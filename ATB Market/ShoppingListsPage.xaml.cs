using ATB_Market.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class ShoppingListsPage : Page
    {
        ObservableCollection<ShoppingList> collection;
        
        public ShoppingListsPage()
        {
            this.InitializeComponent();
            this.collection = new ObservableCollection<ShoppingList>();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Height = 30;
            ContentDialog AddList = new ContentDialog()
            {
                Title = "Введіть назву списку",
                Content = tb,
                CloseButtonText = "Закрити",
                PrimaryButtonText = "Ok",
            };

            if (await AddList.ShowAsync() == ContentDialogResult.Primary)
            {
                ShoppingList l = new ShoppingList();
                l.Name = tb.Text;
                l.Number = collection.Count + 1;
                collection.Add(l);
            }



        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tb = new TextBlock();
            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            tb.Text = "Ви дійсно бажаєте видалити список?";
            tb.TextWrapping = TextWrapping.Wrap;

            ContentDialog AddList = new ContentDialog()
            {
                Title = tb,
                CloseButtonText = "Закрити",
                PrimaryButtonText = "Ok",
            };

            if (await AddList.ShowAsync() == ContentDialogResult.Primary)
            {
                DeleteList(sender);
            }

        }

        private void DeleteList(object sender)
        {
            var item = GetAncestorOfType<ListViewItem>(sender as Button);
            item.IsSelected = true;
            collection.RemoveAt(LV.SelectedIndex);

            for (int i = 0; i < collection.Count; i++)
            {
                collection[i].Number = i + 1;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProductListPage));
        }


        public T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }

    }
}
