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
    /// Логика взаимодействия для WordConstructorPage.xaml
    /// </summary>
    public partial class OfferConstructorPage : Page
    {
        public event Action GoBack;
        Int32 offerIteration;
        Int32 wordIteration;
        Int32[] offerIndexes;
        Boolean correctAnswer;
        Int32 id;
        String[] enWords;
        List<DictionaryString> offers;
        List<TextBlock> tbs;
        List<Button> bts;

        public OfferConstructorPage(Int32 id)
        {
            InitializeComponent();
            this.id = id;
            Initialize();
        }

        private async void Initialize()
        {
            offerIteration = -1;
            offers = await MyDataBase.GetCountOffers(id, 5);
            tbs = new List<TextBlock>();
            bts = new List<Button>();
            Next(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            offerIteration++;
            if (offerIteration == 5)
            { GoBack(); return; }

            tbs.Clear();
            bts.Clear();
            mainWord.Children.Clear();
            blocks.Children.Clear();
            wordIteration = 0;
            correctAnswer = true;
            enWords = SplitOffer(offers[offerIteration]);  //РАЗДЕЛИТЬ ПРЕДЛОЖЕНИЯ НА СЛОВА
            offerIndexes = GetRandomIndexes(enWords.Length, enWords.Length);

            //СОЗДАЕМ ТЕКСТОВЫЕ БЛОКИ
            for (var i = 0; i < enWords.Length; i++)
            {
                tbs.Add(new TextBlock());
                tbs[i].SetResourceReference(StyleProperty, "stringBlock");
                tbs[i].Visibility = Visibility.Hidden;
                mainWord.Children.Add(tbs[i]);

                Button tb = new Button();
                tb.SetResourceReference(StyleProperty, "stringButt");
                tb.Content = enWords[offerIndexes[i]];
                tb.Click += BlockButton_Click;
                bts.Add(tb);
                blocks.Children.Add(bts[i]);
            }


        }

        private async void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button bt in bts)
                bt.Background = Brushes.CadetBlue;

            Button clickButt = sender as Button;
            if (String.Equals(clickButt.Content, enWords[wordIteration]))
            {
                blocks.Children.Remove(clickButt);
                tbs[wordIteration].Text = enWords[wordIteration].ToString();
                tbs[wordIteration].Visibility = Visibility.Visible;
                wordIteration++;
                if (wordIteration == enWords.Length && correctAnswer)
                {
                    await MyDataBase.ChangeOfferPriority(id, offers[offerIteration].En, -1);
                    foreach (TextBlock tb in tbs)
                        tb.Background = Brushes.Green;
                }
            }
            else
            { correctAnswer = false; clickButt.Background = Brushes.Red; }
        }

        private void ShowAnswer(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < enWords.Length; i++)
            { tbs[i].Text = enWords[i].ToString(); tbs[i].Visibility = Visibility.Visible; }
            blocks.Children.Clear();
        }

        private Int32[] GetRandomIndexes(Int32 countIndexes, Int32 countValues)
        {
            Int32[] result = new Int32[countIndexes];
            Random rnd = new Random();
            Boolean flag;
            for (var i = 0; i < countIndexes; i++)
            {
                do
                {
                    flag = true;
                    result[i] = rnd.Next(0, countValues);
                    for (var j = 0; j < i; j++)
                    {
                        if (result[i] == result[j])
                            flag = false;
                    }
                } while (!flag);
            }

            return result;
        }

        private String[] SplitOffer(DictionaryString offer)
        {
            String[] enWords = offer.En.Split(' ');

            return enWords;
        }
    }
}
