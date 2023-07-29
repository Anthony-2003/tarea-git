using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace DataAccess.DAO
{
    public class UsuarioImple : Repository, IUsuario
    {
        public bool login(string user, string password, out int idUsuario)
        {
            idUsuario = 0;
            bool isUserExist;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText= "select * from usuario where nombre_usuario=@user and contraseña=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", password);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        isUserExist = true;
                        reader.Read();
                        idUsuario = reader.GetInt32(0);
                    }
                    else
                        isUserExist = false;
                }
            }
            return isUserExist;
        }

    }
}