using BurgerPlus.ApplicationData;
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
using BurgerPlus.MainPage;

namespace BurgerPlus.MainPage
{
    /// <summary>
    /// Логика взаимодействия для PageUsers.xaml
    /// </summary>
    public partial class PageUsers : Page
    {
       
        public PageUsers()
        {
            InitializeComponent();
            DtGridUsers.ItemsSource = BurgerPlusEntities1.GetContext().Users.ToList();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageUsersAdd((sender as Button).DataContext as Users));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageUsersAdd(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DtGridUsers.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующее {usersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BurgerPlusEntities1.GetContext().Users.RemoveRange(usersForRemoving);
                    BurgerPlusEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DtGridUsers.ItemsSource = BurgerPlusEntities1.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BurgerPlusEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DtGridUsers.ItemsSource = BurgerPlusEntities1.GetContext().Users.ToList();
            }
        }
    }
}

