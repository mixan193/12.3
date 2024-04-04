using _12._2.BL;
using _12._2.DAO;
using MySqlX.XDevAPI;
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

namespace _12._2.ViewControl
{
    /// <summary>
    /// Логика взаимодействия для TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        MainWindow mainWindow;
        public TransferWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            clientsListView.ItemsSource = ClientsDataAccess.GetAll();
        }

        private void EnterAmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text[0] < '0' || e.Text[0] > '9') && e.Text[0] != '.')
            {
                e.Handled = true;
            }
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.employee.TransferAccount(mainWindow.AccountsListView.SelectedItem as Account,
                accountsListView.SelectedItem as Account,
                double.Parse(enterAmountTextBox.Text));
            Close();
        }

        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clientsListView.SelectedItem != null)
            {
                accountsListView.IsEnabled = true;
                accountsListView.ItemsSource = ClientsDataAccess.GetAccountsOfClient(clientsListView.SelectedItem as Client<Account>);
            }
            else
            {
                accountsListView.IsEnabled = false;
                accountsListView.ItemsSource = null;
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientsListView.ItemsSource = ClientsDataAccess.GetClientsBuParam(searchTextBox.Text);

        }

        private void AccountsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enterAmountTextBox.Text = "";
            _ = accountsListView.SelectedItem is null ? enterAmountTextBox.IsEnabled = false : enterAmountTextBox.IsEnabled = true;
        }

        private void enterAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (enterAmountTextBox.Text == "")
            {
                transferButton.IsEnabled = false;
            }
            else
            {
                transferButton.IsEnabled = true;
            }
        }
    }
}
