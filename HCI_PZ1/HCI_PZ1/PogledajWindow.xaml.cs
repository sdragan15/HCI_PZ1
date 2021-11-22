using Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HCI_PZ1
{
    /// <summary>
    /// Interaction logic for PogledajWindow.xaml
    /// </summary>
    public partial class PogledajWindow : Window
    {
        public static Telefon pogledTelefon;
        public PogledajWindow()
        {
            InitializeComponent();



            Telefon tel = pogledTelefon;

            
            lbNaslov.Text = tel.Naziv;
            lbDrzava.Text = "Drzava proizvodnje: " + tel.Drzava;
            lbDrzava.Text += "\nDatum proizvodnje: " + tel.GodinaProizvodnje.Day.ToString() + "/" + tel.GodinaProizvodnje.Month.ToString() + "/" + tel.GodinaProizvodnje.Year.ToString();
            imgSlika.Source = new BitmapImage(new Uri(tel.Slika));

            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            FileStream stream = new FileStream(tel.Opis, FileMode.Open);
            range.Load(stream, DataFormats.Rtf);
            stream.Close();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
