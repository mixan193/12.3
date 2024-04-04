using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using _12._2.BL;
using Microsoft.EntityFrameworkCore;

namespace _12._2.DAO
{
    class ClientsContext : DbContext
    {
        public DbSet<Client<Account>> Clients => Set<Client<Account>>();
        public DbSet<Account> Accounts => Set<Account>();

        private readonly string connectionProperties;
        public ClientsContext(string connectionPropertiesPath)
        {
            if(!File.Exists(connectionPropertiesPath))
            {
                Directory.CreateDirectory("properties");
                StreamWriter writer = new StreamWriter(connectionPropertiesPath);
                writer.WriteLine("server=localhost;\r\nuser=root;\r\npassword=root;\r\ndatabase=clients;");
                writer.Close();
            }
            StreamReader reader = new StreamReader(connectionPropertiesPath);
            connectionProperties = reader.ReadToEnd();
            try
            {
                Database.EnsureCreated();
            }
            catch 
            {
                
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionProperties);
        }
    }
}
