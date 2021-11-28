using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        public static int redniBrojIzmenjenogTelefona;
        public static BindingList<Telefon> listaTelefona { get; set; }

        public MainWindow()
        {
            var values = typeof(Brushes).GetProperties().Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();
            var brushName = values.Select(v => v.Name);
            foreach(string name in brushName)
            {
                Constants.colors.Add(name);  
            }
            

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
            Telefon tel = (Telefon)dataGridPhones.SelectedItem;
            File.Delete(tel.Opis);
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
            
            
            Telefon tel = (Telefon)dataGridPhones.SelectedItem;
            redniBrojIzmenjenogTelefona = listaTelefona.IndexOf(tel);

            EditWindow editWindow = new EditWindow();
            editWindow.ShowDialog();

            dataGridPhones.Items.Refresh();
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
