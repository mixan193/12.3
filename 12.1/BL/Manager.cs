using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using _12._2.DAO;
using _12._2.ViewControl;

namespace _12._2.BL
{
    internal class Manager : Consultant
    {
        public Manager(MainWindow mainWindow) : base(mainWindow)
        {
            Id = 2;
            Name = "Random name";
        }


        protected override void ChooseActionsWithClient(Client<Account> client)
        {
            Console.Clear();
            Console.WriteLine(GetClientInString(client));
            Console.WriteLine("Выберте действие:\r\n" +
                "1. Изменить данные;\r\n" +
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

        public override void ShowModifyDataForm()
        {
            isChange = true;
            mainWindow.MyStackPanel.Children.Clear();
            mainWindow.MyStackPanel.Children.Add(textBlocks[0]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[0]);
            mainWindow.SurnameTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Surname;

            mainWindow.MyStackPanel.Children.Add(textBlocks[1]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[1]);
            mainWindow.NameTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Name;

            mainWindow.MyStackPanel.Children.Add(textBlocks[2]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[2]);
            mainWindow.PatronimicTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).Patronimic;

            mainWindow.MyStackPanel.Children.Add(textBlocks[3]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[3]);
            mainWindow.PhoneNumberTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).PhoneNumber;

            mainWindow.MyStackPanel.Children.Add(textBlocks[4]);
            mainWindow.MyStackPanel.Children.Add(textBoxes[4]);
            mainWindow.SeriesAndNumberOfThePassportTextBox.Text = ((Client<Account>)clientsListView.SelectedItem).SeriesAndNumberOfThePassport;

            mainWindow.MyStackPanel.Children.Add(saveChangesButton);
        }

        public override void EnableAccountHandleButtons()
        {
            if (accountsListView.SelectedItem == null)
            {
                mainWindow.CloseAnAccountButton.IsEnabled = false;
                mainWindow.TopUpButton.IsEnabled = false;
                mainWindow.WithdrawButton.IsEnabled = false;
                mainWindow.TransferButton.IsEnabled = false;
                return;
            }
            mainWindow.CloseAnAccountButton.IsEnabled = true;
            mainWindow.TopUpButton.IsEnabled = true;
            mainWindow.WithdrawButton.IsEnabled = true;
            mainWindow.TransferButton.IsEnabled = true;
        }

        public override void CloseAccount(Account account)
        {
            ((Client<Account>)clientsListView.SelectedItem).Accounts.Remove(account);
            account.IsOpened = false;
            ClientsDataAccess.UpdateAccount(account);
            GetAllAccounts((Client<Account>)mainWindow.ClientsListView.SelectedItem);
        }

        public override void TopUpAccount(Account account, double amount)
        {
            account.TopUp(amount);
            ClientsDataAccess.UpdateAccount(account);
            GetAllAccounts((Client<Account>)mainWindow.ClientsListView.SelectedItem);
            mainWindow.AccountsListView.SelectedItem = account;
        }

        public override void WithdrawAccount(Account account, double amount)
        {
            account.Withdraw(amount);
            ClientsDataAccess.UpdateAccount(account);
            GetAllAccounts((Client<Account>)mainWindow.ClientsListView.SelectedItem);
            mainWindow.AccountsListView.SelectedItem = account;
        }

        public override void TransferAccount(Account senderAccount, Account recipientAccount, double amount)
        {
            senderAccount.Withdraw(amount);
            recipientAccount.TopUp(amount);
            ClientsDataAccess.UpdateAccount(senderAccount);
            ClientsDataAccess.UpdateAccount(recipientAccount);
            GetAllAccounts((Client<Account>)mainWindow.ClientsListView.SelectedItem);
            mainWindow.AccountsListView.SelectedItem = senderAccount;
        }

        public override void ChangeClient(Client<Account> client)
        {
            client.Surname = mainWindow.SurnameTextBox.Text;
            client.Name = mainWindow.NameTextBox.Text;
            client.Patronimic = mainWindow.PatronimicTextBox.Text;
            client.PhoneNumber = Client<Account>.PhoneNumberUniformization(mainWindow.PhoneNumberTextBox.Text);
            client.SeriesAndNumberOfThePassport = mainWindow.SeriesAndNumberOfThePassportTextBox.Text;
            client.WhoChangeData = GetType().Name;
            client.ModificationTime = DateTime.Now;
            client.ModificatedData = "All data";
            client.TypeOfModification = "modification";
            ClientsDataAccess.UpdateClient((Client<Account>)clientsListView.SelectedItem);
            HideModifyDataForm();
        }
    }
}
