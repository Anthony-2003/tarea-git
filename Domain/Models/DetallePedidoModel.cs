using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DetallePedidoModel
    {

        DetallePedidoImple DetallePedidoImple = new DetallePedidoImple();

        public void insertarDetallePedido(DetallePedido detallePedido)
        {

            DetallePedidoImple.insertar(detallePedido);

        }

    }
}
