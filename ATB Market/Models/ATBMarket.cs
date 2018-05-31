using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ATB_Market.Models
{
    public class ATBMarket
    {
        HtmlParser parser;
        HttpClient client;
        IHtmlDocument _document;
        readonly Recipe _recipe;


        /// <summary>
        /// src to image of 7daysDiscount
        /// </summary>
        public string Image7daysDiscount { get; set; }

        /// <summary>
        /// src to image of EconomyDiscount
        /// </summary>
        public string ImageEconomuDiscount { get; set; }

        private readonly ObservableCollection<Discount> _economyDiscountsCollection = new ObservableCollection<Discount>();
        private readonly ObservableCollection<Discount> _7DaysDiscountsCollection = new ObservableCollection<Discount>();
        private readonly ObservableCollection<RecipeCover> _recipesCoverCollection = new ObservableCollection<RecipeCover>();

        public ATBMarket()
        {
            client = new HttpClient();
            parser = new HtmlParser();
            _recipe = new Recipe();
        }


        public async Task<Recipe> GetRecipeAsync(string RecipeURL)
        {

            var document = await GetDocumentAsync(RecipeURL);

            _recipe.PreparingTime = GetPreparingTime(document);
            _recipe.Country = GetCountry(document);
            _recipe.KCal = GetKCal(document);
            _recipe.Fats = GetFats(document);
            _recipe.Proteins = GetProteins(document);
            _recipe.Сarbohydrates = GetCarbonhydrates(document);
            _recipe.Ingredients = GetIngridients(document);
            _recipe.InstuctionSteps = GetPreparingSteps(document);
            _recipe.CountryFlag = GetCountryImage(_recipe.Country);
            return _recipe;
        }


        private BitmapImage GetCountryImage(string countryName)
        {
            BitmapImage image = new BitmapImage();

            switch (countryName)
            {
                case "Італія":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Italy.png", UriKind.Absolute);
                    break;
                case "Франція":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/France.png", UriKind.Absolute);
                    break;
                case "Іспанія":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Spain.png", UriKind.Absolute);
                    break;
                case "Росія":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Russia.png", UriKind.Absolute);
                    break;
                case "Україна":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Ukraine.png", UriKind.Absolute);
                    break;
                case "Туреччина":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Turkey.png", UriKind.Absolute);
                    break;
                case "Японія":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Japan.png", UriKind.Absolute);
                    break;
                case "США":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/USA.png", UriKind.Absolute);
                    break;
                case "Домашня / народна":
                    image.UriSource = new Uri("ms-appx:///Assets/Flags/Global.png", UriKind.Absolute);
                    break;
                default:
                    break;
            }


            return image;
        }

        private List<Step> GetPreparingSteps(IHtmlDocument document)
        {
            var list = document.QuerySelector("div.article_body").Children[1].QuerySelectorAll("li").Select((item) => { return item.TextContent; }).ToList();
            var l = new List<Step>();
            foreach (var item in list)
            {
                Step s = new Step();
                s.step = item;
                s.number = list.IndexOf(item) + 1;
                l.Add(s);
            }
            return l;
        }


        private List<string> GetIngridients(IHtmlDocument document)
        {
            var list = document.QuerySelector("div.article_body").Children[0].QuerySelectorAll("li").Select((item) => { return item.TextContent; });
            var l = new List<string>();

            foreach (var item in list)
            {

                var n = item.Replace("\n", "");
                var ll = n.Replace("  ", "");
                l.Add(ll);
            }
            return l;
        }

        private string GetProteins(IHtmlDocument document)
        {
            var kcall = document.QuerySelector("div.article_body").Children[2].QuerySelectorAll("li");
            var kcal = kcall[1].QuerySelector("span").TextContent;
            var k = kcal.Replace("\n", "");
            return k;
        }

        private string GetFats(IHtmlDocument document)
        {
            var kcall = document.QuerySelector("div.article_body").Children[2].QuerySelectorAll("li");
            var kcal = kcall[2].QuerySelector("span").TextContent;
            var k = kcal.Replace("\n", "");
            return k;
        }

        private string GetCarbonhydrates(IHtmlDocument document)
        {
            var kcall = document.QuerySelector("div.article_body").Children[2].QuerySelectorAll("li");
            var kcal = kcall[3].QuerySelector("span").TextContent;
            var k = kcal.Replace("\n", "");
            return k;
        }



        private string GetKCal(IHtmlDocument document)
        {
            var kcall = document.QuerySelector("div.article_body").Children[2].QuerySelectorAll("li");
            var kcal = kcall[0].QuerySelector("span").TextContent;
            var k = kcal.Replace("\n", "");
            return k;
        }


        private string GetCountry(IHtmlDocument document)
        {
            var preparingTime = document.QuerySelector("div.article_info").QuerySelectorAll("div.text");
            var country = preparingTime[1].QuerySelector("span").TextContent;
            return country;
        }


        private string GetPreparingTime(IHtmlDocument document)
        {
            var preparingTime = document.QuerySelector("div.article_info").QuerySelector("div.text").QuerySelector("span").TextContent;
            return preparingTime;
        }


        /// <summary>
        /// Gets asynchroniously collection of recipeCovers 
        /// </summary>
        /// <returns>OservableCollection of RecipeCover</returns>
        public async Task<ObservableCollection<RecipeCover>> GetRecipeCoversCollectionAsync()
        {
            var document = await GetDocumentAsync("https://www.atbmarket.com/trademark/advices");
            var ul = document.QuerySelector("ul.brand_list");
            var liCol = ul.QuerySelectorAll("li");
            foreach (var item in liCol)
            {
                RecipeCover reC = new RecipeCover();
                reC.RecipeName = item.QuerySelector("h2.title").QuerySelector("a").TextContent;
                reC.RecipeLink = "https://www.atbmarket.com" + item.QuerySelector("a").GetAttribute("href");
                //string imgSRC = "https://www.atbmarket.com/" + item.QuerySelector("a").QuerySelector("img").GetAttribute("src");
                //reC.ImageCover = new BitmapImage(new Uri(imgSRC,UriKind.Absolute));

                reC = GetImage(reC);
                _recipesCoverCollection.Add(reC);

            }
            return _recipesCoverCollection;
        }


        private RecipeCover GetImage(RecipeCover recipe)
        {
            switch (recipe.RecipeName)
            {
                case "Соус Італійський":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/ItalianSouce.jpg", UriKind.Absolute));
                    break;
                case "Сирна паска з білим шоколадом":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/CheesePaskaWithWhiteChocolate.jpg", UriKind.Absolute));
                    break;
                case "Торт \"Наполеон\"":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Napoleon.jpg", UriKind.Absolute));
                    break;
                case "Солодкий пиріг з гарбуза":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/SweetPie.jpg", UriKind.Absolute));
                    break;
                case "Суп Езо Гелин чорбаси":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/EzoSoup.jpg", UriKind.Absolute));
                    break;
                case "Салат «Нісуаз» з тунцем ТМ «De Luxe Foods & Goods Selected»":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/NisuazSalat.jpg", UriKind.Absolute));
                    break;
                case "Кальцоне з лососевим фаршем і шпинатом":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Calcone.jpg", UriKind.Absolute));
                    break;
                case " Паелья з морепродуктами ":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Paelya.jpg", UriKind.Absolute));
                    break;
                case "Польова каша":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Porridge.jpg", UriKind.Absolute));
                    break;
                case "Куліч Пасхальний":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Paska.jpg", UriKind.Absolute));
                    break;
                case "Кава з пінкою маршмеллоу":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Coffee.jpg", UriKind.Absolute));
                    break;
                case "Бланманже сирне":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/BlanMange.jpg", UriKind.Absolute));
                    break;
                case "Паста з сьомгою і креветками у вершковому соусі":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Pasta.jpg", UriKind.Absolute));
                    break;
                case "Ватрушки з сиром":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Vatrushka.jpg", UriKind.Absolute));
                    break;
                case "Окрошка з гірчицею":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Okroshka.jpg", UriKind.Absolute));
                    break;
                case "Піца Пепероні":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Pizza.jpg", UriKind.Absolute));
                    break;
                case "Різдвяна полента":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/polenta.jpg", UriKind.Absolute));
                    break;
                case "Желейний торт «Мозаїка»":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Mozaika.jpg", UriKind.Absolute));
                    break;
                case "Салат з креветками і помідорами чері":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/SalatWithCheri.jpg", UriKind.Absolute));
                    break;
                case "Роли Філадельфія":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Rolly.jpg", UriKind.Absolute));
                    break;
                case "Сирна закуска на чіпсах":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/CheeseWithChips.jpg", UriKind.Absolute));
                    break;
                case "Вівсяно-арахісова гранола":
                    recipe.ImageCover = new BitmapImage(new Uri("ms-appx:///Assets/Images/Granola.jpg", UriKind.Absolute));
                    break;
                default:
                    break;
            }

            return recipe;
        }


        /// <summary>
        /// sets properties for image src of Economy and 7days discounts
        /// </summary>
        /// <returns></returns>
        public async Task GetDiscountsImages()
        {
            var document = await GetDocumentAsync("https://www.atbmarket.com/hot/akcii/");
            var div = document.QuerySelector("div.list_wrapper");
            var ul = div.QuerySelector("ul");
            var images = ul.QuerySelectorAll("img");

            List<string> imagesSRC = new List<string>();

            foreach (var item in images)
            {
                var src = item.GetAttribute("src");
                imagesSRC.Add(src);
            }

            this.ImageEconomuDiscount = "https://www.atbmarket.com" + imagesSRC[0];
            this.Image7daysDiscount = "https://www.atbmarket.com" + imagesSRC[1];

        }

        /// <summary>
        /// return asynchroniously the EconomyDiscountsCollection
        /// </summary>
        /// <returns>ObservableCollection of Discount</returns>
        public async Task<ObservableCollection<Discount>> GetEconomyDiscounts()
        {

            var document = await GetDocumentAsync("https://www.atbmarket.com/hot/akcii/economy/");
            var div = document.QuerySelector("div.list_wrapper");
            var li = div.QuerySelectorAll("li");

            foreach (var item in li)
            {
                try
                {
                    Discount discount = new Discount();
                    var image = item.QuerySelector("img");
                    string src = image.GetAttribute("src");

                    discount.Image = new BitmapImage(new Uri("https://www.atbmarket.com/" + src, UriKind.Absolute));

                    var desc = item.QuerySelector("span.promo_info_text").TextContent.Replace("\n                            ", " ");
                    var des = desc.Replace("\n                       ", "");
                    var d = des.Remove(0, 1);
                    discount.Description = d;

                    var details = item.QuerySelector("div.economy_price").TextContent.Replace("\n\t\t\t\t\t\t\t\t\t\t", " ");

                    var det = details.Replace("\t\t\t\t\t\t\t\t\t", "");

                    discount.DiscountDetails = det;

                    var price = item.QuerySelector("div.promo_price").TextContent;

                    var p = price.Insert(price.Length - 5, ".");
                    var p1 = p.Insert(price.Length - 2, " ");
                    discount.CurrentPrice = p1;

                    discount.PreviousPrice = double.Parse(item.QuerySelector("span.promo_old_price").TextContent);

                    _economyDiscountsCollection.Add(discount);
                }

                catch (Exception ex)
                {
                    ex.ToString();
                }

            }

            return _economyDiscountsCollection;
        }



        /// <summary>
        /// getsAsync 7Days discounts by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>ObservableCollection of Discounts</returns>
        public async Task<ObservableCollection<Discount>> Get7daysDiscountsCollection(int category)
        {

            if (_document == null)
            {
                _document = await GetDocumentAsync("https://www.atbmarket.com/hot/akcii/7day/");

                Continueparsing(category);

            }
            else
            {
                Continueparsing(category);
            }

            return _7DaysDiscountsCollection;
        }





        private void Continueparsing(int category)
        {

            var div = _document.QuerySelector("div.tab-content");
            //var ulCollection = div.QuerySelectorAll("ul");

            switch (category)
            {
                case 1:
                    var ul1 = div.QuerySelector("ul[id='cat1']");
                    ParseCategoryOf7dayDiscount(ul1);
                    break;
                case 2:
                    var ul2 = div.QuerySelector("ul[id='cat2']");
                    ParseCategoryOf7dayDiscount(ul2);
                    break;
                case 3:
                    var ul3 = div.QuerySelector("ul[id='cat3']");
                    ParseCategoryOf7dayDiscount(ul3);
                    break;
                case 4:
                    var ul4 = div.QuerySelector("ul[id='cat4']");
                    ParseCategoryOf7dayDiscount(ul4);
                    break;
                //case 7:
                //    var ul7 = div.QuerySelector("ul[id='cat7']");
                //    ParseCategoryOf7dayDiscount(ul7);
                //    break;
                default:
                    var ul11 = div.QuerySelector("ul[id='cat1']");
                    ParseCategoryOf7dayDiscount(ul11);
                    break;
            }
        }





        private void ParseCategoryOf7dayDiscount(IElement document)
        {
            _7DaysDiscountsCollection.Clear();

            var liColl = document.QuerySelectorAll("li");
            foreach (var item in liColl)
            {
                try
                {
                    Discount dis = new Discount();

                    var imgSrc = item.QuerySelector("img").GetAttribute("src");
                    dis.Image = new BitmapImage(new Uri("https://www.atbmarket.com/" + imgSrc, UriKind.Absolute));

                    var desc = item.QuerySelector("span.promo_info_text").TextContent;
                    var d = desc.Replace("\n", "");
                    var dd = d.Replace("   ", "");
                    var ddd = dd.Remove(0, 1); 
                    dis.Description = ddd;

                    var currPrice = item.QuerySelector("div.promo_price").TextContent;

                    var p = currPrice.Insert(currPrice.Length - 5, ".");
                    var p1 = p.Insert(p.Length - 3, " ");

                    dis.CurrentPrice = p1;

                    _7DaysDiscountsCollection.Add(dis);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

            }

        }


        private async Task<IHtmlDocument> GetDocumentAsync(string url)
        {
            var response = await client.GetStringAsync(url);
            var document = await parser.ParseAsync(response);
            return document;
        }



    }
}
