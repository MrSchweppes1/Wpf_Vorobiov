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
    /// Логика взаимодействия для info.xaml
    /// </summary>
    public partial class info : Page
    {
        public info(auth CurrentUsers)
        {

            InitializeComponent();
            try
            {
                Nametxt.Text = CurrentUsers.users.name;
                drtxt.Text = CurrentUsers.users.dr.ToString("yyyy MM dd");
                genderTxt.Text = CurrentUsers.users.genders.gender;
                List<users_to_traits> LUTT = BaseCon.BaseModel.users_to_traits.Where(x => x.id_user == CurrentUsers.id).ToList();
                foreach (users_to_traits UT in LUTT)
                {
                    DopInfotxt.Text += UT.traits.trait + " ";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Информация в системе не найдена: " + e.Message);
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            load.MainFrame.GoBack();
        }
    }
}
