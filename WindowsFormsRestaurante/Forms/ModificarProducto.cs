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
    public partial class ModificarProducto : Form
    {

        int id;
        private Inventario inventarioForm;

        public ModificarProducto(int id, string descripcion, decimal precio, int cantidad, byte[] foto, Inventario inventarioForm)
        {
            InitializeComponent();
            this.id = id;
            this.txtActualizarDescripcion.Text = descripcion;
            this.txtActualizarPrecio.Text = precio.ToString();
            this.txtActualizarCantidad.Text = cantidad.ToString();
            if (foto != null)
            {
                using (MemoryStream ms = new MemoryStream(foto))
                {
                    Image img = Image.FromStream(ms);
                    this.pcbProducto.Image = img;
                }
            }
            this.inventarioForm=inventarioForm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditarFoto_Click(object sender, EventArgs e)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ProductoModel productoModel = new ProductoModel();
            Image imagen = pcbProducto.Image;
            int maxDimension = 800;
            Image imagenReducida = imagen.GetThumbnailImage(maxDimension, maxDimension, null, IntPtr.Zero);

            // Guardar la imagen reducida en el MemoryStream en formato JPEG
            MemoryStream ms = new MemoryStream();
            imagenReducida.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // Obtener el arreglo de bytes de la imagen
            byte[] foto = ms.ToArray();


            if (txtActualizarDescripcion.Text != "" && txtActualizarCantidad.Text != "" && txtActualizarPrecio.Text !="")
            {
                Producto producto = new Producto(id, txtActualizarDescripcion.Text, SqlMoney.Parse(txtActualizarPrecio.Text), int.Parse(txtActualizarCantidad.Text), foto);
                productoModel.updateProduct(producto);
                MessageBox.Show("El producto se ha actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                inventarioForm.refrescarDataGridView();
                this.Close();
            }

            else
            {
                MessageBox.Show("Antes de guardar debes completar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
