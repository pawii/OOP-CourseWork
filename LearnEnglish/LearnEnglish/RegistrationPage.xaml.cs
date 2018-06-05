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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public event Action ToAuthorization;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void registrationBut_Click(object sender, RoutedEventArgs e)
        {
            loginTB.Background = Brushes.White;
            passwordTB.Background = Brushes.White;
            if (loginTB.Text == "")
            { loginTB.Background = Brushes.Red; return; }
            if (passwordTB.Text == "")
            { passwordTB.Background = Brushes.Red; return; }

            if (!await MyDataBase.CheckLogins(loginTB.Text))
            { MessageBox.Show("Такой логин уже существует"); return; }
            await MyDataBase.Registration(loginTB.Text, passwordTB.Text);
            MessageBox.Show("Регистрация прошла успешно");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToAuthorization();
        }
    }
}
