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
using System.Windows.Shapes;

namespace LearnEnglish
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        Int32 id;
        DictionaryPage dp;
        OfferDictionaryPage odp;
        MainPage mp;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void SetCountWords()
        {
            countWordsLabel.Content = "Количество слов в словаре: " + (await MyDataBase.GetCountWords(id));
        }

        private async void SetCountOffers()
        {
            countOffersLabel.Content = "Количество предложений в словаре: " + (await MyDataBase.GetCountOffers(id));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            YandexLinguistics.NET.Lang lang = MyTranslator.GetLanguage(translateTB.Text);
            if (lang != YandexLinguistics.NET.Lang.En && lang != YandexLinguistics.NET.Lang.Ru)
            { MessageBox.Show("Язык не определен", "Язык не определен", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            TranslatorPage tp = new TranslatorPage(id, translateTB.Text, lang);
            tp.GoBack += OnGoBack;
            tp.AddWord += OnWordAdd;
            Pages.Content = tp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while (!MyDataBase.Create()) { }
            AuthorizationPage ap = new AuthorizationPage();
            ap.Enter += OnEnter;
            ap.OpenRegistrationPage += OnOpenRegistrationPage;
            Pages.Content = ap;
        }

        private void OnWordAdd()
        {
            SetCountWords();
            dp.Refresh();
        }

        private void OnOfferAdd()
        {
            SetCountOffers();
            odp.Refresh();
        }

        private void OnGoBack()
        {
            Pages.Content = mp;
        }

        private async void OnOpenTranslateWordPage()
        {
            if (await MyDataBase.GetCountWords(id) < 10)
            { MessageBox.Show("Слов должно больше 10!"); return; }
            TranslateWordPage twp = new TranslateWordPage(id);
            twp.GoBack += OnGoBack;
            Pages.Content = twp;
        }

        private async void OnOpenTranslateWordPage2()
        {
            if (await MyDataBase.GetCountWords(id) < 10)
            { MessageBox.Show("Слов должно больше 10!"); return; }
            TranslateWordPage2 twp = new TranslateWordPage2(id);
            twp.GoBack += OnGoBack;
            Pages.Content = twp;
        }

        private async void OnOpenWordConstructorPage()
        {
            if (await MyDataBase.GetCountWords(id) < 5)
            { MessageBox.Show("Слов должно больше 5!"); return; }
            WordConstructorPage wcp = new WordConstructorPage(id);
            wcp.GoBack += OnGoBack;
            Pages.Content = wcp;
        }

        private void OnOpenTranslatorPage()
        {
            TranslatorPage tp = new TranslatorPage(id);
            tp.GoBack += OnGoBack;
            tp.AddWord += OnWordAdd;
            tp.AddOffer += OnOfferAdd;
            Pages.Content = tp;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Pages.Content = dp;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AuthorizationPage ap = new AuthorizationPage();
            ap.Enter += OnEnter;
            ap.OpenRegistrationPage += OnOpenRegistrationPage;
            Pages.Content = ap;

            toolBar.Visibility = Visibility.Hidden;
        }

        private void OnEnter(Int32 id)
        {
            this.id = id;

            toolBar.Visibility = Visibility.Visible;
            SetCountWords();
            SetCountOffers();

            mp = new MainPage();
            mp.OpenTranslateWordPage += OnOpenTranslateWordPage;
            mp.OpenTranslateWordPage2 += OnOpenTranslateWordPage2;
            mp.OpenWordConstructorPage += OnOpenWordConstructorPage;
            mp.OpenTranslatorPage += OnOpenTranslatorPage;
            mp.OpenWordsToPriorityPage += OnOpenWordsToPriorityPage;
            mp.OpenOfferConstructorPage += OnOpenOfferConstructorPage;
            dp = new DictionaryPage(id);
            dp.GoBack += OnGoBack;
            odp = new OfferDictionaryPage(id);
            odp.GoBack += OnGoBack;

            Pages.Content = mp;
        }

        private void OnOpenRegistrationPage()
        {
            RegistrationPage rp = new RegistrationPage();
            rp.ToAuthorization += OnToAuthorization;
            Pages.Content = rp;
        }

        private void OnOpenWordsToPriorityPage()
        {
            WordsToPriorityPage wtpp = new WordsToPriorityPage(id);
            wtpp.GoBack += OnGoBack;
            wtpp.AddWord += OnWordAdd;
            Pages.Content = wtpp;
        }

        private async void OnOpenOfferConstructorPage()
        {
            if (await MyDataBase.GetCountOffers(id) < 5)
            { MessageBox.Show("Предложений должно больше 5!"); return; }

            OfferConstructorPage ocp = new OfferConstructorPage(id);
            ocp.GoBack += OnGoBack;
            Pages.Content = ocp;
        }

        private void OnToAuthorization()
        {
            AuthorizationPage ap = new AuthorizationPage();
            ap.Enter += OnEnter;
            ap.OpenRegistrationPage += OnOpenRegistrationPage;
            Pages.Content = ap;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Pages.Content = odp;
        }
    }
}