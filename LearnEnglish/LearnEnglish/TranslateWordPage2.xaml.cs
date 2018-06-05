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

using System.Windows.Forms;

namespace LearnEnglish
{
    /// <summary>
    /// Логика взаимодействия для TranslateWordPage2.xaml
    /// </summary>
    public partial class TranslateWordPage2 : Page
    {
        public event Action GoBack;
        Int32 id;
        Int32 iteration;
        decimal time = 5;
        Timer timer;
        List<DictionaryString> strings;
        System.Windows.Controls.Button[] butts = new System.Windows.Controls.Button[4];
        Int32[] buttIndexes;

        public TranslateWordPage2(Int32 id)
        {
            InitializeComponent();
            this.id = id;
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            Initialize();
        }

        private async void Initialize()
        {
            strings = await MyDataBase.GetCountWords(id, 10);
            iteration = -1;
            timeLabel.Content = time;

            // ТУТ КНОПКИ СОЗДАЕМ
            for (var i = 0; i < 4; i++)
            {
                butts[i] = new System.Windows.Controls.Button();
                butts[i].FontSize = 30;
                butts[i].Padding = new Thickness(5);
                butts[i].Margin = new Thickness(0, 0, 0, 10);
                butts[i].Background = Brushes.AliceBlue;
                buttsPanel.Children.Add(butts[i]);
            }
            var nextButt = new System.Windows.Controls.Button();
            nextButt.Content = "Далее";
            nextButt.FontSize = 30;
            nextButt.Padding = new Thickness(5);
            nextButt.Margin = new Thickness(0, 30, 0, 10);
            nextButt.Background = Brushes.AliceBlue;
            nextButt.Click += Next;
            buttsPanel.Children.Add(nextButt);
            // ЧТОБЫ МЕНЬШЕ ТЕКСТА, ПОТОМ СОЗДАТЬ СТИЛЬ

            Next(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
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

        private void Next(object sender, RoutedEventArgs e)
        {
            iteration++;
            if (iteration == 10)
            { GoBack(); return; }

            RefreshButts();
            time = 5;
            timer.Start();
            errorTimeLabel.Visibility = Visibility.Hidden;
            SetSignButts(true);
            mainWord.Text = strings[iteration].En;
            buttIndexes = GetRandomIndexes(4, 4);
            Int32[] wordIndexes;

            Boolean flag;
            do
            {
                flag = true;
                wordIndexes = GetRandomIndexes(3, 10);
                for (var i = 0; i < 3; i++)
                    if (String.Equals(strings[iteration].Ru, strings[wordIndexes[i]].Ru))
                        flag = false;
            } while (!flag);




            butts[buttIndexes[0]].Content = strings[iteration].Ru;
            for (Int32 i = 0, j = 1; i < 3; i++, j++)
            {
                butts[buttIndexes[j]].Content = strings[wordIndexes[i]].Ru;
            }
        }

        private void timer_Tick(Object sender, EventArgs args)
        {
            time -= (Decimal)0.1;
            Math.Round(time, 1);
            timeLabel.Content = time.ToString();
            if (time == 0)
            {
                timer.Stop();
                SetSignButts(false);
                errorTimeLabel.Visibility = Visibility.Visible;
                butts[buttIndexes[0]].Background = Brushes.Green;
            }
        }

        private void SetSignButts(Boolean state)
        {
            if (state)
                for (var i = 0; i < 4; i++)
                    butts[i].Click += butts_Click;
            else
                for (var i = 0; i < 4; i++)
                    butts[i].Click -= butts_Click;
        }

        private async void butts_Click(Object sender, EventArgs args)
        {
            System.Windows.Controls.Button currentButt = (System.Windows.Controls.Button)sender;
            if (String.Equals(currentButt.Content, butts[buttIndexes[0]].Content))
            {
                butts[buttIndexes[0]].Background = Brushes.Green;
                await MyDataBase.ChangeWordPriority(id, mainWord.Text, -2);
            }
            else
            {
                butts[buttIndexes[0]].Background = Brushes.Green;
                currentButt.Background = Brushes.Red;
            }
            timer.Stop();
        }

        private void RefreshButts()
        {
            for (var i = 0; i < 4; i++)
                butts[i].Background = Brushes.AliceBlue;
        }
    }
}
