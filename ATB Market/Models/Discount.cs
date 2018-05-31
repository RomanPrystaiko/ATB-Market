using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ATB_Market.Models
{
    public class Discount
    {
        /// <summary>
        /// Image of product
        /// </summary>
        public BitmapImage Image { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// per cents of dicount of product
        /// </summary>
        public string DiscountDetails { get; set; }
        /// <summary>
        /// price before discount
        /// </summary>
        public double PreviousPrice { get; set; }
        /// <summary>
        /// current price after discount
        /// </summary>
        public string CurrentPrice { get; set; }
    }
}
