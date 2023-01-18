using System;
using System.Globalization;
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
        //ObservableCollection is used to hold a list of "Produkt" objects - Karteczek :3
        internal ObservableCollection<Produkt> ListaProduktow = new();

        //This is a nullable object of the "Produkt" class, used to store a new product that will be added to the list
        //Your favourite way to make setters and getters XD
        private Produkt? NowyProdukt { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            PrzygotujWiazanie(); //method to prepare data binding
        }
        private void PrzygotujWiazanie()
        {
            lstProdukty.ItemsSource = ListaProduktow; //binding the listbox to the list of products

            CollectionView widok = (CollectionView)CollectionViewSource.GetDefaultView(lstProdukty.ItemsSource);

            //sort items in the list by the "Pokoj" property in ascending order
            widok.SortDescriptions.Add(new SortDescription("Pokoj", ListSortDirection.Ascending));
            //sort items in the list by the "Nazwa" property in ascending order
            widok.SortDescriptions.Add(new SortDescription("Nazwa", ListSortDirection.Ascending));

            widok.Filter = FiltrUzytkownika;//adding filter to the list

            this.lstProdukty.LostFocus += (s, e) => this.lstProdukty.SelectedItems.Clear();
        }
        private bool FiltrUzytkownika(object item) //Filter method to filter the list of products
        {
            if (String.IsNullOrEmpty(txtFilter.Text)) //if the filter textbox is empty
                return true; //return all items
            else //otherwise return only items whose "Nazwa" property contains the text in the filter textbox
                return (((Produkt)item).Nazwa.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        //Event handler for when the text in the filter textbox changes
        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e) 
        {
            CollectionViewSource.GetDefaultView(lstProdukty.ItemsSource).Refresh(); //refresh the list to apply the filter
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
            if (Int16.Parse(pokoj_add.Text) > 0 && Int16.Parse(pokoj_add.Text) < 11)
            {
                NowyProdukt = new Produkt(Int16.Parse(pokoj_add.Text), "", nazwa_add.Text);
                ListaProduktow.Add(NowyProdukt);
                pokoj_add.Text = "";
                nazwa_add.Text = "";
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną nazwę pokoju!");
            }
        }

        private void BtnUnselect_Click(object sender, RoutedEventArgs e)
        {
            lstProdukty.SelectedItems.Clear();

            pokoj_add.Text = "";
            nazwa_add.Text = "";
        }

        private void Label_KeyPress(object sender, KeyEventArgs e)
        {
            short x = 0;
            if (Int16.TryParse(pokoj_add.Text, out x))
            {
                if (x > 0 && x < 11)
                {
                    
                }
                else
                {
                    MessageBox.Show("Wprowadź poprawny numer pokoju!");
                    pokoj_add.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Numer pokoju musi być liczbą!");
                pokoj_add.Text = "";
            }
        }
    }
}
