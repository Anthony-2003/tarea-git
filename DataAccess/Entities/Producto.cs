using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Producto
    {
        public int PK_ID_PRODUCTO { get; set; }
        public string descripcion { get; set; }
        public SqlMoney precio { get; set; }
        public int stock { get; set; }
        public byte[] foto { get; set; }

        public Producto() { }   

        public Producto(string descripcion, SqlMoney precio, int stock, byte[] foto)
        {
            this.descripcion=descripcion;
            this.precio=precio;
            this.stock=stock;
            this.foto = foto;
        }


        public Producto(int id, string descripcion, SqlMoney precio, int stock, byte[] foto)
        {
            this.PK_ID_PRODUCTO = id;
            this.descripcion=descripcion;
            this.precio=precio;
            this.stock=stock;
            this.foto = foto;
        }

    }
}
