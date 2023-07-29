using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MesaModel: Mesa
    {

        MesaImple mesaDao = new MesaImple();

        public List<Mesa> obtenerMesas()
        {
            return mesaDao.obtenerMesas();
        }

        public void cambiarEstadoMesa(int idMesa, string cambiarEstado)
        {
            mesaDao.cambiarEstadoMesa(idMesa, cambiarEstado);
        }

    }
}
