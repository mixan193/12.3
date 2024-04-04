using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._2.BL
{
    internal interface IGetClient
    {
        string GetClientInString(Client<Account> client);
        void GetAllClients();
    }
}
