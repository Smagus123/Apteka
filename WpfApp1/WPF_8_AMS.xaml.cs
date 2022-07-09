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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy WPF_8_AMS.xaml
    /// </summary>
    public partial class WPF_8_AMS : Window
    {
        public WPF_8_AMS()
        {
            InitializeComponent();

            AptekaEntities db = new AptekaEntities();
            var aptk = from d in db.Lekis
                       select new
                       {

                           LekiNazwa = d.Nazwa,
                           ProducentNazwa = d.Producent
                       };

            foreach (var item in aptk)
            {
                Console.WriteLine(item.LekiNazwa);
                Console.WriteLine(item.ProducentNazwa);
            }

            this.gridLeki.ItemsSource = aptk.ToList();
                
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            AptekaEntities db = new AptekaEntities();

            Leki lekiObject = new Leki()
            {
                
                Nazwa = txtNazwa.Text,
                Producent = txtProducent.Text,
                Rodzaj = txtRodzaj.Text
            };

            db.Lekis.Add(lekiObject);
            db.SaveChanges();

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            AptekaEntities db = new AptekaEntities();


            this.gridLeki.ItemsSource = db.Lekis.ToList();
        }

        private void gridLeki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridLeki.SelectedItems.Count >= 0)
            {
                if (this.gridLeki.SelectedItems[0].GetType() == typeof(Leki))
                {

                    Leki d = (Leki)this.gridLeki.SelectedItems;
                    this.txtNazwa2.Text = d.Nazwa;
                    this.txtProducent2.Text = d.Producent;
                    this.txtRodzaj2.Text = d.Rodzaj;
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AptekaEntities db = new AptekaEntities();

            var r = from d in db.Lekis
                    where d.Id == 1
                    select d;
            foreach (var item in r)
            {
                MessageBox.Show(item.Nazwa);
                item.Nazwa = "Apap 2 - Zaktualizowane dane";
            }

            db.SaveChanges();
        }
    }
}
