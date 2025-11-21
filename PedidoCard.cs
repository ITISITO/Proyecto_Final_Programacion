using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class PedidoCard : UserControl
    {
        private Pedido pedidoActual;

        public event Action<Pedido> OnEditar;
        public event Action<Pedido> OnEliminar;
        public event Action<Pedido> OnCambiarEstado;

        public PedidoCard()
        {
            InitializeComponent();
        }

        public PedidoCard(Pedido pedido)
        {
            InitializeComponent();
            this.pedidoActual = pedido;
            CargarDatos();
            AplicarColorEstado();
        }

        private void CargarDatos()
        {
            if (pedidoActual == null) return;

            lblMesa.Text = $"Mesa: {pedidoActual.NumeroMesa}";
            lblCliente.Text = $"Cliente: {pedidoActual.NombreCliente}";
            lblPlatos.Text = $"Platos: {pedidoActual.ObtenerResumenPlatos()}";
            lblTotal.Text = $"Total: ${pedidoActual.Total:F2}";
            lblEstado.Text = $"Estado: {pedidoActual.Estado}";
            lblFecha.Text = $"Fecha: {pedidoActual.FechaPedido:dd/MM/yyyy HH:mm}";
        }

        private void AplicarColorEstado()
        {
            switch (pedidoActual.Estado)
            {
                case "En Preparación":
                    this.BackColor = Color.FromArgb(255, 243, 224);
                    lblEstado.ForeColor = Color.FromArgb(255, 140, 0);
                    break;

                case "Listo":
                    this.BackColor = Color.FromArgb(220, 255, 220);
                    lblEstado.ForeColor = Color.FromArgb(0, 128, 0);
                    break;

                case "Entregado":
                    this.BackColor = Color.FromArgb(240, 240, 240);
                    lblEstado.ForeColor = Color.FromArgb(100, 100, 100);
                    break;

                default:
                    this.BackColor = Color.White;
                    lblEstado.ForeColor = Color.Black;
                    break;
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            OnEliminar?.Invoke(pedidoActual);

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            OnEditar?.Invoke(pedidoActual);

        }

        private void btnCambiarEstado_Click_1(object sender, EventArgs e)
        {
            OnCambiarEstado?.Invoke(pedidoActual);
        }
    }
}
