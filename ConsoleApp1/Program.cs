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
            GetAttribute(GetDocument());
        }

        static HtmlDocument GetDocument()
        {
            var url = "https://xedap.vn/shop/xe-dap-duong-pho-touring-giant-escape-3-banh-700c-2021/";
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
    }
}
