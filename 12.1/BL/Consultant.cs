using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using _12._2.DAO;
using _12._2.ViewControl;

namespace _12._2.BL
{
    public class Consultant : IGetClient, IChangeClient, ITransferer<Consultant>    {
        protected MainWindow mainWindow;
        protected ListView clientsListView;
        protected ListView accountsListView;
        protected TextBlock[] textBlocks = new TextBlock[5];
        protected TextBox[] textBoxes = new TextBox[5];
        protected Button findButton;
        protected Button saveChangesButton;
        protected Button changeDataButton;
        protected Button deleteClientButton;
        protected TextBlock infoTextBlock;
        protected bool isChange;

        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public Consultant(MainWindow mainWindow)
        {
            Id = 1;
            Name = "Random name";
            this.mainWindow = mainWindow;
            clientsListView = mainWindow.ClientsListView;
            accountsListView = mainWindow.AccountsListView;
            textBlocks[0] = mainWindow.SurnameTextBlock;
            textBlocks[1] = mainWindow.NameTextBlock;
            textBlocks[2] = mainWindow.PatronimicTextBlock;
            textBlocks[3] = mainWindow.PhoneNumberTextBlock;
            textBlocks[4] = mainWindow.SeriesAndNumberOfThePassportTextBlock;
            textBoxes[0] = mainWindow.SurnameTextBox;
            textBoxes[1] = mainWindow.NameTextBox;
            textBoxes[2] = mainWindow.PatronimicTextBox;
            textBoxes[3] = mainWindow.PhoneNumberTextBox;
            textBoxes[4] = mainWindow.SeriesAndNumberOfThePassportTextBox;
            findButton = mainWindow.FindButton;
            saveChangesButton = mainWindow.SaveChangesButton;
            changeDataButton = mainWindow.ChangeDataButton;
            deleteClientButton = mainWindow.DeleteClientButton;
            infoTextBlock = mainWindow.InfoTextBlock;
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Visibility = Visibility.Visible;
        }
        public void ShowAddClientForm()
        {
            isChange = false;
            mainWindow.MyStackPanel.Children.Clear();

            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);

            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }
        public virtual void ShowModifyDataForm()
        {
            isChange = true;
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);
            mainWindow.SurnameTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Surname;
            mainWindow.SurnameTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);
            mainWindow.NameTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Name;
            mainWindow.NameTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);
            mainWindow.PatronimicTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Patronimic;
            mainWindow.PatronimicTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);
            mainWindow.PhoneNumberTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).PhoneNumber;

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).SeriesAndNumberOfThePassport;
            mainWindow.SeriesAndNumberOfThePassportTextBox.IsEnabled = false;

            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }
        protected virtual void HideModifyDataForm()
        {
            mainWindow.SurnameTextBox.IsEnabled = true;
            mainWindow.NameTextBox.IsEnabled = true;
            mainWindow.PatronimicTextBox.IsEnabled = true;
            mainWindow.SeriesAndNumberOfThePassportTextBox.IsEnabled = true;
            mainWindow.SurnameTextBox.Text = string.Empty;
            mainWindow.NameTextBox.Text = string.Empty;
            mainWindow.PatronimicTextBox.Text = string.Empty;
            mainWindow.PhoneNumberTextBox.Text = string.Empty;
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text = string.Empty;
            mainWindow.MyStackPanel.Children.Clear();
        }

        public void ShowClientInfo()
        {
            HideModifyDataForm();
            mainWindow.MyStackPanel.Children.Clear();
            if (clientsListView.SelectedItem != null)
            {
                mainWindow.MyStackPanel.Children.Add(infoTextBlock);
                infoTextBlock.Text = GetClientInString((Client<Account>)clientsListView.SelectedItem);
                mainWindow.MyStackPanel.Children.Add(changeDataButton);
                mainWindow.MyStackPanel.Children.Add(deleteClientButton);
                mainWindow.OpenAnAccountButton.IsEnabled = true;
                accountsListView.IsEnabled = true;
                GetAllAccounts((Client<Account>)clientsListView.SelectedItem);
            }
        }
        public async void SaveChanges()
        {
            if (isChange)
            {
                ChangeClient((Client<Account>)clientsListView.SelectedItem);
            }
            else
            {
                await AddClientAsync();
            }
            GetAllClients();
        }
        protected virtual void ChooseActionsWithClient(Client<Account> client)
        {
            Console.Clear();
            Console.WriteLine(GetClientInString(client));
            Console.WriteLine("Выберте действие:\r\n" +
                "1. Изменить номер;\r\n" +
                "2. Выход\r\n");
            switch (Console.ReadKey(false).Key)
            {
                case ConsoleKey.D1:
                    ChangeClient(client);
                    break;
                default:
                    break;
            }

        }

        public string GetClientInString(Client<Account> client)
        {
            if (client == null)
            {
                return "Такого клиента не существует";
            }
            else
            {
                string result = string.Empty;
                result += $"ИД: {client.Id}\r\n";
                result += $"Фамилия: {client.Surname}\r\n";
                result += $"Имя: {client.Name}\r\n";
                result += $"Отчество: {client.Patronimic}\r\n";
                result += $"Номер телефона: {client.PhoneNumber}\r\n";
                if (client.SeriesAndNumberOfThePassport != string.Empty)
                {
                    result += "Серия и номер паспорта: **********\r\n";
                }
                else
                {
                    result += "Серия и номер паспорта: отсутствует\r\n";
                }
                result += $"Дата и время изменения записи: {client.ModificationTime}\r\n";
                result += $"Было изменено: {client.ModificatedData}\r\n";
                result += $"Тип изменений: {client.TypeOfModification}\r\n";
                result += $"Кто менял: {client.WhoChangeData}\r\n";
                return result;
            }
        }
        public async Task AddClientAsync()
        {
            string surname;
            string name;
            string patronimic;
            string phoneNumber;
            string seriesAndNumberOfThePassport;

            surname = mainWindow.SurnameTextBox.Text;
            name = mainWindow.NameTextBox.Text;
            patronimic = mainWindow.PatronimicTextBox.Text;
            phoneNumber = mainWindow.PhoneNumberTextBox.Text;
            seriesAndNumberOfThePassport = mainWindow.SeriesAndNumberOfThePassportTextBox.Text;
            await ClientsDataAccess.AddClientAsync(new Client<Account>(surname, name, patronimic, phoneNumber, seriesAndNumberOfThePassport, GetType().Name));
            HideModifyDataForm();
        }
        public virtual void ChangeClient(Client<Account> client)
        {
            client.PhoneNumber = mainWindow.PhoneNumberTextBox.Text;
            client.WhoChangeData = GetType().Name;
            client.ModificationTime = DateTime.Now;
            client.ModificatedData = "phone number";
            client.TypeOfModification = "modification";
            ClientsDataAccess.UpdateClient(client);
            HideModifyDataForm();
        }

        public virtual void EnableAccountHandleButtons()
        {

        }

        public virtual void TopUpAccount(Account account, double amount)
        {

        }

        public virtual void WithdrawAccount(Account account, double amount)
        {

        }
        public virtual void TransferAccount(Account senderAccount, Account recipientAccount, double amount)
        {

        }

        public void AddAccount(Account account)
        {
            ((Client<Account>)clientsListView.SelectedItem).Accounts.Add(account);
            ClientsDataAccess.UpdateClient((Client<Account>)clientsListView.SelectedItem);
            GetAllAccounts((Client<Account>)clientsListView.SelectedItem);
        }
        public virtual void CloseAccount(Account account)
        {

        }

        public void GetAllClients()
        {
            clientsListView.ItemsSource = ClientsDataAccess.GetAll();
        }

        protected void GetAllAccounts(Client<Account> client)
        {
            accountsListView.ItemsSource = ClientsDataAccess.GetAccountsOfClient(client);
        }
    }
}
