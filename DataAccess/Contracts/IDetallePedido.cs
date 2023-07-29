using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    internal interface IDetallePedido
    {

        void insertar(DetallePedido detallePedido);

    }
}
