//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppProject
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Documents;

    public partial class Cooking_Recipes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recipe { get; set; }
        public int Creator { get; set; }
        public System.DateTime? Date_of_Creation { get; set; }
        public string Ingridients { get; set; }

        public virtual User User { get; set; }


        public List<User> Users {
            get {

                List<User> users = RecipesEntities1.GetContext().User.ToList();

                return users;
            }
            }
    }
}
