using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ATB_Market.Models
{
   public class Recipe
    {
        public List<string> Ingredients = new List<string>();
        public List<Step> InstuctionSteps = new List<Step>();
        public string Country { get; set; }
        public string PreparingTime { get; set; }
        public string KCal { get; set; }
        public string Proteins { get; set; }
        public string Fats { get; set; }
        public string Сarbohydrates { get; set; }
        public BitmapImage CountryFlag { get; set; }
    }
}
