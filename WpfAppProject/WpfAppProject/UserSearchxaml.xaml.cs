using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfAppProject
{
    /// <summary>
    /// Логика взаимодействия для UserSearchxaml.xaml
    /// </summary>
    public partial class UserSearchxaml : Page
    {
        List<Cooking_Recipes> items = null;
        public UserSearchxaml()
        {
            InitializeComponent();
            items = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
            DataGridRecipes.ItemsSource = items;

            var context = RecipesEntities1.GetContext();

            var adminUserIds = context.User.Where(u => u.Role == "admin").Select(u => u.ID).ToList();
            var adminUsernames = context.User.Where(u => adminUserIds.Contains(u.ID)).Select(u => u.Username).ToList();
            adminUsernames.Insert(0, "Все авторы");

            ComboBoxAuthor.ItemsSource = adminUsernames;
            ComboBoxAuthor.SelectedIndex = 0;
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxRecipe.Text == "" && ComboBoxAuthor.SelectedValue.ToString() == "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                DataGridRecipes.ItemsSource = items;
                return;
            }
            else if (TextBoxRecipe.Text != "" && ComboBoxAuthor.SelectedValue.ToString() == "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.Name == TextBoxRecipe.Text).ToList();
                if (items.Count == 0)
                {
                    MessageBox.Show("Такого блюда не существует, попробуйте снова");
                    return;
                }
                else
                {
                    DataGridRecipes.ItemsSource = items;
                    return;
                }
            }
            else if (TextBoxRecipe.Text == "" && ComboBoxAuthor.SelectedValue.ToString() != "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.User.Username.ToString() == ComboBoxAuthor.SelectedValue.ToString()).ToList();
                if (items.Count == 0)
                {
                    MessageBox.Show($"У {ComboBoxAuthor.SelectedValue.ToString()} нету рецептов");
                    return;
                }
                else
                {
                    DataGridRecipes.ItemsSource = items;
                    return;
                }
            }
            else if (TextBoxRecipe.Text != "" && ComboBoxAuthor.SelectedValue.ToString() != "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.User.Username.ToString() == ComboBoxAuthor.SelectedValue.ToString() && r.Name == TextBoxRecipe.Text).ToList();
                if (items.Count == 0)
                {
                    MessageBox.Show($"У {ComboBoxAuthor.SelectedValue.ToString()} нету такого рецепта");
                    return;
                }
                else
                {
                    DataGridRecipes.ItemsSource = items;
                    return;
                }
            }
        }
    }
}
