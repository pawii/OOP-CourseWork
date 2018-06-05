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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public event Action OpenTranslateWordPage;
        public event Action OpenTranslateWordPage2;
        public event Action OpenWordConstructorPage;
        public event Action OpenTranslatorPage;
        public event Action OpenWordsToPriorityPage;
        public event Action OpenOfferConstructorPage;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenTranslateWordPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenTranslateWordPage2();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenWordConstructorPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenTranslatorPage();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenWordsToPriorityPage();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            OpenOfferConstructorPage();
        }
    }
}
