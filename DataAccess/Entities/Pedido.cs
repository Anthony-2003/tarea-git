using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Pedido
    {
        public Pedido(int fK_ID_MESA, int fK_ID_MESERO, DateTime fecha, SqlMoney total)
        {
            FK_ID_MESA=fK_ID_MESA;
            FK_ID_MESERO=fK_ID_MESERO;
            this.fecha=fecha;
            this.total=total;
        }

        public int PK_ID_PEDIDO { get; set; }
        public int FK_ID_MESA { get; set; }
        public int FK_ID_MESERO { get; set; }
        public DateTime fecha { get; set; }
        public SqlMoney total { get; set; }

    }
}
