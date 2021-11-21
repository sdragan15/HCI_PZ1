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
using Classes;

namespace HCI_PZ1
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();

            cmbDrzave.ItemsSource = Constants.Drzave;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                Telefon t1 = new Telefon();
                t1.RedniBroj = ++Constants.redniBrojTelefona;
                t1.Naziv = tbModel.Text;
                t1.Drzava = cmbDrzave.SelectedItem.ToString();
                t1.GodinaProizvodnje = dpDate.SelectedDate.Value;
                t1.Slika = AddPhoto.uriSlike;

                AddPhoto.uriSlike = "";

                MainWindow.listaTelefona.Add(t1);

                this.Close();
            }
        }


        public bool validate()
        {
            bool valid = true;

            if (tbModel.Text.Trim().Equals(""))
            {
                valid = false;
                errNaziv.Content = "Niste uneli naziv";
                errNaziv.Foreground = Brushes.Red;
            }
            else
            {
                errNaziv.Content = "";
            }

            if(cmbDrzave.SelectedItem == null)
            {
                valid = false;
                errLabel.Content = "Izaberite drzavu";
                errLabel.Foreground = Brushes.Red;
            }
            else
            {
                errLabel.Content = "";
            }

            if(dpDate.SelectedDate == null)
            {
                valid = false;
                dpDate.Foreground = Brushes.Red;
            }
            else
            {
                dpDate.Foreground = Brushes.Black;
            }

            if (AddPhoto.uriSlike.Equals(""))
            {
                valid = false;
                errSlika.Content = "Izaberite sliku";
                errSlika.Foreground = Brushes.Red;
            }
            else
            {
                errSlika.Content = "";
                errSlika.Foreground = Brushes.Gray;
            }

            return valid;
        }


        private void tbModel_GotFocus(object sender, RoutedEventArgs e)
        {
            errNaziv.Content = "";
        }

        private void tbModel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbModel.Text.Trim().Equals(""))
            {
                errNaziv.Foreground = Brushes.Gray;
                errNaziv.Content = "Unesite naziv telefona";
            }
        }


        private void cmbDrzave_GotFocus(object sender, RoutedEventArgs e)
        {
            errLabel.Content = "";
        }

        private void cmbDrzave_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbDrzave.SelectedItem == null)
            {
                errLabel.Foreground = Brushes.Gray;
                errLabel.Content = "Izaberi drzavu";
            }
        }

        private void dpDate_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            dpDate.Foreground = Brushes.Black;
        }

        private void dpDate_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if(dpDate.SelectedDate != null)
            {
                dpDate.Foreground = Brushes.Black;
            }
        }

        private void dpDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dpDate.SelectedDate != null)
            {
                dpDate.Foreground = Brushes.Black;
            }
        }


        private void btnSlika_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto addPhoto = new AddPhoto();
            addPhoto.ShowDialog();

            try
            {
                imgSlika.Source = new BitmapImage(new Uri(AddPhoto.uriSlike));
                errSlika.Content = "";
                errSlika.Foreground = Brushes.Gray;
            }
            catch(Exception ex)
            {
                errSlika.Content = "Izaberite sliku";
                errSlika.Foreground = Brushes.Red;
                imgSlika.Source = null;
                AddPhoto.uriSlike = "";
            }
            
        }






        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        
    }
}
