using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsRestaurante.Shared;

namespace WindowsFormsRestaurante.Forms
{
    public partial class Inicio : Form
    {
        int idMesero = UserSession.IdUsuario;
       private Timer timer = new Timer();


        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            actualizarHoraEnTiempoReal();
            establecerNombreUsuario();
        }

        private void actualizarHoraEnTiempoReal()
        {
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        private void establecerNombreUsuario()
        {
            MeseroModel meseroModel = new MeseroModel();
            lblNombreUsuario.Text = meseroModel.obtenerNombreMesero(idMesero);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt"); // Actualiza la hora cada segundo
            lblFecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy"); // Actualiza la fecha cada segundo
        }

    }
}

