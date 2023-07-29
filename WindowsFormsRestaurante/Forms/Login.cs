using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Windows.Forms;
using System.Data.SqlClient;
using Domain.ValueObjetcs;
using WindowsFormsRestaurante.Shared;

namespace WindowsFormsRestaurante
{
    public partial class Login : Form
    {

        //variables to make the screen draggable

      

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

   
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            FormUtils.Instance.MouseDown(sender, e, this);
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            FormUtils.Instance.MouseMove(sender, e, this);
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            FormUtils.Instance.MouseUp(sender, e);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {

            // Si el usuario existe, se cerrara esta pestaña y mostrara la pantalla de carga
            UsuarioModel userModel = new UsuarioModel();
            var isUserExist = userModel.loginUser(txtUsuario.Text, txtContraseña.Text, out int idUsuario);
            UserSession.IdUsuario = idUsuario;

            if (isUserExist)
            {

                this.Dispose();

            }

            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}



