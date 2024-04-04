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
using _12._2.BL;
using _12._2.ViewControl;

namespace _12._2
{
    /// <summary>
    /// Логика взаимодействия для WithdrawWindow.xaml
    /// </summary>
    public partial class WithdrawWindow : Window
    {
        MainWindow mainWindow;
        public WithdrawWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void EnterAmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text[0] < '0' || e.Text[0] > '9') && e.Text[0] != '.')
            {
                e.Handled = true;
            }
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.employee.WithdrawAccount(mainWindow.AccountsListView.SelectedItem as Account, double.Parse(enterAmountTextBox.Text));
            Close();
        }
    }
}
