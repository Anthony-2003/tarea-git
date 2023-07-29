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
    public class PedidoImple : Repository, IPedido
    {
        public void insertar(Pedido pedido, out int idPedido)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var commnad = new SqlCommand())
                {
                    commnad.Connection = connection;
                    commnad.CommandText = "INSERT INTO pedido (FK_ID_MESA, FK_ID_MESERO, fecha, total) VALUES (@FK_ID_MESA, @FK_ID_MESERO, @fecha, @total); SELECT SCOPE_IDENTITY()";
                    commnad.Parameters.AddWithValue("@FK_ID_MESA", pedido.FK_ID_MESA);
                    commnad.Parameters.AddWithValue("@FK_ID_MESERO", pedido.FK_ID_MESERO);
                    commnad.Parameters.AddWithValue("@fecha", pedido.fecha);
                    commnad.Parameters.AddWithValue("@total", pedido.total);
                    object result = commnad.ExecuteScalar();
                    idPedido = Convert.ToInt32(Convert.ToDecimal(result));

                }
            }
        }
    }
}
