using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Usuario
    {
        public int PK_ID_USUARIO { get; set; }
        public string nombre_usuario { get; set; }
        public string contraseña { get; set; }

    }
}
