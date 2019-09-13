using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ecampus2.Data;
namespace Ecampus2
{

    
    public partial class WindowProfile : Window
    {
        private DataStudent student= new DataStudent();
        public WindowProfile()
        {
            InitializeComponent();
            WindowStyle window = new WindowStyle();
            Panel.SetZIndex(LessonMain, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10+(150*index),0,0,0);

            switch (index)
            {
                case 0:
                    {
                        new GetJsonData().FormattingJson();
                        List<Expander> expanders = student.Lessons();
                        Panel.SetZIndex(LessonMain, 1);
                        Panel.SetZIndex(GridWiFi, 0);
                        Panel.SetZIndex(GridPlacement, 0);
                        foreach (var items in expanders)
                            StackMain.Children.Add(items);
                        break;
                    }
                    
                case 1:
                    student.Schedule();
                    break;
                case 2:
                    {
                        //string pass = student.WiFI();
                        Panel.SetZIndex(LessonMain, 0);
                        Panel.SetZIndex(GridWiFi, 1);
                        Panel.SetZIndex(GridPlacement, 0);
                        break;
                    }
                case 3:
                    Panel.SetZIndex(LessonMain, 0);
                    Panel.SetZIndex(GridWiFi, 0);
                    Panel.SetZIndex(GridPlacement, 1);
                    break;
                case 4:
                   

                    break;
                case 5:
                    StackMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    StackMain.Background = Brushes.HotPink;
                    break;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            student = new DataStudent();
            var account = new AccountProfile();
            var data = student.MyAccount();
            account.StudentName.Content = data[0];
            account.University.Content = data[1];
            account.Institute.Content = data[2];
            account.Kafedra.Content = data[4];
            account.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string pass = student.WiFI();
            string[] a = pass.Split(' ');
            textBox1.Text = a[0];
            textbox2.Text = a[1];
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            JobsStack.Children.RemoveRange(0, JobsStack.Children.Count);
            student = new DataStudent();
            student.Placement(SearchBox.Text);
            foreach (var items in PlacementJson.JsonPlacement.items)
            {
                string price;
                if (items.Salary == null)
                    price = null;
                else if (items.Salary.To == null)
                    price = items.Salary.From +  " "+ items.Salary.Currency;
                else
                    price = "from " + items.Salary.From + "-" + 
                        items.Salary.To.ToString() +"."+
                        items.Salary.Currency;
                


                #region instance
                //var BlockProfession = new TextBlock()
                //{
                //    Foreground = Brushes.DarkBlue,
                //    Text = items.Name
                //};
                //var BlockCompany = new TextBlock()
                //{
                //    Foreground = Brushes.Black,
                //    Text = items.Employer.Name
                //};
                //var BlockArea = new TextBlock()
                //{
                //    Foreground = Brushes.Black,
                //    Text = items.area.Name
                //};
                //var BlockMoney = new TextBlock()
                //{
                //    Foreground = Brushes.Black,
                //    Text = items.Salary.From + " " + items.Salary.To
                //};
                #endregion
                var infoJobs = new List<TextBlock>();
              
                infoJobs.Add(new TextBlock()
                {
                    // Element money
                    Foreground = Brushes.Black,
                    Text = price,
                    Margin = new Thickness(800, 5, 5, 5),
                    FontSize = 15
                    
                });
                infoJobs.Add(new TextBlock()
                {
                    // Element name of region
                    Foreground = Brushes.Black,
                    Text = items.area.Name
                });
                infoJobs.Add(new TextBlock()
                {
                    // Element name of company
                    Foreground = Brushes.Black,
                    Text = items.Employer.Name
                });
                infoJobs.Add(new TextBlock()
                {
                    // Element Profession
                    Foreground = Brushes.DarkBlue,
                    Text = items.Name
                });
                infoJobs.Reverse();
                var stackPanel = new StackPanel();
                for (int i = 0; i < infoJobs.Count; i++)
                {
                    stackPanel.Children.Add(infoJobs[i]);
                }
                
                var listView = new ListViewItem();
                listView.Content = stackPanel;
                listView.Background = Brushes.DarkGray;
                listView.Margin = new Thickness(5);
                listView.Uid = items.Id.ToString();
                listView.PreviewMouseLeftButtonUp +=ListViewItem_Click;


                JobsStack.Children.Add(listView);
                
            }
           

        }

        private void ListViewItem_Click(object sender, MouseButtonEventArgs e)
        {
            //int index = int.Parse(((Button)e.Source).Uid);
            string id = ((UIElement)sender).Uid;
            System.Diagnostics.Process.Start($"https://stavropol.hh.ru/vacancy/{id}?query={SearchBox.Text}");
        }
    }
}
