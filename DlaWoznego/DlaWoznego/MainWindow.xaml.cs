using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DlaWoznego
{
    public partial class MainWindow : Window
    {
        internal ObservableCollection<Produkt> ListaProduktow = new();
        private Produkt? NowyProdukt { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PrzygotujWiazanie();
        }
        private void PrzygotujWiazanie()
        {
            lstProdukty.ItemsSource = ListaProduktow;
            CollectionView widok = (CollectionView)CollectionViewSource.GetDefaultView(lstProdukty.ItemsSource);
            widok.SortDescriptions.Add(new SortDescription("Pokoj", ListSortDirection.Ascending));
            widok.SortDescriptions.Add(new SortDescription("Nazwa", ListSortDirection.Ascending));

            widok.Filter = FiltrUzytkownika;
        }
        private bool FiltrUzytkownika(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return (((Produkt)item).Nazwa.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstProdukty.ItemsSource).Refresh();
        }

        private void RemoveBtn2_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult odpowiedz = MessageBox.Show("Czy wykasować zaznaczone karteczki?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (odpowiedz == MessageBoxResult.Yes)
            {
                ObservableCollection<Produkt> ListaProduktowCopy = new(ListaProduktow);
                foreach (Produkt prod in lstProdukty.SelectedItems)
                {
                    ListaProduktowCopy.Remove(prod);
                }
                ListaProduktow.Clear();
                foreach (Produkt prod in ListaProduktowCopy)
                {
                    ListaProduktow.Add(prod);
                }
            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            string[] pokoje = { "Lobby", "Biuro 1", "Biuro 2", "Serwerownia 1", "Serwerownia 2", "Kuchnia", "Łazienka", "Sala Konferencyjna", "Magazyn", "Moja kanciapa" };
            bool same = false;
            for (int i = 0; pokoje.Length > i; i++)
            {
                if (pokoj_add.Text == pokoje[i])
                {
                    same = true;
                }
            }
            if (same)
            {
                NowyProdukt = new Produkt(pokoj_add.Text, nazwa_add.Text);
                ListaProduktow.Add(NowyProdukt);
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną nazwę pokoju!");
            }
        }

        private void BtnPotwierdz_Click(object sender, RoutedEventArgs e)
        {
            //if (czyNowyProdukt) { ListaProduktow.Add(nowyProdukt); }
            // this.DialogResult = true;
        }
    }
}
