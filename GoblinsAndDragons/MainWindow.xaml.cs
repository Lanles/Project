using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace GoblinsAndDragons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameStart gameStart = new GameStart();
            gameStart.ShowDialog(); // Otvori novi prozor koji zapocinje igru za podrazumevanim podacima
        }

        private void customCharButton_Click(object sender, RoutedEventArgs e)
        {
            Creator creator = new Creator();
            creator.ShowDialog(); // Otvori novi prozor u kome igrac namesta karakteristike igraca
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Zatvori igricu na klik dugmeta
        }
    }
}
