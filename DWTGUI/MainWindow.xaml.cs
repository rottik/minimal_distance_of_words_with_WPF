using System;
using System.IO;
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


namespace DWTGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> words = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btHledej_Click(object sender, RoutedEventArgs e)
        {
            wordList.Items.Clear();
            string slovo = textBox.Text.Trim().ToLower();
            if (slovo.Length == 0)
                return;
            string vysledek = "";
            int nbest = 10;
            var best = Distance.n_nearest_words(slovo, words, nbest);
            vysledek = best.OrderBy(p=>p.Value).First().Key;
            labelBestWord.Content = vysledek != "" ? vysledek : "\"nenalezeno\"";
            foreach (var v in best.OrderBy(p => p.Value))
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = v.Key+" "+v.Value.ToString();
                wordList.Items.Add(itm);
            }
        }

        private void btClearWord_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void btClearList_Click(object sender, RoutedEventArgs e)
        {
            wordList.Items.Clear();
        }

        private void btLoadWords_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            dlg.DefaultExt = ".txt";

            Nullable<bool> result = dlg.ShowDialog();
                        
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                words = File.ReadAllLines(filename,Encoding.GetEncoding(1250)).ToList();
            }
        }
    }
}
