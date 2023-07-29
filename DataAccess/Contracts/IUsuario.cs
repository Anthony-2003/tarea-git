using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    internal interface IUsuario
    {

    
        bool login(string user, string password, out int idUsuario);

    }
}
