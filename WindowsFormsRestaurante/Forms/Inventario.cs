using DataAccess.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsRestaurante.Forms
{

    public partial class Inventario : Form
    {

        private bool agregarProductoCerrado = true;
        public AgregarProducto agregarProductoForm;
        ProductoModel productoModel = new ProductoModel();
        public Inventario()
        {
            InitializeComponent();
            showAllProductos();

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void Inventario_Load(object sender, EventArgs e)
        {

        }

        public void refrescarDataGridView()
        {
            dgvProductos.DataSource = productoModel.obtenerProductos();
        }

        private void showAllProductos()
        {
            dgvProductos.DataSource = productoModel.obtenerProductos();
            dgvProductos.RowHeadersVisible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (agregarProductoCerrado)
            {
                agregarProductoForm = new AgregarProducto(this);
                agregarProductoForm.FormClosed += AgregarProducto_FormClosed;
                agregarProductoCerrado = false;
                agregarProductoForm.Show();
            }
            else
            {
                agregarProductoForm.Activate();
            }
        }

        private void AgregarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            agregarProductoForm = null;
            agregarProductoCerrado = true;
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mostrarImagenProducto();
        }
        private void mostrarImagenProducto()
        {
            int idProducto = (int)dgvProductos.SelectedRows[0].Cells[0].Value;
            byte[] fotoProducto = productoModel.obtenerFotoProducto(idProducto);

            using (var stream = new MemoryStream(fotoProducto))
            {
                pcbProducto.Image = Image.FromStream(stream);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int idProducto = ((int)dgvProductos.SelectedRows[0].Cells[0].Value);
            string descripcionProducto = dgvProductos.SelectedRows[0].Cells[1].Value.ToString();
            decimal precio = (decimal)dgvProductos.SelectedRows[0].Cells[2].Value;
            int stock = ((int)dgvProductos.SelectedRows[0].Cells[3].Value);


            Image imagen = pcbProducto.Image;
            int maxDimension = 800;
            Image imagenReducida = imagen.GetThumbnailImage(maxDimension, maxDimension, null, IntPtr.Zero);

            // Guardar la imagen reducida en el MemoryStream en formato JPEG
            MemoryStream ms = new MemoryStream();
            imagenReducida.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // Obtener el arreglo de bytes de la imagen
            byte[] foto = ms.ToArray();

            ModificarProducto modificarProductoForm = new ModificarProducto(idProducto, descripcionProducto, precio, stock, foto, this);
            modificarProductoForm.Show();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int idProducto = ((int)dgvProductos.SelectedRows[0].Cells[0].Value);

            if (MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                productoModel.deleteProduct(idProducto);
                refrescarDataGridView();
                MessageBox.Show("El producto se ha eliminado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            dgvProductos.DataSource = productoModel.buscarProducto(txtDescripcion.Text);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)  && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten caracteres", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {

                e.Handled = false;

            }
        }
    }
}









