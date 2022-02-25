using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var doc = GetDocument();
            GetAttribute(doc);
            GetName(doc);
        }

        static HtmlDocument GetDocument()
        {
            var url = "https://xedap.vn/shop/xe-dap-tre-em-youth-trinx-junior-1-0-disc-20-phanh-dia-banh-20-inches-2021/";
            var web = new HtmlWeb();
            return web.Load(url);
        }
        static void GetAttribute(HtmlDocument doc)
        {
            
            var table = doc.DocumentNode.SelectNodes("//table[@class='bike_specifications']/tbody/tr");
            
            Console.WriteLine(table.Count);
            foreach (var item in table)
            {
                //Console.WriteLine(item.InnerText);
                var th = item.SelectSingleNode("./th");
                if (th!=null && !(th.InnerText == "Thông số kỹ thuật"))
                {
                    Console.WriteLine(th.InnerText);
                }
                var td = item.SelectSingleNode("./td");
                if (td != null && !(td.InnerText == "Xem thêm"))
                {
                    Console.WriteLine(td.InnerText);
                }
                
                
                Console.WriteLine("================================");

            }
        }
        static void GetName (HtmlDocument doc)
        {
            var title = doc.DocumentNode.SelectSingleNode("//h1[@class='product-title product_title entry-title']");
            Console.WriteLine(title.InnerText);
            
            var price = doc.DocumentNode.SelectSingleNode("//span[@class='woocommerce-Price-amount amount']");
            Console.WriteLine(price.InnerText);
            var img = doc.DocumentNode.SelectSingleNode("//img[@class='wp-post-image skip-lazy']");
            Console.WriteLine(img.GetAttributeValue("src",""));
            var description = doc.DocumentNode.SelectSingleNode("//div[@id='tab-description']");
            Console.WriteLine(description.InnerText);
        }
    }
}
