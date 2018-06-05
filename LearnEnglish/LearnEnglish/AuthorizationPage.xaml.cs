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

namespace LearnEnglish
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public event Action<Int32> Enter;
        public event Action OpenRegistrationPage;

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void enterBut_Click(object sender, RoutedEventArgs e)
        {
            loginTB.Background = Brushes.White;
            passwordTB.Background = Brushes.White;
            if (loginTB.Text == "")
            { loginTB.Background = Brushes.Red; return; }
            if (passwordTB.Password == "")
            { passwordTB.Background = Brushes.Red; return; }

            Int32 id = await MyDataBase.GetId(loginTB.Text);
            if (id < 1)
            { MessageBox.Show("Неправильный логин"); return; }
            String password = await MyDataBase.GetPassword(id);
            if (!String.Equals(password, passwordTB.Password))
            { MessageBox.Show("Неправильный пароль"); return; }

            Enter(id);
        }

        private void registrationBut_Click(object sender, RoutedEventArgs e)
        {
            OpenRegistrationPage();
        }
    }
}
