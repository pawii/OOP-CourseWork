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
    public partial class WordConstructorPage : Page
    {
        public event Action GoBack;
        Int32 wordIteration;
        Int32 charIteration;
        Int32[] wordIndexes;
        Boolean correctAnswer;
        Int32 id;
        List<DictionaryString> strings;
        List<TextBlock> tbs;
        List<Button> bts;

        public WordConstructorPage(Int32 id)
        {
            InitializeComponent();
            this.id = id;
            Initialize();
        }

        private async void Initialize()
        {
            wordIteration = -1;
            strings = await MyDataBase.GetCountWords(id, 5);
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
            wordIteration++;
            if (wordIteration == 5)
            { GoBack(); return; }

            tbs.Clear();
            bts.Clear();
            mainWord.Children.Clear();
            blocks.Children.Clear();
            charIteration = 0;
            correctAnswer = true;
            wordIndexes = GetRandomIndexes(strings[wordIteration].En.Length, strings[wordIteration].En.Length);

            //СОЗДАЕМ ТЕКСТОВЫЕ БЛОКИ
            for (var i = 0; i < strings[wordIteration].En.Length; i++)
            {
                tbs.Add(new TextBlock());
                tbs[i].SetResourceReference(StyleProperty, "charBlock");
                tbs[i].Margin = new Thickness(1);
                mainWord.Children.Add(tbs[i]);

                Button tb = new Button();
                tb.SetResourceReference(StyleProperty, "charButt");
                tb.Margin = new Thickness(5);
                tb.Content = strings[wordIteration].En[wordIndexes[i]];
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
            if (String.Equals(clickButt.Content, strings[wordIteration].En[charIteration]))
            {
                blocks.Children.Remove(clickButt);
                tbs[charIteration].Text = strings[wordIteration].En[charIteration].ToString();
                charIteration++;
                if (charIteration == strings[wordIteration].En.Length && correctAnswer)
                {
                    await MyDataBase.ChangeWordPriority(id, strings[wordIteration].En, -1);
                    foreach (TextBlock tb in tbs)
                        tb.Background = Brushes.Green;
                }
            }
            else
            { correctAnswer = false; clickButt.Background = Brushes.Red; }
        }

        private void ShowAnswer(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < strings[wordIteration].En.Length; i++)
                tbs[i].Text = strings[wordIteration].En[i].ToString();
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
    }
}
