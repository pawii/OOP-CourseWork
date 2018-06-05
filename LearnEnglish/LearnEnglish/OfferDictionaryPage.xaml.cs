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
    /// Логика взаимодействия для OfferDictionaryPage.xaml
    /// </summary>
    public partial class OfferDictionaryPage : Page
    {
        private class Canv
        {
            private Int32 id;
            public StackPanel main_stp;
            public TextBlock enLabel;
            public TextBlock ruLabel;
            public Button butt;

            public Canv(Int32 id, DictionaryString str)
            {
                this.id = id;
                main_stp = new StackPanel();
                StackPanel horisontal_stp = new StackPanel();
                horisontal_stp.Orientation = Orientation.Horizontal;

                enLabel = new TextBlock();
                enLabel.Text = str.En;
                enLabel.FontSize = 30;
                enLabel.Width = 260;
                enLabel.Margin = new Thickness(0,0,10,0);
                enLabel.TextWrapping = TextWrapping.Wrap;

                ruLabel = new TextBlock();
                ruLabel.Text = str.Ru;
                ruLabel.FontSize = 30;
                ruLabel.Width = 260;
                ruLabel.TextWrapping = TextWrapping.Wrap;

                horisontal_stp.Children.Add(enLabel);
                horisontal_stp.Children.Add(ruLabel);

                butt = new Button();
                butt.Content = "Уже изучил";
                butt.Margin = new Thickness(0, 20, 0, 20);
                butt.Width = 150;
                butt.FontSize = 28;
                butt.Click += Button_Click;

                main_stp.Children.Add(horisontal_stp);
                main_stp.Children.Add(butt);
            }

            private async void Button_Click(object sender, RoutedEventArgs e)
            {
                await MyDataBase.ChangeOfferPriority(id, enLabel.Text.ToString(), -10);
                ((Button)sender).Background = Brushes.Green;
            }
        }

        private List<Canv> listCanvas;
        private Int32 id;
        public event Action GoBack;

        public OfferDictionaryPage()
        {
            listCanvas = new List<Canv>();
            InitializeComponent();
        }

        public OfferDictionaryPage(Int32 id) : this()
        {
            this.id = id;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void CreateString(DictionaryString str)
        {
            ListViewItem lvi = new ListViewItem();
            Canv canv = new Canv(id, str);

            listView.Items.Add(canv.main_stp);
            listCanvas.Add(canv);
        }

        public async void Refresh()
        {
            listView.Items.Clear();
            listCanvas.Clear();
            foreach (DictionaryString str in await MyDataBase.GetOffers(id))
            {
                CreateString(str);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }
    }
}
