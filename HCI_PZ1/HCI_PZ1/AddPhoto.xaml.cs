﻿using System;
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

namespace HCI_PZ1
{
    /// <summary>
    /// Interaction logic for AddPhoto.xaml
    /// </summary>
    public partial class AddPhoto : Window
    {
        public static bool valid = false;
        public static string uriSlike;

        public AddPhoto()
        {
            InitializeComponent();
        }

       

        private void tbSlika_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string slika = tbSlika.Text;
            uriSlike = slika;
            try
            {
                imgSlika.Source = new BitmapImage(new Uri(slika));
                valid = true;
            }
            catch(Exception error)
            {
                valid = false;
                Console.WriteLine(error.Message);
            }
            
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if(valid)
            {
                this.Close();
            }
            else
            {
                uriSlike = "";
            }
        }



        //Male fuje --------------------------------------------------------------------------------
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        
    }
}