using DataAccess.DAO;
using DataAccess.Entities;
using Domain.Models;
using Domain.ValueObjetcs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsRestaurante.Shared;
using WindowsFormsRestaurante.Utilities;

namespace WindowsFormsRestaurante.Forms
{
    public partial class Pedidos : Form
    {
        int idMesero = UserSession.IdUsuario;
        PedidoManager pedidoManager = PedidoManager.GetInstance(); // 
        string[,] Lista = new string[200, 200];
        int fila = 0;
        string nDeMesa;
        public Pedidos()
        {
            InitializeComponent();
        }

        public Pedidos(string nDeMesa)
        {
            InitializeComponent();
            this.nDeMesa = nDeMesa;
            MostrarDetallesPedido(int.Parse(nDeMesa));

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            if (txtTotal.Text != "" && txtPagoCon.Text != ""  && txtDevuelta.Text !="" && dgvProducto.RowCount > 0)
            {
                if (double.Parse(txtPagoCon.Text) < double.Parse(txtTotal.Text))
                {
                    MessageBox.Show("La cantidad introducida no puede ser menor al total a pagar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPagoCon.Focus();
                }

                else
                {

                    DateTime fechaActual = DateTime.Now;
                    PedidoModel pedidoModel = new PedidoModel();
                    DetallePedidoModel detallePedidoModel = new DetallePedidoModel();
                    Pedido pedido = new Pedido(int.Parse(lblNumeroMesa.Text), idMesero, fechaActual, SqlMoney.Parse(txtTotal.Text));
                    pedidoModel.insertarPedido(pedido, out int idPedido);

                    foreach (DataGridViewRow Rows in dgvProducto.Rows)
                    {
                        object cellIdProducto = Rows.Cells[0].Value;
                        int idProducto = Convert.ToInt32(cellIdProducto);

                        object cellCantidadProducto = Rows.Cells[3].Value;
                        int cantidadProductos = Convert.ToInt32(cellCantidadProducto);

                        SqlMoney precioUnitario = (SqlMoney)Rows.Cells[2].Value;

                        decimal subtotalDecimal = Convert.ToDecimal(Rows.Cells[4].Value);
                        SqlMoney subtotal = new SqlMoney(subtotalDecimal);

                        DetallePedido detallePedido = new DetallePedido(idPedido, idProducto, cantidadProductos, precioUnitario, subtotal);
                        detallePedidoModel.insertarDetallePedido(detallePedido);

                        ProductoModel productoModel = new ProductoModel();
                        productoModel.actualizarCantidadProducto(idProducto, cantidadProductos);

                    }

                    imprimirTicketFactura();
                    pedidoManager.eliminarPedido(int.Parse(nDeMesa));
                    dgvProducto.Rows.Clear();
                    MesaModel mesaModel = new MesaModel();
                    mesaModel.cambiarEstadoMesa(int.Parse(nDeMesa), "disponible");

                    Mesas mesas = new Mesas();
                    MenuPrincipal mainMenu = (MenuPrincipal)this.ParentForm;
                    mainMenu.abrirVentana(mesas);
                }

            }
            else
            {
                MessageBox.Show("Antes de imprimir debe completar la información necesaria", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public void establecerProducto(string id, string descripcion, decimal precio)
        {
            this.txtId.Text = id;
            this.txtDescripcion.Text = descripcion;
            this.txtPrecio.Text = precio.ToString();
            this.txtCantidad.Focus();

        }

        private void imprimirTicketFactura()
        {
            Utilities.CreaTicket ticket = new Utilities.CreaTicket();
            MeseroModel meseroModel = new MeseroModel();

            string nombreCompletoMesero = meseroModel.obtenerNombreMesero(idMesero);

            ticket.TextoCentro("LA PARRILLA PERFECTA"); //imprime una linea de descripcion
            ticket.TextoCentro("**********************************");

            ticket.TextoIzquierda("Dirc: Av. Charles de Gaulle 74");
            ticket.TextoIzquierda("Tel: 809-313-3124");
            ticket.TextoIzquierda("Rnc: 130-99999-(1)");
            ticket.TextoIzquierda("");
            ticket.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            ticket.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            ticket.TextoIzquierda("Le Atendio: " + nombreCompletoMesero);
            ticket.TextoIzquierda("");

            CreaTicket.LineasGuion();
            CreaTicket.EncabezadoVenta();
            CreaTicket.LineasGuion();

            foreach (DataGridViewRow r in dgvProducto.Rows)
            { //                        Nombre del articulo                Precio                                   Cantidad                                SubTotal 


                ticket.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString()));//imprime una linea de descripcion                                                                                                                                                                                               //imprime una linea de descripcion
            }


            CreaTicket.LineasGuion();
            ticket.AgregaTotales("Total", double.Parse(txtTotal.Text)); // imprime linea con total
            ticket.TextoIzquierda(" ");
            ticket.AgregaTotales("Efectivo Entregado:", double.Parse(txtPagoCon.Text));
            ticket.AgregaTotales("Efectivo Devuelto:", double.Parse(txtDevuelta.Text));

            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("**********************************");
            ticket.TextoCentro("*     Gracias por preferirnos    *");
            ticket.TextoCentro("**********************************");
            ticket.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer"; //mpueden usar variable
            ticket.ImprimirTiket(impresora);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (ProductoEmergente frmProductos = new ProductoEmergente())
            {
                frmProductos.Owner = this;
                frmProductos.ShowDialog();
            }

            txtCantidad.Focus();
        }


        public void CostoAPagar()
        {

            decimal costoTotalDecimal = 0;
            SqlMoney costoTotal = 0;
            int contarFilas = 0;

            contarFilas = dgvProducto.RowCount;

            for (int a = 0; a < contarFilas; a++)
            {
                if (decimal.TryParse(dgvProducto.Rows[a].Cells[4].Value.ToString(), out costoTotalDecimal))
                {

                    costoTotal = new SqlMoney(costoTotalDecimal);

                }

            }

            txtTotal.Text = costoTotal.ToString();
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {


            DataGridViewRow row = null;

            //Reccorre todas las filas del datagrid y verifica si ya existe un platillo con el mismo nombre

            foreach (DataGridViewRow r in dgvProducto.Rows)
            {
                if (r.Cells[1].Value.ToString() == txtDescripcion.Text)
                {
                    row = r;
                    break;
                }
            }


            if (txtDescripcion.Text != "" && txtCantidad.Text != "" && txtPrecio.Text != "")
            {

                if (row != null)
                {
                    int cantidad = Convert.ToInt32(row.Cells[3].Value) + Convert.ToInt32(txtCantidad.Text);

                    SqlMoney precioSql = (SqlMoney)row.Cells[2].Value;
                    double precio = (double)precioSql.Value;

                    double subtotal = cantidad * precio;

                    row.Cells[3].Value = cantidad.ToString();
                    row.Cells[4].Value = subtotal.ToString();
                }

                //Sino, solo se agrega el producto
                else
                {

                    if (dgvProducto.Rows.Count >= 0)
                    {
                        Lista[fila, 0] = txtId.Text;
                        Lista[fila, 1] = txtDescripcion.Text;
                        Lista[fila, 2] = txtPrecio.Text;
                        Lista[fila, 3] = txtCantidad.Text;
                        Lista[fila, 4] = (SqlMoney.Parse(txtCantidad.Text) * SqlMoney.Parse(txtPrecio.Text)).ToString();

                        dgvProducto.Rows.Add(Lista[fila, 0], Lista[fila, 1], SqlMoney.Parse(Lista[fila, 2]), Lista[fila, 3], Lista[fila, 4]);

                        PedidoDetalle guardarPedidoDetalleTemp = new PedidoDetalle(int.Parse(Lista[fila, 0]), Lista[fila, 1], SqlMoney.Parse(Lista[fila, 2]), int.Parse(Lista[fila, 3]), SqlMoney.Parse(Lista[fila, 4]));
                        pedidoManager.AgregarPedido(int.Parse(nDeMesa), new List<PedidoDetalle> { guardarPedidoDetalleTemp });
                        Dictionary<int, List<PedidoDetalle>> pedidosPorMesa;
                        pedidosPorMesa = new Dictionary<int, List<PedidoDetalle>>();
                        pedidosPorMesa = pedidoManager.PedidosPorMesa;

                    }

                }

                txtId.Text = txtDescripcion.Text = txtPrecio.Text = txtCantidad.Text = "";
                CostoAPagar();
            }

            else
            {
                MessageBox.Show("Debes completar todos los campos antes de insertarlos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.SelectedRows.Count > 0)
            {
                dgvProducto.Rows.RemoveAt(0);
                CostoAPagar();
            }

            else
            {
                MessageBox.Show("Seleccione una fila", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MostrarDetallesPedido(int nDeMesa)
        {
            List<PedidoDetalle> detalles = PedidoManager.GetInstance().ObtenerDetallesPedido(nDeMesa);

            if (detalles != null)
            {
                dgvProducto.Rows.Clear();
                foreach (PedidoDetalle detalle in detalles)
                {
                    dgvProducto.Rows.Add(detalle.idProducto, detalle.descripcion, detalle.precio, detalle.cantidad, detalle.subtotal);
                }

                CostoAPagar();
            }
        }

        private void pcbMofongo_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            // Obtener información de la comida del Tag del PictureBox
            string tag = pictureBox.Tag.ToString();
            string id = tag.Split('-')[0];
            string descripcion = tag.Split('-')[1];
            decimal precio = decimal.Parse(tag.Split('-')[2]);

            // Mostrar la información en los TextBox correspondientes
            txtId.Text = id;
            txtDescripcion.Text = descripcion;
            txtPrecio.Text = precio.ToString();
            txtCantidad.Focus();
        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
            lblNumeroMesa.Text = nDeMesa;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Mesas mesas = new Mesas();
            MenuPrincipal mainMenu = (MenuPrincipal)this.ParentForm;
            mainMenu.abrirVentana(mesas);
        }

        private void txtPagoCon_TextChanged(object sender, EventArgs e)
        {
            if (txtTotal.Text == "")
            {
                MessageBox.Show("No puedes introducir una cantidad si el total a pagar esta vacio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                try
                {
                    if (txtPagoCon.Text == "")
                    {
                        txtPagoCon.Text = "0";
                    }

                    SqlMoney.Parse(txtPagoCon.Text);
                    txtDevuelta.Text = (SqlMoney.Parse(txtPagoCon.Text) - SqlMoney.Parse(txtTotal.Text)).ToString();

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);

                }
            }

        }

        private void txtPagoCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPagoCon_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void txtPagoCon_Click(object sender, EventArgs e)
        {
            txtPagoCon.Text = "";
        }
    }
}
