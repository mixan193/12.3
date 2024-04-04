using _12._2.BL;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12._2.DAO
{
    public static class ClientsDataAccess
    {
        public static string connectionPropertiesPath = "properties/ClientsDBConnection.properties";

        public static async Task<int> AddClientAsync(Client<Account> client)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                await context.AddAsync(client);
                return await context.SaveChangesAsync();
            }
        }

        public static async Task<int> RemoveClient(Client<Account> client)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                context.Remove(client);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Возвращает экземпляр класса Client по имени, фамилии и отчеству
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronimic"></param>
        /// <returns></returns>
        public static Client<Account>? GetClient(string surname, string name, string patronimic)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                foreach (Client<Account> client in context.Clients)
                {
                    if (client.Surname == surname)
                    {
                        if (client.Name == name)
                        {
                            if (client.Patronimic == patronimic)
                            {
                                return client;
                            }
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Возвращает экземпляр класса Client по номеру телефона
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Client<Account>> GetClientsBuParam(string param)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                List<Client<Account>> result = [];
                foreach (Client<Account> client in context.Clients)
                {
                    if (client.Surname.Contains(param) ||
                        client.Name.Contains(param) ||
                        client.Patronimic.Contains(param)||
                        client.PhoneNumber.Contains(param) ||
                        client.SeriesAndNumberOfThePassport.Contains(param))
                    {
                        result.Add(client);
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// Возвращает экземпляр класса Client по номеру ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Client<Account>?> GetClient(int id)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                return await context.Clients.FindAsync(id);
            }
        }


        public static async void UpdateClient(Client<Account> client)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                context.Clients.Update(client);
                await context.SaveChangesAsync();
            }
        }

        public static List<Client<Account>> GetAll()
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                return [.. context.Clients];
            }

        }

        public static List<Account> GetAccountsOfClient(Client<Account> client)
        {
            using (ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                MySqlParameter id = new MySqlParameter("@Id", client.Id);
                return (List<Account>)(client.Accounts = context.Accounts.FromSqlRaw("SELECT * FROM accounts WHERE OwnerId = @Id AND IsOpened = 1", id).ToList());
            }
        }

        public static async void UpdateAccount(Account account)
        {
            using(ClientsContext context = new ClientsContext(connectionPropertiesPath))
            {
                context.Accounts.Update(account);
                await context.SaveChangesAsync();
            }
        }
    }
}
