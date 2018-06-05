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

using System.Text.RegularExpressions;

namespace LearnEnglish
{
    /// <summary>
    /// Логика взаимодействия для TranslatorPage.xaml
    /// </summary>
    public partial class TranslatorPage : Page
    {
        public event Action GoBack;
        public event Action AddWord;
        public event Action AddOffer;
        Int32 id;
        YandexLinguistics.NET.Lang lang1 = YandexLinguistics.NET.Lang.Ru;
        YandexLinguistics.NET.Lang lang2 = YandexLinguistics.NET.Lang.En;

        public TranslatorPage(Int32 id)
        {
            InitializeComponent();
            this.id = id;
        }

        public TranslatorPage(Int32 id, String word, YandexLinguistics.NET.Lang lang)
        {
            InitializeComponent();
            this.id = id;
            firstTB.Text = word;
            if (lang == YandexLinguistics.NET.Lang.En)
            {
                Button_Click_2(null, null);
            }
            else
                Button_Click_1(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lang1 == YandexLinguistics.NET.Lang.Ru)
                secondTB.Text = MyTranslator.TranslateToEn(firstTB.Text);
            else
                secondTB.Text = MyTranslator.TranslateToRus(firstTB.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            YandexLinguistics.NET.Lang lang = lang1;
            String labelStr = firstLabel.Content.ToString();

            lang1 = lang2;
            firstLabel.Content = secondLabel.Content;
            lang2 = lang;
            secondLabel.Content = labelStr;

            if(firstTB.Text != "")
                Button_Click_1(null, null);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button_Click_1(null, null);

            if(MyTranslator.GetLanguage(firstTB.Text) != lang1 || MyTranslator.GetLanguage(secondTB.Text) != lang2)
            { MessageBox.Show("Некорректное слово"); return; }

            Regex rx = new Regex(@"^\w+$");
            if (rx.IsMatch(firstTB.Text))
            {
                if (lang1 == YandexLinguistics.NET.Lang.Ru)
                    await MyDataBase.AddWord(id, secondTB.Text, firstTB.Text);
                else
                    await MyDataBase.AddWord(id, firstTB.Text, secondTB.Text);
                AddWord();
            }
            else
            {
                if (lang1 == YandexLinguistics.NET.Lang.Ru)
                    await MyDataBase.AddOffer(id, secondTB.Text, firstTB.Text);
                else
                    await MyDataBase.AddOffer(id, firstTB.Text, secondTB.Text);
                AddOffer();
            }
        }
    }
}
