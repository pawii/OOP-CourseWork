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
    /// Логика взаимодействия для WordsToPriorityPage.xaml
    /// </summary>
    public partial class WordsToPriorityPage : Page
    {
        public class Canv
        {
            private Int32 id;
            public Canvas canvas;
            public Label enLabel;
            public Label ruLabel;
            public Button butt;

            public Canv(Int32 id, DictionaryString str)
            {
                this.id = id;
                canvas = new Canvas();

                enLabel = new Label();
                enLabel.SetValue(ContentProperty, str.En);
                enLabel.FontSize = 30;

                ruLabel = new Label();
                ruLabel.FontSize = 30;
                ruLabel.SetValue(ContentProperty, str.Ru);

                butt = new Button();
                butt.Content = "Добавить";
                butt.Margin = new Thickness(0, 10, 0, 0);
                butt.Padding = new Thickness(5);
                butt.FontSize = 20;
                butt.Click += Button_Click;

                canvas.Children.Add(enLabel);
                canvas.Children.Add(ruLabel);
                canvas.Children.Add(butt);
                canvas.Height = 50;
                Canvas.SetLeft(ruLabel, 170);
                Canvas.SetLeft(butt, 380);
            }

            private async void Button_Click(object sender, RoutedEventArgs e)
            {
                await MyDataBase.AddWord(id, enLabel.Content.ToString(), ruLabel.Content.ToString());
            }
        }

        private List<Canv> listCanvas;
        private Int32 id;
        public event Action GoBack;
        public event Action AddWord;

        public WordsToPriorityPage()
        {
            InitializeComponent();
        }

        public WordsToPriorityPage(Int32 id) : this()
        {
            this.id = id;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            listCanvas = new List<Canv>();
            levelLabel.Content = "Ваш уровень: " + (await MyDataBase.GetLevel(id));
            Refresh();
        }

        private void CreateString(DictionaryString str)
        {
            ListViewItem lvi = new ListViewItem();
            Canv canv = new Canv(id, str);
            canv.butt.Click += Button1_Click;

            listView.Items.Add(canv.canvas);
            listCanvas.Add(canv);
        }

        public async void Refresh()
        {
            listView.Items.Clear();
            listCanvas.Clear();
            foreach (DictionaryString str in await MyDataBase.GetWordsLevel(id))
            {
                CreateString(str);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            AddWord();
        }
    }
}
