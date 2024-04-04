using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._2.BL
{
    interface ITransferer<in T>
    {
        void TransferAccount(Account senderAccount, Account recipientAccount, double amount);

    }
}
