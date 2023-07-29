using DataAccess.Entities;
using Domain.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsRestaurante.Forms
{
    public partial class AgregarProducto : Form
    {

        private Inventario inventarioForm;

        public AgregarProducto(Inventario inventarioForm)
        {
            InitializeComponent();
            this.inventarioForm = inventarioForm;
        }

        private void limpiarTextBox()
        {
            txtCantidad.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            byte[] imagenBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pcbProducto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagenBytes = ms.ToArray();
            }

            ProductoModel productoModel = new ProductoModel();

            if (txtDescripcion.Text != "" && txtCantidad.Text != "" && txtPrecio.Text !="")
            {
                Producto producto = new Producto(txtDescripcion.Text, SqlMoney.Parse(txtPrecio.Text), int.Parse(txtCantidad.Text), imagenBytes);
                productoModel.insertProduct(producto);
                limpiarTextBox();            
                MessageBox.Show("Se ha agregado el producto correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Image imagen = Image.FromFile("C:\\Users\\ANTHO-PC\\source\\repos\\SolutionRestaurante\\WindowsFormsRestaurante\\Resources\\Img\\fotopredeterminada.jpg");
                pcbProducto.Image = imagen;
                inventarioForm.refrescarDataGridView();
            }

            else
            {
                MessageBox.Show("Antes de guardar debes completar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {

        }

        private void AgregarProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pcbProducto.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
        }
    }
}
