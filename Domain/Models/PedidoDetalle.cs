using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PedidoDetalle
    {
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public SqlMoney precio { get; set; }
        public int cantidad { get; set; }
        public SqlMoney subtotal { get; set; }

        public PedidoDetalle(int idProducto, string descripcion, SqlMoney precio, int cantidad, SqlMoney subtotal)
        {
            this.idProducto=idProducto;
            this.descripcion=descripcion;
            this.precio=precio;
            this.cantidad=cantidad;
            this.subtotal=subtotal;
        }
    }
}
