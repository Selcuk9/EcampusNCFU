using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Io;
using Leaf.xNet;
using Newtonsoft.Json;

namespace Ecampus2
{
    public class PlacementJson
    {
       public static Json JsonPlacement { get; set; }
       public PlacementJson(string nameRequest)
        {
            HttpRequest request = new HttpRequest();
            request.UserAgent = Http.FirefoxUserAgent();
            request.KeepAlive = true;
            //request.AddHeader("Accept", "1");
            //request.AddHeader("Host", "	api.hh.ru");
            var response = request.Get($"https://api.hh.ru/vacancies?area=84&text={nameRequest}&page=0").ToString();
            var normalJson = Newtonsoft.Json.Linq.JObject.Parse(response).ToString();
            File.WriteAllText(@"C:\Users\79899\source\repos\Ecampus2\Ecampus2\JsonDataPlacement.json", normalJson);
            try
            {
                JsonPlacement = JsonConvert.DeserializeObject<Json>(File.ReadAllText(@"C:\Users\79899\source\repos\Ecampus2\Ecampus2\JsonDataPlacement.json", Encoding.UTF8));

            }
            catch (Exception b )
            {
                var a = b.Message;
            }
            
        }
        
    }


    public sealed class Json
    {
        public int found { get; set; }
        public Vacancy[] items { get; set; }
    }

    public sealed class Vacancy
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Money Salary { get; set; }
        public Terr area { get; set; }
        public Slavery Employer { get; set; }
    }
    public sealed class Money
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Currency { get; set; }
 
    }
    public sealed class Terr
    {
        public string Name { get; set; }
        
    }
    public sealed class Slavery
    {
        public string Name { get; set; }
        public string Id { get; set;}
    }
}
