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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();

            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbDrzave.ItemsSource = Constants.Drzave;
            cmbFontSize.ItemsSource = Constants.fontSizes;
            cmbColor.ItemsSource = Constants.colors;

            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            FileStream stream = new FileStream(MainWindow.izmenjeniTelefon.Opis, FileMode.Open);
            range.Load(stream, DataFormats.Rtf);
            stream.Close();

            cmbDrzave.SelectedItem = MainWindow.izmenjeniTelefon.Drzava;
            dpDate.SelectedDate = MainWindow.izmenjeniTelefon.GodinaProizvodnje;
            tbModel.Text = MainWindow.izmenjeniTelefon.Naziv;
            AddPhoto.uriSlike = MainWindow.izmenjeniTelefon.Slika;
            imgSlika.Source = new BitmapImage(new Uri(AddPhoto.uriSlike));
            errLabel.Content = "";
            errNaziv.Content = "";
            errSlika.Content = "";
            
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                Telefon t1 = new Telefon();
                if (MainWindow.listaTelefona.Count != 0)
                {
                    t1.RedniBroj = MainWindow.listaTelefona[0].RedniBroj + 1;
                }
                else
                {
                    t1.RedniBroj = 1;
                }

                t1.Naziv = tbModel.Text;
                t1.Drzava = cmbDrzave.SelectedItem.ToString();
                t1.GodinaProizvodnje = dpDate.SelectedDate.Value;
                t1.Slika = AddPhoto.uriSlike;
                t1.Opis = "rtbFile_" + t1.RedniBroj.ToString() + ".rtf";

                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                FileStream stream = new FileStream(t1.Opis, FileMode.Create);
                range.Save(stream, DataFormats.Rtf);
                stream.Close();

                AddPhoto.uriSlike = "";

                MainWindow.listaTelefona.Insert(0, t1);

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

            if (cmbDrzave.SelectedItem == null)
            {
                valid = false;
                errLabel.Content = "Izaberite drzavu";
                errLabel.Foreground = Brushes.Red;
            }
            else
            {
                errLabel.Content = "";
            }

            if (dpDate.SelectedDate == null)
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
            if (dpDate.SelectedDate != null)
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
            catch
            {
                errSlika.Content = "Izaberite sliku";
                errSlika.Foreground = Brushes.Red;
                imgSlika.Source = null;
                AddPhoto.uriSlike = "";
            }

        }


        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.SelectedItem = temp;

            temp = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            foreach (string s in Constants.colors)
            {
                if (temp.ToString().Equals(ColorConverter.ConvertFromString(s).ToString()))
                {
                    cmbColor.SelectedItem = s;

                    break;
                }
            }

        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null && !rtbEditor.Selection.IsEmpty)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null && !rtbEditor.Selection.IsEmpty)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);
            }
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbColor.SelectedItem != null && !rtbEditor.Selection.IsEmpty)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, cmbColor.SelectedItem);
            }
        }




        // Kratke fuje -------------------------------------------------------------------------------

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto.uriSlike = "";
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
