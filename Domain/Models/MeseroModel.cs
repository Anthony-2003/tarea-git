using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MeseroModel
    {

        MeseroImple meseroDao = new MeseroImple();

        public string obtenerNombreMesero(int id)
        {
           return meseroDao.obtenerNombreMesero(id);
        }

    }
}
