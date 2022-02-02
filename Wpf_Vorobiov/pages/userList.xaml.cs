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
using Wpf_Vorobiov.classes;

namespace Wpf_Vorobiov.pages
{
    /// <summary>
    /// Логика взаимодействия для userList.xaml
    /// </summary>
    public partial class userList : Page
    {
        List<users> users;
        public userList()
        {
            InitializeComponent();
            lbUsersList.ItemsSource = BaseCon.BaseModel.users.ToList();
            users = BaseCon.BaseModel.users.ToList();
            List<genders> genders = BaseCon.BaseModel.genders.ToList();
            Gen.ItemsSource = genders;
            Gen.SelectedValuePath = "id";
            Gen.DisplayMemberPath = "gender";
        }

        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int id = Convert.ToInt32(lb.Uid);
            lb.ItemsSource = BaseCon.BaseModel.users_to_traits.Where(x => x.id_user == id).ToList();
            lb.DisplayMemberPath = "traits.trait";
        }

        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth tUser = BaseCon.BaseModel.auth.FirstOrDefault(x => x.id == id);
            load.MainFrame.Navigate(new change(tUser));
        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth DellUs = BaseCon.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseCon.BaseModel.auth.Remove(DellUs);
            BaseCon.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь удален!");
            TimeSpan.FromSeconds(3);
            lbUsersList.ItemsSource = BaseCon.BaseModel.users.ToList();
        }

        private void createbtn_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new reg(1));

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new login());
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            List<users> lt = users.ToList();
            if (tbStart.Text != "" && tbFinish.Text != "")
            {
                if (Convert.ToInt32(tbStart.Text) >= 0)
                {
                    if (Convert.ToInt32(tbFinish.Text) < users.Count)
                    {
                        int start = Convert.ToInt32(tbStart.Text) - 1;
                        int finish = Convert.ToInt32(tbFinish.Text);
                        lt = users.Skip(start).Take(finish - start).ToList();
                    }
                    else
                    {
                        MessageBox.Show("Строка, до которой необходимо выбрать элементы, больше максимальной. \nДля корректной работы фильтра, поправьте данное значение.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                        int start = Convert.ToInt32(tbStart.Text) - 1;
                        int finish = Convert.ToInt32(tbFinish.Text);
                        lt = users.Skip(start).Take(finish - start).ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Строка, от которой необходимо реализовать выборку - отрицательная.\nДля корректной работы фильтра, поправьте данное значение.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    int start = Convert.ToInt32(tbStart.Text) - 1;
                    int finish = Convert.ToInt32(tbFinish.Text);
                    lt = users.Skip(start).Take(finish - start).ToList();
                }
            }
            if (Gen.SelectedItem != null)
                lt = lt.Where(x => x.gender == Convert.ToInt32(Gen.SelectedValue)).ToList();

            lbUsersList.ItemsSource = lt;
        }

        private void btnRset_Click(object sender, RoutedEventArgs e)
        {
            tbStart.Text = null;
            tbFinish.Text = null;
            Gen.SelectedItem = null;
            List<users> lt = users.ToList();
            lbUsersList.ItemsSource = lt;
        }

        private void tbStart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
