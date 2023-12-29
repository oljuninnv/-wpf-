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
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data;
using System.Globalization;
using static WpfAppProject.Authorization;

namespace WpfAppProject
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        public string action = "";
        List<Cooking_Recipes> items = null;
      
        public Search()
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

        //public static Cooking_Recipes RecipesEntities { get; set; }

        public bool isDirty = true;

        private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var context = RecipesEntities1.GetContext();
            switch (action)
            {
                case "add":
                    var addedRecipe = context.Cooking_Recipes.Local.Last();
                    if (addedRecipe != null)
                    {
                        context.Cooking_Recipes.Remove(addedRecipe);
                        context.SaveChanges();
                        DataGridRecipes.ItemsSource = null;
                        DataGridRecipes.ItemsSource = context.Cooking_Recipes.ToList();
                        MessageBox.Show("Последнее добавление рецепта было отменено");
                    }
                    break;
                case "undo":
                    DataGridRecipes.ItemsSource = null;
                    DataGridRecipes.ItemsSource = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                    break;
                case "":
                    DataGridRecipes.ItemsSource = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                    break;
            }

            MessageBox.Show("Отмена действий");
            isDirty = true;
            DataGridRecipes.IsReadOnly = true;
        }

        private void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridRecipes.ItemsSource = null;

            int UserId = Authorization.DataTransfer.UserID;

            var adminUsers = RecipesEntities1.GetContext().User.Where(u => u.ID == UserId).ToList();
            var items = new List<Cooking_Recipes>();
            foreach (var user in adminUsers)
            {
                var recipes = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.Creator == UserId).ToList();
                items.AddRange(recipes);
            }

            DataGridRecipes.ItemsSource = items;

            DataGridRecipes.IsReadOnly = false;
            DataGridRecipes.BeginEdit();
            action = "undo";
            isDirty = false;
            MessageBox.Show("Редактировать");
        }

        private void Edit_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int UserId = Authorization.DataTransfer.UserID;

            var items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.Creator == UserId).ToList();
            DataGridRecipes.ItemsSource = items;

            DataGridRecipes.IsReadOnly = false;
            int maxId = RecipesEntities1.GetContext().Cooking_Recipes.ToList().Max(i => i.ID);
            Cooking_Recipes cooking_Recipes = new Cooking_Recipes()
            {
                ID = maxId + 1,
                Name = "Введите название блюда",
                Description = "Введите описание",
                Recipe = "Введите рецепт",
                Creator = UserId,
                Date_of_Creation = System.DateTime.Now,
                Ingridients = "Введите ингридиенты",
            };
            RecipesEntities1.GetContext().Cooking_Recipes.Add(cooking_Recipes);
            try
            {
                RecipesEntities1.GetContext().SaveChanges();
                DataGridRecipes.ItemsSource = null;
                items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.Creator == UserId).ToList();
                DataGridRecipes.ItemsSource = items;
                DataGridRecipes.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            action = "add";
            isDirty = false;
        }

        private void New_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RecipesEntities1.GetContext().SaveChanges();
            DataGridRecipes.ItemsSource = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
            DataGridRecipes.Items.Refresh();
            DataGridRecipes.IsReadOnly = true;
            action = "";
            isDirty = true;
        }

        private void Save_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void Find_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = true;

            //Вывод всех рецептов
            if (TextBoxRecipe.Text == "" && ComboBoxAuthor.SelectedValue.ToString() == "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                DataGridRecipes.ItemsSource = items;
                return;
            }
            //Вывод всех рецептов с определённым названием
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
            ////Вывод всех рецептов у определённого автора
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
            ////Вывод определённого рецепта у определённого автора
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

        private void Find_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = true;
            int UserId = Authorization.DataTransfer.UserID;

            var items = RecipesEntities1.GetContext().Cooking_Recipes.Where(r => r.Creator == UserId).ToList();

            Cooking_Recipes cook = DataGridRecipes.SelectedItem as Cooking_Recipes;
            if (cook != null && cook.Creator == UserId)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные ",
               "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataGridRecipes.SelectedIndex =
                    DataGridRecipes.SelectedIndex == 0 ? 1 :
                    DataGridRecipes.SelectedIndex - 1;
                    RecipesEntities1.GetContext().Cooking_Recipes.Remove(cook);
                    RecipesEntities1.GetContext().SaveChanges();
                    DataGridRecipes.ItemsSource = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                    DataGridRecipes.Items.Refresh();
                }
            }
            else if (cook.Creator != UserId)
            {
                MessageBox.Show("Вы не можете удалять записи других пользователей");
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
            action = "";
        }

        private void Delete_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxRecipe.Text == "" && ComboBoxAuthor.SelectedValue.ToString() == "Все авторы")
            {
                items = RecipesEntities1.GetContext().Cooking_Recipes.ToList();
                DataGridRecipes.ItemsSource = items;
                return;
            }
            //Вывод всех рецептов с определённым названием
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
            ////Вывод всех рецептов у определённого автора
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
        }
    }
}