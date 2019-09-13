using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Parser;
using Ecampus2.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Leaf.xNet;

namespace Ecampus2

{
    public class DataStudent : GetJsonData, IDataStudent
    {
        public List<string> MyAccount()
        {
            List<string> data = new List<string>();
            var response = UserSessionData.request.Get("https://ecampus.ncfu.ru/account").ToString();
            var htmlAcc = new HtmlParser().ParseDocument(response);
            data.Add(htmlAcc.QuerySelector(".username").InnerHtml);
            var collection = htmlAcc.QuerySelectorAll("ul.breadcrumb li").ToList();
            foreach (var items in collection)
            {
                data.Add(items.TextContent);
            }
            return data; 
        }

        public void Placement(string nameReq)
        {
          var placement =  new PlacementJson(nameReq);
        }

        public void Rating()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Бета метод в дальнейшем надо будет вводить информацию о пользователи автоматом 
        /// </summary>
        public List<Expander> Lessons()
        {
            
        
            int studentId = DataObj.specialities[0].StudentId;
            var NameSpecialities = DataObj.specialities[0].Name;
            string NameObject = DataObj.specialities[0].AcademicYears[2 - 1].Terms[1 - 1].Courses[0].Name;// Тут должно выбрать тот курс в котором студент учится или перешел Terms-семестр
            int IdCotegory = DataObj.specialities[0].AcademicYears[2 - 1].Terms[1 - 1].Courses[0].LessonTypes[0].Id;
            string NameCotegory = DataObj.specialities[0].AcademicYears[2 - 1].Terms[1 - 1].Courses[0].LessonTypes[0].Name;
           
            int count = DataObj.specialities[0].AcademicYears[1].Terms[0].Courses.Length;
            Dictionary<string, List<Lesson>> dic = new Dictionary<string, List<Lesson>>();
            List<Expander> expanders = new List<Expander>();
            for (int i = 0; i < count; i++)
            {
                dic.Add(DataObj.specialities[0].AcademicYears[1].Terms[0].Courses[i].Name,
                DataObj.specialities[0].AcademicYears[1].Terms[0].Courses[i].LessonTypes.ToList());

                expanders.Add(new Expander()
                {
                    Header = dic.ElementAt(i).Key,
                    MaxHeight = 200,
                    IsExpanded = false,
                    Margin = new Thickness(5),
                    Padding = new Thickness(0),
                    BorderBrush = Brushes.Black,
                   
                    Foreground = Brushes.Aqua,
                    FontFamily = new FontFamily("Times New Roman Bold"),
                    FontSize = 18,
                });
            }
            return expanders;
                 
            
           
            
           // profile.GridMain.Children.Add(expanders[0]);   // не сохранился в гриде надо понять
        }

        public string Teachers()
        {
            throw new NotImplementedException();
        }

        public string WiFI()
        {
                string response = UserSessionData.request.Get("https://ecampus.ncfu.ru/DomainAccountInfo").ToString();
                var nameWifi = (new HtmlParser().ParseDocument(response)).QuerySelector(".form-control-static").InnerHtml.Remove(0,3).Replace("</b>"," ");
                response = UserSessionData.request.Post("https://ecampus.ncfu.ru/DomainAccountInfo/GetDomenP").ToString();
                string a = response.Remove(0, 1).Replace("\0", null).Replace('"',new char());
                return nameWifi + response;
        }

        public void Schedule()
        {
            #region parseOnTheWeb
            //string response = UserSessionData.request.Get("https://ecampus.ncfu.ru/schedule").ToString();
            //try
            //{
            //    var fillSchedule = new HtmlParser().ParseDocument(response).QuerySelectorAll("table tbody");
            //    var titles = fillSchedule.Select(m => m.TextContent);
            //    foreach (var items in fillSchedule)
            //    {
            //        var a = items.TextContent;
            //    }
            //}
            //catch (Exception)
            //{
            //}
            #endregion
            DateTime date = DateTime.Now;
            
        }
    }
}
