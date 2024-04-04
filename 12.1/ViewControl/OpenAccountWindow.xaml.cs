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
    /// Логика взаимодействия для OpenAccountWindow.xaml
    /// </summary>
    public partial class OpenAccountWindow : Window
    {
        readonly MainWindow mainWindow;
        Account? account;
        public OpenAccountWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void OpenWindow_Closed(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(RateTextBox.Text, out double interestRate);
            double.TryParse(AmountTextBox.Text, out double amount);

            if (CreditRadioButton.IsChecked == true)
            {
                account = new CreditAccount((Client<Account>)mainWindow.ClientsListView.SelectedItem, amount, interestRate);
            }
            else
            {
                if(CapitalisationCheckBox.IsChecked == true)
                {
                    account = new DepositAccountWithCapitalisation((Client<Account>)mainWindow.ClientsListView.SelectedItem, amount, interestRate);
                }
                else
                {
                    account = new DepositAccount((Client<Account>)mainWindow.ClientsListView.SelectedItem, amount, interestRate);
                }
            }
            mainWindow.employee.AddAccount(account);
            mainWindow.AccountsListView.ItemsSource = ((Client<Account>)mainWindow.ClientsListView.SelectedItem).Accounts;
            Close();
        }

        private void DepositRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Open_Button.IsEnabled = true;
            CapitalisationCheckBox.IsEnabled = true;
            RateTextBox.IsEnabled = true;
            AmountTextBox.IsEnabled = true;
            
        }

        private void CreditRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Open_Button.IsEnabled = true;
            CapitalisationCheckBox.IsChecked = false;
            CapitalisationCheckBox.IsEnabled = false;
            RateTextBox.IsEnabled = true;
            AmountTextBox.IsEnabled = true;
        }
        
        private void RateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckTextBoxInput(e);
        }

        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckTextBoxInput(e);
        }

        private void CheckTextBoxInput(TextCompositionEventArgs e)
        {
            if ((e.Text[0] < '0' || e.Text[0] > '9') && e.Text[0] != '.')
            {
                e.Handled = true;
            }
        }

    }
}
