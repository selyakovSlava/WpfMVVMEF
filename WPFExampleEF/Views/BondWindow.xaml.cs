using System.Windows;
using WPFExampleEF.Models;

namespace WPFExampleEF.Views
{
    /// <summary>
    /// Логика взаимодействия для BondWindow.xaml
    /// </summary>
    public partial class BondWindow : Window
    {
        public BondModel Bond { get; private set; }

        public BondWindow(BondModel bond)
        {
            InitializeComponent();

            Bond = bond;
            DataContext = Bond;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Bond.Update();
            DialogResult = true;
        }
    }
}
