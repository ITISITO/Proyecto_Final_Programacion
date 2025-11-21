using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class PanelPedidos : Form
    {
        private SesionLogin sesionActiva;
        private Timer temporizador;
        private int tiempoRestante;

        public PanelPedidos(SesionLogin sesion)
        {
            InitializeComponent();

            this.sesionActiva = sesion;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += Cerrar_Formulario;

            // Verificar que los platos de ejemplo existan
            Plato platoEjemplo = new Plato();
            if (platoEjemplo.ReadDataFromJson().Count == 0)
            {
                platoEjemplo.CrearPlatosEjemplo();
            }

            InicializarInterfazSesion();
            IniciarContadorSesion();
        }

        private void InicializarInterfazSesion()
        {
            // Actualizar los labels con la información del usuario
            lblNombre.Text = sesionActiva.Nombre;
            lblRol.Text = sesionActiva.Rol;

            // Cargar estados en el ComboBox
            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.AddRange(new object[] { "Todos", "En Preparación", "Listo", "Entregado" });
            cmbFiltroEstado.SelectedIndex = 0; // Seleccionar "Todos" por defecto
        }

        private void IniciarContadorSesion()
        {
            // Calcular tiempo restante desde que inició la sesión
            if (sesionActiva != null)
            {
                var inicio = sesionActiva.FechaInicio;
                var expiracion = inicio.AddMinutes(30);
                var segundosRestantes = (int)(expiracion - DateTime.Now).TotalSeconds;
                tiempoRestante = Math.Max(segundosRestantes, 0);
            }
            else
            {
                tiempoRestante = 30 * 60; // 30 minutos en segundos
            }

            // Crear y configurar el timer
            temporizador = new Timer();
            temporizador.Interval = 1000; // 1 segundo
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

        }

        private void btnNuevoPedido_Click_1(object sender, EventArgs e)
        {
            // Abrir formulario de nuevo pedido
            FormPedido formPedido = new FormPedido();
            formPedido.ShowDialog();
        }

        private void gestionarMenúToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Abrir formulario de gestión de platos
            FormGestionPlatos formPlatos = new FormGestionPlatos();
            formPlatos.ShowDialog();
        }
    }
}
