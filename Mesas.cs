using DataAccess.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsRestaurante.Forms
{
    public partial class Mesas : Form
    {



        public Mesas()
        {
            InitializeComponent();

        }

        private void Tables_Load(object sender, EventArgs e)
        {

            obtenerInfoMesas();
            pasarNumDeMesaAPedidos();
            cambiarColorPanelOcupado();

        }

        private void MesaSeleccionada(string nMesa)
        {
            Pedidos pedidos = new Pedidos(nMesa);
            MenuPrincipal mainMenu = (MenuPrincipal)this.ParentForm;
            mainMenu.abrirVentana(pedidos);
        }

        private void pasarNumDeMesaAPedidos()
        {
            foreach (Control control in this.Controls)
            {

                if (control is Panel)
                {
                    string numMesa = "";

                    foreach (Control innerControl in control.Controls)
                    {
                        if (innerControl is Label && innerControl.Name.StartsWith("lblNumTable"))
                        {
                            numMesa = innerControl.Text;
                            break;
                        }
                    }

                    control.Click += (s, Args) =>
                    {
                        MesaSeleccionada(numMesa);

                    };
                }
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void table1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblTable1Amount_Click(object sender, EventArgs e)
        {

        }

        private void lblNumTable1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        public void obtenerInfoMesas()
        {
            MesaModel mesaModel = new MesaModel();
            List<Mesa> listaMesas = mesaModel.obtenerMesas();

            // Iterar sobre la lista de mesas y actualizar los labels
            for (int i = 0; i < listaMesas.Count; i++)
            {
                // Obtener los datos de la mesa actual
                int idMesa = listaMesas[i].PK_ID_MESA;
                int capacidad = listaMesas[i].capacidad;
                string estado = listaMesas[i].estado;

                string labelNumTable = "lblNumTable" + (i+1).ToString();
                string labelMesaCantidad = "lblMesa" + (i+1) + "Cantidad".ToString();
                string labelEstadoMesa = "lblEstadoMesa" + (i + 1).ToString();

                System.Windows.Forms.Label lblNumTable = this.Controls.Find(labelNumTable, true).FirstOrDefault() as System.Windows.Forms.Label;
                System.Windows.Forms.Label lblMesaCantidad = this.Controls.Find(labelMesaCantidad, true).FirstOrDefault() as System.Windows.Forms.Label;
                System.Windows.Forms.Label lblEstadoMesa = this.Controls.Find(labelEstadoMesa, true).FirstOrDefault() as System.Windows.Forms.Label;

                if (lblNumTable != null)
                {
                    lblNumTable.Text = idMesa.ToString();
                    lblMesaCantidad.Text = capacidad.ToString();
                    lblEstadoMesa.Text = estado.ToString();
                }

            }
        }



        private void lblEstadoMesa1_Click(object sender, EventArgs e)
        {

        }

        private void table9_MouseHover_1(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BackColor = Color.FromArgb(148, 117, 71);
        }

        private void table5_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BackColor = Color.FromArgb(209, 166, 100);
        }

        public void cambiarColorPanelOcupado()
        {
            for (int i = 1; i <= 9; i++)
            {
                // Obtener los controles correspondientes a la mesa actual
                Panel panelMesa = this.Controls["table" + i] as Panel;
                Label lblEstadoMesa = panelMesa.Controls["lblEstadoMesa" + i] as Label;
                PictureBox fotoMesa = panelMesa.Controls["pictureBox" + i] as PictureBox;

                // Verificar si el estado de la mesa es "ocupada"
                if (lblEstadoMesa.Text == "ocupada")
                {
                    fotoMesa.Image = Image.FromFile(@"C:\Users\ANTHO-PC\source\repos\SolutionRestaurante\WindowsFormsRestaurante\Resources\Img\mesaOcupada.png");

                    // Cambiar el color de fondo del panel
                    panelMesa.BackColor = Color.FromArgb(148, 117, 71);

                    panelMesa.MouseHover -= new EventHandler(table9_MouseHover_1);
                    panelMesa.MouseLeave -= new EventHandler(table5_MouseLeave);

                }
            }
        }

        private void table1_Click(object sender, EventArgs e)
        {

            Panel panel = (Panel)sender;

            Label nMesa = panel.Controls
            .OfType<Label>()
            .Where(l => l.Name.StartsWith("lblNumTable"))
            .FirstOrDefault();

            Label estado = panel.Controls
            .OfType<Label>()
            .Where(l => l.Name.StartsWith("lblEstadoMesa"))
            .FirstOrDefault();

            MesaModel mesaModel = new MesaModel();
            mesaModel.cambiarEstadoMesa(int.Parse(nMesa.Text), "ocupada");

        }
    }

}
