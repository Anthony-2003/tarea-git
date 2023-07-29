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
    public partial class ProductoEmergente : Form
    {
        public ProductoEmergente()
        {
            InitializeComponent();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProductoEmergente_Load(object sender, EventArgs e)
        {
            ProductoModel productoModel = new ProductoModel();
            dgvProductos.DataSource = productoModel.obtenerProductos();
        }


        

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgvProductos.SelectedRows[0].Cells[0].Value.ToString();
            string descripcion = dgvProductos.SelectedRows[0].Cells[1].Value.ToString();
            decimal precio = (decimal) dgvProductos.SelectedRows[0].Cells[2].Value;

            Pedidos pedidos = (Pedidos)this.Owner;
            pedidos.establecerProducto(id,descripcion, precio);
            this.Close();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
