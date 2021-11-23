using System;
using System.Collections.Generic;
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
using Classes;

namespace HCI_PZ1
{
    public partial class MainWindow : Window
    {
        public static DataIO serializer = new DataIO();
        public static Telefon viewTelefon;
        public static Telefon izmenjeniTelefon;
        public static BindingList<Telefon> listaTelefona { get; set; }

        public MainWindow()
        {

            listaTelefona = serializer.DeSerializeObject<BindingList<Telefon>>(Constants.savePath);
            if(listaTelefona == null)
            {
                listaTelefona = new BindingList<Telefon>();
            }

            DataContext = this;
            InitializeComponent();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void save(object sender, EventArgs e)
        {
            MainWindow.serializer.SerializeObject<BindingList<Telefon>>(MainWindow.listaTelefona, Constants.savePath);
        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            listaTelefona.Remove((Telefon)dataGridPhones.SelectedItem);
        }

        private void btnPogledaj_Click(object sender, RoutedEventArgs e)
        {
            PogledajWindow.pogledTelefon = (Telefon)dataGridPhones.SelectedItem;
            PogledajWindow pogledaj = new PogledajWindow();
            
            pogledaj.ShowDialog();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            int num0 = listaTelefona.Count();
            izmenjeniTelefon = (Telefon)dataGridPhones.SelectedItem;
            EditWindow editWindow = new EditWindow();
            editWindow.ShowDialog();

            int num1 = listaTelefona.Count();
            if(num0 < num1)
                listaTelefona.Remove((Telefon)dataGridPhones.SelectedItem);
        }







        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
