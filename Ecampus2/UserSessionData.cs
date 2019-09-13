using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Leaf.xNet;
using System.Threading.Tasks;

namespace Ecampus2
{
    public class UserSessionData
    {
        public static HttpRequest request { get;} = new HttpRequest();
        public string Password { get; set; }
        public string Username { get; set; }

        public UserSessionData(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Login()
        {
            string link = "https://ecampus.ncfu.ru/account/login?returnUrl=%2Faccount";
            HttpResponse response = request.Get(link);
            HtmlParser parser = new HtmlParser();
            var doc = parser.ParseDocument(response.ToString());
            var RequestVerificationToken = doc.QuerySelector("[name='__RequestVerificationToken']");
            string token = ((IHtmlInputElement)RequestVerificationToken).DefaultValue;
            #region Cookie
            //CookieStorage storage = new CookieStorage();
            //var cookie = response.Cookies.Container.GetCookies(new Uri(link))[0]; // Get cookie
            //storage.Set(cookie);
            //var a = cookie.GetType();
            #endregion
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Referer", "https://ecampus.ncfu.ru/account/login?returnUrl=%2Faccount");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.KeepAlive = true;
            request.UserAgent = Http.FirefoxUserAgent();
            //////////////////////////Params//////////////////////////
            var reqParams = new RequestParams();
            reqParams["Login"] = Username; //Password  
            reqParams["Password"] = Password; //Username  
            reqParams["__RequestVerificationToken"] = token;
            var document = parser.ParseDocument(request.Post("https://ecampus.ncfu.ru/account/login?returnUrl=%2Faccount", reqParams).ToString()).QuerySelector(".validation-summary-errors ul li");
            if (document == null)
            {
                return "Success";
            }
            else
            {
                string check = document.InnerHtml;
                return check;
            }
        }
        public async Task<string> LoginAsync()
        {
            string check = await Task.Run(() => Login());
            return check;
        }
    }
}
