using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DetallePedidoImple : Repository, IDetallePedido
    {
    

        public void insertar(DetallePedido detallePedido)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var commnad = new SqlCommand())
                {
                    commnad.Connection = connection;
                    commnad.CommandText = "INSERT INTO detalle_pedido (FK_ID_PEDIDO, FK_ID_PRODUCTO, cantidad_productos, precio_unitario, subtotal) OUTPUT INSERTED.PK_ID_DETALLE_PEDIDO VALUES (@FK_ID_PEDIDO, @FK_ID_PRODUCTO, @cantidad_productos, @precio_unitario, @subtotal)";
                    commnad.Parameters.AddWithValue("@FK_ID_PEDIDO", detallePedido.FK_ID_PEDIDO);
                    commnad.Parameters.AddWithValue("@FK_ID_PRODUCTO", detallePedido.FK_ID_PRODUCTO);
                    commnad.Parameters.AddWithValue("@cantidad_productos", detallePedido.cantidad_productos);
                    commnad.Parameters.AddWithValue("@precio_unitario", detallePedido.precio_unitario);
                    commnad.Parameters.AddWithValue("@subtotal", detallePedido.subtotal);
                    commnad.ExecuteNonQuery();

                }
            }
        }
    }
}
