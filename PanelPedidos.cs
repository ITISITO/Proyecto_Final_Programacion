using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class PanelPedidos : Form
    {
        private SesionLogin sesionActiva;
        private Timer temporizador;
        private int tiempoRestante;

        // ⬇️⬇️⬇️ NUEVO - AGREGAR ESTAS 2 LÍNEAS ⬇️⬇️⬇️
        private Pedido pedido = new Pedido();
        private List<Pedido> ListaPedidos = new List<Pedido>();

        public PanelPedidos(SesionLogin sesion)
        {
            InitializeComponent();

            this.sesionActiva = sesion;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormClosing += Cerrar_Formulario;

            Plato platoEjemplo = new Plato();
            if (platoEjemplo.ReadDataFromJson().Count == 0)
            {
                platoEjemplo.CrearPlatosEjemplo();
            }

            InicializarInterfazSesion();
            IniciarContadorSesion();
            CargarPedidos(); // ⬅️⬅️⬅️ NUEVO - AGREGAR ESTA LÍNEA
        }

        private void InicializarInterfazSesion()
        {
            lblNombre.Text = sesionActiva.Nombre;
            lblRol.Text = sesionActiva.Rol;

            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.AddRange(new object[] { "Todos", "En Preparación", "Listo", "Entregado" });
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void IniciarContadorSesion()
        {
            if (sesionActiva != null)
            {
                var inicio = sesionActiva.FechaInicio;
                var expiracion = inicio.AddMinutes(30);
                var segundosRestantes = (int)(expiracion - DateTime.Now).TotalSeconds;
                tiempoRestante = Math.Max(segundosRestantes, 0);
            }
            else
            {
                tiempoRestante = 30 * 60;
            }

            temporizador = new Timer();
            temporizador.Interval = 1000;
            temporizador.Tick += TemporizadorSesion_Tick;
            temporizador.Start();

            ActualizarDisplayTiempo();
        }

        private void TemporizadorSesion_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;

            if (tiempoRestante < 0)
                tiempoRestante = 0;

            ActualizarDisplayTiempo();

            if (tiempoRestante <= 0)
            {
                temporizador.Stop();
                CerrarSesionPorExpiracion();
            }
        }

        private void ActualizarDisplayTiempo()
        {
            int minutos = tiempoRestante / 60;
            int segundos = tiempoRestante % 60;

            lblTiempoSesion.Text = $"Tiempo Restante: {minutos:D2}:{segundos:D2}";
        }

        private void CerrarSesionPorExpiracion()
        {
            MessageBox.Show(
                "La sesión ha expirado por inactividad.",
                "Sesión finalizada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            CerrarSesion();
        }

        private void CerrarSesion()
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                temporizador.Dispose();
            }

            string archivoSesion = "SesionActiva.json";
            if (File.Exists(archivoSesion))
            {
                File.Delete(archivoSesion);
            }

            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
            this.Close();
        }

        private void Cerrar_Formulario(object sender, FormClosingEventArgs e)
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                temporizador.Dispose();
            }

            Application.Exit();
        }

        // ⬇️⬇️⬇️⬇️⬇️ NUEVO - TODO ESTO ES NUEVO ⬇️⬇️⬇️⬇️⬇️

        private void CargarPedidos()
        {
            flowPanelPedidos.Controls.Clear();

            ListaPedidos = pedido.ReadDataFromJson();

            string filtro = cmbFiltroEstado.SelectedItem?.ToString() ?? "Todos";

            var pedidosFiltrados = filtro == "Todos"
                ? ListaPedidos
                : ListaPedidos.Where(p => p.Estado == filtro).ToList();

            foreach (var pedidoItem in pedidosFiltrados)
            {
                PedidoCard card = new PedidoCard(pedidoItem);

                card.OnEditar += Card_OnEditar;
                card.OnEliminar += Card_OnEliminar;
                card.OnCambiarEstado += Card_OnCambiarEstado;

                flowPanelPedidos.Controls.Add(card);
            }
        }

        private void Card_OnEditar(Pedido pedidoItem)
        {
            FormPedido formEditar = new FormPedido(pedidoItem);

            if (formEditar.ShowDialog() == DialogResult.OK)
            {
                CargarPedidos();
            }
        }

        private void Card_OnEliminar(Pedido pedidoItem)
        {
            DialogResult confirmar = MessageBox.Show(
                $"¿Seguro que deseas eliminar el pedido de la mesa {pedidoItem.NumeroMesa}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                ListaPedidos.Remove(pedidoItem);
                pedido.GuardarJson(ListaPedidos);
                CargarPedidos();
            }
        }

        private void Card_OnCambiarEstado(Pedido pedidoItem)
        {
            string nuevoEstado = "";

            switch (pedidoItem.Estado)
            {
                case "En Preparación":
                    nuevoEstado = "Listo";
                    break;
                case "Listo":
                    nuevoEstado = "Entregado";
                    break;
                case "Entregado":
                    nuevoEstado = "En Preparación";
                    break;
                default:
                    nuevoEstado = "En Preparación";
                    break;
            }

            var index = ListaPedidos.FindIndex(p => p.Id == pedidoItem.Id);
            if (index != -1)
            {
                ListaPedidos[index].Estado = nuevoEstado;
                pedido.GuardarJson(ListaPedidos);
                CargarPedidos();
            }
        }

        // ⬆️⬆️⬆️⬆️⬆️ FIN DE LO NUEVO ⬆️⬆️⬆️⬆️⬆️

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var confirmar = MessageBox.Show(
                "¿Seguro que deseas cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                CerrarSesion();
            }
        }

        private void cmbFiltroEstado_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CargarPedidos(); // ⬅️⬅️⬅️ CAMBIAR - AGREGAR ESTA LÍNEA
        }

        private void btnNuevoPedido_Click_1(object sender, EventArgs e)
        {
            FormPedido formPedido = new FormPedido();

            if (formPedido.ShowDialog() == DialogResult.OK) // ⬅️⬅️⬅️ CAMBIAR - AGREGAR EL IF
            {
                CargarPedidos(); // ⬅️⬅️⬅️ NUEVO
            }
        }

        private void gestionarMenúToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormGestionPlatos formPlatos = new FormGestionPlatos();
            formPlatos.ShowDialog();
        }
    }
}