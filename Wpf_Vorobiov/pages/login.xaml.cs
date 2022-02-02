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
using System.Windows.Threading;
using Wpf_Vorobiov.classes;

namespace Wpf_Vorobiov.pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        Random random = new Random();
        public login()
        {
            InitializeComponent();
        }

        auth CurrentUsers;
        string kode;
        bool flagKode = false;
        string hash;

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Hash.IsChecked == true)
                {
                    hash = Convert.ToString(Convert.ToBase64String(Encoding.UTF8.GetBytes(Passtxt.Password)));
                    CurrentUsers = BaseCon.BaseModel.auth.FirstOrDefault(x => x.login == Logtxt.Text && x.password == hash);
                }
                else
                {
                    CurrentUsers = BaseCon.BaseModel.auth.FirstOrDefault(x => x.login == Logtxt.Text && x.password == Passtxt.Password);
                }
                if (CurrentUsers != null)
                {//сюда напишем алгоритм перехода на страницу в зависимости от роли
                    if (flagKode == false)
                    {
                        generateKey();
                        flagKode = true;
                    }
                    else if (kode == txtKey.Text)
                    {
                        switch (CurrentUsers.role)
                        {

                            case 1:
                                MessageBox.Show("Вы вошли как администратор");
                                load.MainFrame.Navigate(new userList());
                                break;
                            case 2:
                            default:
                                MessageBox.Show("Вы вошли как обычный пользователь");
                                load.MainFrame.Navigate(new info(CurrentUsers));
                                break;
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Пользователь не зарегестрирован");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Возникла  ошибка" + exp.Message);
            }
        }

        private void generateKey()
        {
            imgRefresh.IsEnabled = false;
            kode = "";

            for (int i = 0; i < 8; i++)
            {
                kode += ((char)random.Next(33, 122)).ToString();
            }
            txtKey.Text = kode;
            MessageBox.Show(kode, "введите код в соответствующее поле на форме.", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            imgRefresh.IsEnabled = true;
            kode = ((char)random.Next(33, 122)).ToString();
            // MessageBox.Show("время вышло");
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            generateKey();
            flagKode = true;
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new reg(0));
        }

        private void RegBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnDLL_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new DLLPage());
        }

        private void btnGRAPH_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new GraphPage());
        }

        private void btnVideo_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.Navigate(new media());
        }
    }
}
