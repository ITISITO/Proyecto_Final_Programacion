using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class PanelPedidos : Form
    {
        private SesionLogin sesionActiva;
        private Timer temporizador;
        private int tiempoRestante;

        private Pedido pedido = new Pedido();
        private List<Pedido> ListaPedidos = new List<Pedido>();

        public PanelPedidos(SesionLogin sesion)
        {
            InitializeComponent();

            this.sesionActiva = sesion;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += Cerrar_Formulario;

            ListaPedidos = pedido.ReadDataFromJson();

            Plato platoEjemplo = new Plato();
            if (platoEjemplo.ReadDataFromJson().Count == 0)
            {
                platoEjemplo.CrearPlatosEjemplo();
            }

            InicializarInterfazSesion();
            CargarPedidos();
            IniciarContadorSesion();
        }

        private void InicializarInterfazSesion()
        {
            this.Text = $"Sistema de Pedidos - {sesionActiva.Nombre} ({sesionActiva.Rol})";
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

            this.Text = $"Sistema de Pedidos - {sesionActiva.Nombre} | ⏱️ {minutos:D2}:{segundos:D2}";
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

        private void CargarPedidos()
        {
            // Tu código para cargar pedidos
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

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {

        }
    }
}