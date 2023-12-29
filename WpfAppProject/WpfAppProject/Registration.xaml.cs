using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfAppProject
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public class DataTransfer
        {
            public static int UserID { get; set; }
            public static string Username { get; set; }
            public static string role { get; set; }
            public static string Password { get; set; }
            
        }
        public Registration()
        {
            InitializeComponent();
            UsernameLabel.Visibility = Visibility.Collapsed;
            Password1Label.Visibility = Visibility.Collapsed;
            Password2Label.Visibility = Visibility.Collapsed;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsernameLabel.Content = "";
            Password1Label.Content = "";
            Password2Label.Content = "";

            Regex usernameRegex = new Regex(@"^[a-zA-Zа-яА-Я]{3,20}$");
            Regex passwordRegex = new Regex(@"^.{10,20}$");

            if (!usernameRegex.IsMatch(UsernameTextBox.Text))
            {
                UsernameLabel.Content = "Имя пользователя должно содержать \nот 3 до 20 символов (латинские или русские буквы)";
                UsernameLabel.Visibility = Visibility.Visible;
                return;
            }
            if (!passwordRegex.IsMatch(Password1TextBox.Text))
            {
                Password1Label.Content = "Пароль должен содержать от 10 до 20 символов";
                Password1Label.Visibility = Visibility.Visible;
                return;
            }
            if(Password1TextBox.Text != Password2TextBox.Text)
            {
                Password2Label.Content = "Пароли должны совпадать";
                Password2Label.Visibility = Visibility.Visible;
                return;
            }

            int UserNumber = RecipesEntities1.GetContext().User.ToList().Max(i => i.ID);

            if (UserNumber == 0)
            {
                UserNumber = 1;
            }
            else if (UserNumber == 1)
            {
                UserNumber++;
            }

            User user = new User()
            {
                ID = UserNumber + 1,
                Username = UsernameTextBox.Text,
                Password = Password1TextBox.Text,
                Role = "user",
            };

            RecipesEntities1.GetContext().User.Add(user);
            try
            {
                RecipesEntities1.GetContext().SaveChanges();
                MessageBox.Show("Успешная регистрация");

                DataTransfer.UserID = UserNumber + 1;
                DataTransfer.Username = UsernameTextBox.Text;
                DataTransfer.Password = Password1TextBox.Text;
                DataTransfer.role = "user";               

                NavigationService.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
    }
}
