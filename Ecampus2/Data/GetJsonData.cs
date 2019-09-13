using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using System.IO;


namespace Ecampus2.Data
{
    public class GetJsonData
    {
        protected static Obj DataObj { get; set; } 
        public void FormattingJson()    
        {
            string response = UserSessionData.request.Get("https://ecampus.ncfu.ru/studies").ToString();
            IHtmlDocument htmlStudies = new HtmlParser().ParseDocument(response);
            string doc = htmlStudies.QuerySelector("[type='text/javascript']").InnerHtml;
            var json = doc.Remove(0, doc.LastIndexOf("=") + 2);
            var jObj = json.Remove(json.Length - 2);
            var normalJson = Newtonsoft.Json.Linq.JObject.Parse(jObj).ToString();
            string path = @"C:\Users\79899\source\repos\Ecampus2\Ecampus2\Data\Object.json";
            File.WriteAllText(path, normalJson);
            //var obj = File.Exists(path) ? JsonConvert.DeserializeObject<Obj>(File.ReadAllText(path)):
            DataObj = JsonConvert.DeserializeObject<Obj>(File.ReadAllText(path));   
        }
        
    }
    public class Obj
    {
        public Specialities[] specialities { get; set; }
        public int Kod_cart { get; set; }
        public int portionSize { get; set; }
    }
    public class Specialities
    {
        [JsonProperty("Id")]
        public int StudentId { get; set; } // Ид студента
        public string Name { get; set; } // Направление (ПИН)
        public Years[] AcademicYears { get; set; }
    }

    public class Years
    {
        public Terms[] Terms { get; set; }
    }

    public class Terms
    {
        public string TermTypeName { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public Cours[] Courses { get; set; }
    }

    public class Cours
    {
        public Lesson[] LessonTypes { get; set; }
        public string Name { get; set; } // название предмета 
    }

    public class Lesson
    {
        public int Id { get; set; } // id раздела 
        public string Name { get; set; } // Название раздела
    }
}
