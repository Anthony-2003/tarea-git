using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Domain.Models
{
    public class PedidoModel
    {

        PedidoImple pedidoDao = new PedidoImple();

        public void insertarPedido(Pedido pedido, out int idPedido)
        {
            pedidoDao.insertar(pedido, out idPedido);
        }

    }
}

