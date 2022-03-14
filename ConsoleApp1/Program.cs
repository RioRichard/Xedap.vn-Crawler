using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Model;
using System.Net;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static DataContext context = new DataContext();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var AttrDict = new Dictionary<string, int>();

            GetCategoryURL(AttrDict);
            
        }
        static void GetCategoryURL(Dictionary<string, int> AttrDict)
        {
            var url = "https://xedap.vn";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var cate = doc.DocumentNode.SelectSingleNode("//li[@id='menu-item-5661']/ul");
            var li = cate.SelectNodes("./li");
            foreach (var item in li)
            {
                var tagA = item.FirstChild;
                var Category = new Category() { CategoryName = tagA.InnerText, Isdelete= false };
                context.Categories.Add(Category);
                context.SaveChanges();
                Console.WriteLine(tagA.InnerText);
                GetItemURL(tagA.GetAttributeValue("href", "null"), AttrDict,Category.IDCategory);
            }
        }

        static void GetItemURL(string url, Dictionary<string, int> AttrDict, int cateID)
        {
            
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var multiHref = doc.DocumentNode.SelectNodes("//div[@class='image-fade_in_back']");
            foreach (var item in multiHref)
            {
                var href = item.SelectSingleNode("./a");
                var link = href.GetAttributeValue("href", "nothing");
                Console.WriteLine(link);

                var itemDocument = GetDocument(link);

                var productId = GetName(itemDocument, cateID);
                GetAttribute(itemDocument, AttrDict, productId);
                Console.WriteLine("=========================================");
            }
        }
        static HtmlDocument GetDocument(string url)
        {
            
            var web = new HtmlWeb();
            return web.Load(url);
        }
        static void GetAttribute(HtmlDocument doc, Dictionary<string, int> AttrDict, int productId)
        {
            
            var table = doc.DocumentNode.SelectNodes("//table[@class='bike_specifications']/tbody/tr");
            if ( table == null)
                return;
            Console.WriteLine("-------------Attr-------------");
            foreach (var item in table)
            {
                //Console.WriteLine(item.InnerText);
                var th = item.SelectSingleNode("./th");
                if (th!=null && !(th.InnerText == "Thông số kỹ thuật"))
                {
                    Console.WriteLine(th.InnerText);
                    if (!AttrDict.ContainsKey(th.InnerText))
                    {
                        var Attr = new ConsoleApp1.Model.Attribute()
                        {
                            AttributeName = th.InnerText,
                            IsDelete = false
                        };
                        context.Attributes.Add(Attr);
                        context.SaveChanges();
                        AttrDict.Add(Attr.AttributeName, Attr.IDAttribute);
                    }
                }
                var td = item.SelectSingleNode("./td");
                if (td != null && !(td.InnerText == "Xem thêm") && !(td.InnerText == "Rút gọn"))
                {
                    Console.WriteLine(td.InnerText);
                    var AttrId = AttrDict[th.InnerText];
                    var value = td.InnerText;

                    if (td.InnerText.Length > 400)
                        value.Remove(400);
                    var ProductAttr = new ProductAttribute()
                    {
                        IDProduct = productId,
                        IDAttribute = AttrId,
                        AttributeValue = value,


                    };
                    context.ProductAttributes.Add(ProductAttr);
                    context.SaveChanges();
                }


                


            }
        }
        static int GetName (HtmlDocument doc, int cateID)
        {
            Console.WriteLine("-------------Info-------------");
            var title = doc.DocumentNode.SelectSingleNode("//h1[@class='product-title product_title entry-title']");
            Console.WriteLine(title.InnerText);
            Console.WriteLine("--------------------------------");
            
            var price = doc.DocumentNode.SelectSingleNode("//span[@class='woocommerce-Price-amount amount']");
            var total = price.InnerText.Replace(".",string.Empty).Replace("VND",string.Empty).Trim();
            Console.WriteLine(int.Parse(total));
            var img = doc.DocumentNode.SelectSingleNode("//img[@class='wp-post-image skip-lazy']");
            Console.WriteLine("--------------------------------");
            Console.WriteLine(img.GetAttributeValue("src",null));
            var description = doc.DocumentNode.SelectSingleNode("//div[@id='tab-description']");
            Console.WriteLine("--------------------------------");
            var descriptionText = description.InnerText.Replace("/n/n","/n");
            if (descriptionText.Length > 1000)
                descriptionText = descriptionText.Remove(1000);
            Console.WriteLine(descriptionText);

            var product = new Product()
            {
                Name = title.InnerText,
                IDCategory = cateID,
                Price = int.Parse(total),
                ImageURL = SaveImage(img.GetAttributeValue("src", null)),
                Stock = 100,


                Description = descriptionText,
                IsDelete = false,
            };
            

            context.Products.Add(product);

            context.SaveChanges();

            return product.IDProduct;

        }
        static string SaveImage(string imgUrl )
        {
            string path = string.Empty;
            if (!string.IsNullOrEmpty(imgUrl))
            {
                using(WebClient client = new WebClient())
                {
                    path = Path.GetFileName(imgUrl);
                    client.DownloadFile(imgUrl, "../../Image/"+path);
                }
            }
            return path;
        }
    }
}
