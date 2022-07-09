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


        private int updatingLekiID = 0;
        private void gridLeki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridLeki.SelectedIndex >= 0)
                {
                if (this.gridLeki.SelectedItems.Count >= 0)
                    {
                        if (this.gridLeki.SelectedItems[0].GetType() == typeof(Leki))
                            {

                                Leki d = (Leki)this.gridLeki.SelectedItems[0];
                                this.txtNazwa2.Text = d.Nazwa;
                                this.txtProducent2.Text = d.Producent;
                                this.txtRodzaj2.Text = d.Rodzaj;

                                this.updatingLekiID = d.Id;
                            }
                    }
                }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            AptekaEntities db = new AptekaEntities();

            var r = from d in db.Lekis
                    where d.Id == this.updatingLekiID
                    select d;

            Leki obj = r.SingleOrDefault();

            if(obj != null)
            {
                obj.Nazwa = this.txtNazwa2.Text;
                obj.Producent = this.txtProducent2.Text;
                obj.Rodzaj = this.txtRodzaj2.Text;
            }

            db.SaveChanges();
        }
    }
}
