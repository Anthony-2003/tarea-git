using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using System.Data.SqlTypes;
using System.Data;

namespace Domain.Models
{
    public class ProductoModel
    {

        ProductoImple productoDao = new ProductoImple();

        public void insertProduct(Producto producto)
        {
           productoDao.insertar(producto);
        }

        public DataTable obtenerProductos()
        {
            return productoDao.obtenerTabla();
        }

        public byte[] obtenerFotoProducto(int id)
        {
            return productoDao.obtenerFotoProducto(id);
        }

        public void updateProduct(Producto producto)
        {
            productoDao.actualizar(producto);
        }

        public void deleteProduct(int id)
        {
            productoDao.borrar(id);
        }

        public DataTable buscarProducto(string descripcion)
        {
            return productoDao.buscar(descripcion);
        }

        public void actualizarCantidadProducto(int id, int restarCantidad)
        {
            productoDao.actualizarCantidadProducto(id, restarCantidad);
        }
 
    }
}
