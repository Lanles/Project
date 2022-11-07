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
using System.Text.RegularExpressions;

namespace GoblinsAndDragons
{
    /// <summary>
    /// Interaction logic for Creator.xaml
    /// </summary>
    public partial class Creator : Window
    {
        public Creator()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            HPEntered.MaxLength = 2;
            APEntered.MaxLength = 2;
            DPEntered.MaxLength = 2;
            DMEntered.MaxLength = 2; // Korisniku zabranimo da unosi bilo sta sem brojeva i maksimalno moze 2 broja
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(HPEntered.Text) || string.IsNullOrEmpty(APEntered.Text) 
                || string.IsNullOrEmpty(DPEntered.Text) || string.IsNullOrEmpty(DMEntered.Text))
            {
                MessageBox.Show("You have not filled out all of the fields! Fill them all out and try again.");
            } // Proveravamo da li su slicajno prazni neki prozori
            else 
            {
                GameStart gameStart = new GameStart();

                gameStart.hpH = Int32.Parse(HPEntered.Text);
                gameStart.apH = Int32.Parse(APEntered.Text);
                gameStart.dpH = Int32.Parse(DPEntered.Text);
                gameStart.dmH = Int32.Parse(DMEntered.Text);

                this.Close();
                gameStart.ShowDialog();
            } // Otvaramo prozor za GameStart, trenutni gasimo i prenosimo unete valute
        }

        private void closeGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zatvori prozor
        }
    }
}
