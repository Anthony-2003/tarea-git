using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductoImple : Repository, IProducto
    {

     

        public void borrar(int id)
        {

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var commnad = new SqlCommand())
                {
                    commnad.Connection = connection;
                    commnad.CommandText = "DELETE FROM producto WHERE PK_ID_PRODUCTO = @id";
                    commnad.Parameters.AddWithValue("@id", id);
                    commnad.ExecuteNonQuery();
                }
            }
        }

        public DataTable obtenerTabla()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var commnad = new SqlCommand())
                {
                    commnad.Connection = connection;
                    commnad.CommandText = "select PK_ID_PRODUCTO as ID, descripcion as Descripción, precio as Precio, stock as Stock from producto";
                    SqlDataAdapter adapter = new SqlDataAdapter(commnad);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public void insertar(Producto producto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var commnad = new SqlCommand())
                {
                    commnad.Connection = connection;
                    commnad.CommandText = "INSERT INTO producto (descripcion, precio, stock, foto) VALUES (@descripcion, @precio, @stock, @foto)";
                    commnad.Parameters.AddWithValue("@descripcion", producto.descripcion);
                    commnad.Parameters.AddWithValue("@precio", producto.precio);
                    commnad.Parameters.AddWithValue("@stock", producto.stock);
                    commnad.Parameters.AddWithValue("@foto", producto.foto);
                    commnad.ExecuteNonQuery();
                }
            }
        }

        public byte[] obtenerFotoProducto(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT foto FROM producto WHERE PK_ID_PRODUCTO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (byte[])result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public DataTable buscar(string producto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT PK_ID_PRODUCTO AS ID, descripcion AS Descripción, precio AS Precio, stock AS Stock FROM producto WHERE descripcion LIKE '%' + @productName + '%'";
                    command.Parameters.AddWithValue("@productName", producto);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public void actualizar(Producto producto)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE producto SET descripcion = @descripcion, precio = @precio, stock = @stock, foto = @foto WHERE PK_ID_PRODUCTO = @id";
                    command.Parameters.AddWithValue("@id", producto.PK_ID_PRODUCTO);
                    command.Parameters.AddWithValue("@descripcion", producto.descripcion);
                    command.Parameters.AddWithValue("@precio", producto.precio);
                    command.Parameters.AddWithValue("@stock", producto.stock);
                    command.Parameters.AddWithValue("@foto", producto.foto);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void actualizarCantidadProducto(int id, int restarCantidad)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE producto SET stock = stock - @restarCantidad WHERE PK_ID_PRODUCTO = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@restarCantidad", restarCantidad);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
