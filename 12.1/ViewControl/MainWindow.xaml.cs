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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _12._2.BL;
using _12._2.DAO;

namespace _12._2.ViewControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Consultant? employee;
        public static OpenAccountWindow? openAccountWindow;
        public TopUpWindow? topUpWindow;
        public WithdrawWindow? withdrawWindow;
        public TransferWindow? transferWindow;
        public MainWindow()
        {
            InitializeComponent();
            ClientsListView.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(ListView_OnColumnClick));
        }

        private void ConsultantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            employee = new Consultant(this);
            EnableUI();
        }

        private void ManagerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            employee = new Manager(this);
            EnableUI();
        }

        private void EnableUI()
        {
            ConsultantRadioButton.IsEnabled = false;
            ManagerRadioButton.IsEnabled = false;
            ClientsListView.IsEnabled = true;
            AddClientButton.IsEnabled = true;
            employee.GetAllClients();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            employee.SaveChanges();
        }

        private void ChangeData_Click(object sender, RoutedEventArgs e)
        {
            employee.ShowModifyDataForm();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            ClientsDataAccess.RemoveClient((Client<Account>)ClientsListView.SelectedItem);
            employee.GetAllClients();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            employee.ShowAddClientForm();
        }

        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee.ShowClientInfo();
        }

        private void ListView_OnColumnClick(object sender, RoutedEventArgs e)
        {
            //ClientsDataAccess.Sort((e.OriginalSource as GridViewColumnHeader).Content.ToString());
            //employee.GetAllClients();
        }

        private void OpenAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            openAccountWindow = new OpenAccountWindow(this);
            openAccountWindow.Show();
            IsEnabled = false;
        }

        private void CloseAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if(AccountsListView.SelectedItem != null)
            {
                employee.CloseAccount(AccountsListView.SelectedItem as Account);
            }
        }

        private void AccountsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee.EnableAccountHandleButtons();
            if (AccountsListView.SelectedItem != null)
            {
                historyTextBlock.Text = ((Account)AccountsListView.SelectedItem).History;
            }
        }

        private void TopUpButton_Click(object sender, RoutedEventArgs e)
        {
            topUpWindow = new TopUpWindow(this);
            topUpWindow.Show();
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            withdrawWindow = new WithdrawWindow(this);
            withdrawWindow.Show();
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            transferWindow = new TransferWindow(this);
            transferWindow.Show();
        }
    }
}
