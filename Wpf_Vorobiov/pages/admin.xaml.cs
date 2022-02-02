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
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
            dgUsers.ItemsSource = BaseCon.BaseModel.auth.ToList();
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            auth SelectedUser = (auth)dgUsers.SelectedItem;//сохраняем выбранную строку datagrid в отдельный объект
            BaseCon.BaseModel.auth.Remove(SelectedUser);//удаляем эту строку из модели
            BaseCon.BaseModel.SaveChanges();//синхронизируем изменения с сервером
            MessageBox.Show("Выбранный пользователь удален");//обратная связь с оператором программы
            dgUsers.ItemsSource = BaseCon.BaseModel.auth.ToList();//обновить строки в datagrid
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            BaseCon.BaseModel.SaveChanges();
        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.GoBack();
        }
    }
}
