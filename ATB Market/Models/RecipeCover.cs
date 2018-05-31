using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ATB_Market.Models
{
    public class RecipeCover
    {
        public BitmapImage ImageCover { get; set; }
        public string RecipeName { get; set; }
        public string RecipeLink { get; set; }
    }
}
