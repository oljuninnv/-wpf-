   M S B A M L   `   `   `  ����H  DWpfAppProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfAppProjectWpfAppProject  5      "    WpfAppProject.UserSearchxaml   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfAppProject     5        N� Title$  UserSearchxaml��5	      .��5
      � ���	#FFFCAE98 � ColumnDefinitions 5   
   �� 5   
   $
��478* �6   �� 5   
   $
��259* �6    � RowDefinitions 5   
   �� 5      $	��20* �6   �� 5      $
��100* �6   �� 5      $
��150* �6   .+�5   
   �� $��0��6   $��5,0,0,5q�6!   $��20(�62   $��0��6@   $
��Left=�6P   $��Bottom=�6k   $��27��6�   $	��159��6�   Список рецептов6�   &   System.Windows.Controls.DataGrid  5      -   5        $��DataGridRecipes��5      $��1��6    � Cursor$
 Hand��64      
IsReadOnly . $
��Auto��6|   $
��Auto��6�   $��4q�6�      AutoGenerateColumns .  ��6B   �� *Cooking_Recipes+   Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6!   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+	 �� Mode$	 TwoWay=�  5      $ Описание��6!    63   �� *Description+  5      $ Ингредиенты��6!    66   �� *Ingridients+  5      $) #Шаги приготовления��6!    6=   �� *Recipe+$	 TwoWay=�  5      $ 
Автор��6&    65   �� 
 �� Path$
 User.Username�$	 TwoWay=�  5      $ Дата создания��6&    6=   �� *Date_of_Creation+ �� StringFormat$ 
yyyy-MM-dd��5       �� 5"      $��
Поиск��6   $
��Left=�6   $��20(�67   $	��Top=�6E   $��38��6]   $��66��6i   $��1��6t   $��1��6�   $��	131,0,0,0q�6�   �� 5#      -   5        $��TextBlockRecipe��5#      $��
Блюдо��6'   $��15(�64   $	��Top=�6B   $
��Left=�6[   $��45��6v   $��20��6�   $��1��6�   $��1��6�   $��8,44,0,0q�6�   �� 5$      -   5        $��TextBlockAuthor��5$      $��
Автор��6'   $��15(�64   $
��Left=�6B   $��41��6]   $��Bottom=�6h   $��20��6�   $��1��6�   $��1��6�   $��8,0,0,60q�6�   �� 5%      -   5         ��Name$ TextBoxRecipe��5%      $	��200��6%   $��24��62   $	��Top=�6>   $
��Left=�6V   $��1��6q   $��1��6~   $��	60,40,0,0q�6�   �� 5&      -   5        $��ComboBoxAuthor��5&      $	��200��6%   $��22��61   $��1��6=   $��1��6J   $��Bottom=�6Z   $
��Left=�6u   $��	60,0,0,60q�6�   �� 5'      -   5         ��Name$ Find_Button��5'      $��
Поиск��6"   $��1��62   $��1��6?   $	��200��6O   $��24��6[   $��Bottom=�6g   $��60,114,0,10q�6�   $
��Left=�6�   6�   5)      5*      