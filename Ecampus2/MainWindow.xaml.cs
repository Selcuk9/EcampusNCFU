using System.Windows;
namespace Ecampus2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            usernameBox.Text = "";
            passwordBox.Password = "";
            WindowProfile profile;
            UserSessionData user = new UserSessionData(usernameBox.Text, passwordBox.Password);
            string resualt= await user.LoginAsync();
            if (resualt == "Success")
            {
                profile = new WindowProfile();
                profile.Show();
                //  GetJsonData.FormattingJson();
                this.Close();

            }
            else
            {
                checkBlock.Padding = new Thickness(10);
                checkBlock.Text = resualt;
            }
            
        }
    }
}
