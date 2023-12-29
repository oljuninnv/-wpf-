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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    { 
        public Authorization()
        {
            InitializeComponent();
            UsernameLabel.Visibility = Visibility.Collapsed;
            PasswordLabel.Visibility = Visibility.Collapsed;
        }
        public class DataTransfer
        {
            public static int UserID { get; set; }
            public static string Username { get; set; }
            public static string role { get; set; }
            public static string Password { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsernameLabel.Content = "";
            PasswordLabel.Content = "";

            if (UsernameTextBox.Text == "")
            {
                UsernameLabel.Content = "Введите логин";
                UsernameLabel.Visibility = Visibility.Visible;
                return;
            }

            if (PasswordTextBox.Text == "")
            {
                PasswordLabel.Content = "Введите пароль";
                PasswordLabel.Visibility = Visibility.Visible;
                return;
            }

            User user = new User();

            user = RecipesEntities1.GetContext().User.Where(u  => u.Username == UsernameTextBox.Text && u.Password == PasswordTextBox.Text).FirstOrDefault();

            if(user == null)
            {
                MessageBox.Show("Такого пользователя не существует или вы ввели неправильно имя или пароль");
                return;
            }

            DataTransfer.UserID = user.ID;
            DataTransfer.Username = user.Username;
            DataTransfer.role = user.Role;
            DataTransfer.Password = user.Password;

            NavigationService.Navigate(new MainPage());
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

    }
}
