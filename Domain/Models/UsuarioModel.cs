using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.DAO;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UsuarioModel:Usuario
    {


        public UsuarioModel() { }

        public UsuarioModel(int idUsuario, string nombreUsuario, string contrasena)
        {
            this.PK_ID_USUARIO = idUsuario;
            this.nombre_usuario = nombreUsuario;
            this.contraseña = contrasena;
        }

        //Métodos

        UsuarioImple usuarioDao = new UsuarioImple();

        public bool loginUser(string user, string pass, out int idUsuario)
        {
            return usuarioDao.login(user, pass, out idUsuario);
        }




    }
}
