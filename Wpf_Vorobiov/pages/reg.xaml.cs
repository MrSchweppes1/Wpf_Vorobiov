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
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    public partial class reg : Page
    {
        int flag = 0;
        public reg(int c)
        {
            InitializeComponent();
            list1.ItemsSource = BaseCon.BaseModel.genders.ToList();
            list1.SelectedValuePath = "id";
            list1.DisplayMemberPath = "gender";

            string[] traits2 = new string[4];
            List<traits> traits1 = BaseCon.BaseModel.traits.ToList();
            int i = 0;
            foreach (traits tr in traits1)
            {
                traits2[i] = tr.trait;
                i++;
            }
            admin.Visibility = Visibility.Visible;
            flag = c;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.GoBack();
        }

        private void Zapbut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int adm = 2;
                if (admin.IsChecked == true)
                {
                    adm = 1;
                }
                auth LogPass = new auth() { login = Logtxt.Text, password = Passtxt.Password, role = adm };
                BaseCon.BaseModel.auth.Add(LogPass);
                BaseCon.BaseModel.SaveChanges();


                users NewUs = new users();
                NewUs.name = Nametxt.Text;
                NewUs.id = LogPass.id;
                NewUs.gender = (int)list1.SelectedValue;
                NewUs.dr = (DateTime)Datedr.SelectedDate;

                BaseCon.BaseModel.users.Add(NewUs);


                if (dobr.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = NewUs.id;
                    UTT.id_trait = 1;
                    BaseCon.BaseModel.users_to_traits.Add(UTT);
                }
                if (nezh.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = NewUs.id;
                    UTT.id_trait = 2;
                    BaseCon.BaseModel.users_to_traits.Add(UTT);

                }
                if (lask.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = NewUs.id;
                    UTT.id_trait = 3;
                    BaseCon.BaseModel.users_to_traits.Add(UTT);

                }
                BaseCon.BaseModel.SaveChanges();

                MessageBox.Show("Пользователь " + Logtxt.Text + ", успешно зарегистрирован");
                if (flag == 0)
                    load.MainFrame.GoBack();
                else
                    load.MainFrame.Navigate(new userList());
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
