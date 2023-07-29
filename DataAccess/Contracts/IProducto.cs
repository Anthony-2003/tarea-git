using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    internal interface IProducto
    {
        DataTable buscar(string producto);
        void insertar(Producto producto);
        void actualizar(Producto producto);
        void borrar(int id);
        DataTable obtenerTabla();
        byte[] obtenerFotoProducto(int id);
        void actualizarCantidadProducto(int id, int restarCantidad);

    }
}
