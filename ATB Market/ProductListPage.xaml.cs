using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ATB_Market
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductListPage : Page
    {
        ObservableCollection<string> collection = new ObservableCollection<string>();

        public ProductListPage()
        {
            this.InitializeComponent();
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
                collection.Add(tb.Text);
            }

        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var textBlock1 = (sender as ListView).SelectedItem;

            textBlock1 = String.Format("<del>{0}</del>", textBlock1);

            var textBlock = ((sender as ListView).SelectedItem as ListViewItem).Content as TextBlock;

            if (textBlock.TextDecorations == Windows.UI.Text.TextDecorations.Strikethrough)
            {
                textBlock.TextDecorations = Windows.UI.Text.TextDecorations.None;
            }
            else
            {
                textBlock.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
            }

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
