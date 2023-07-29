using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class DetallePedido
    {

        public int PK_ID_DETALLE_PEDIDO { get; set; }
        public int FK_ID_PEDIDO { get; set; }
        public int FK_ID_PRODUCTO { get; set; }
        public int cantidad_productos { get; set; }
        public SqlMoney precio_unitario { get; set; }  
        public SqlMoney subtotal { get; set; }

        public DetallePedido(int fK_ID_PEDIDO, int fK_ID_PRODUCTO, int cantidad_productos, SqlMoney precio_unitario, SqlMoney subtotal)
        {
            FK_ID_PEDIDO=fK_ID_PEDIDO;
            FK_ID_PRODUCTO=fK_ID_PRODUCTO;
            this.cantidad_productos=cantidad_productos;
            this.precio_unitario=precio_unitario;
            this.subtotal=subtotal;
        }
    }
}
