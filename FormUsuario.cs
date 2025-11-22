using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormUsuario : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> lista = new List<Usuario>();
        private SesionLogin sesionActiva; // ⬅️ AGREGAR
        private Timer temporizador;
        private int tiempoRestante;


        // ⬇️⬇️⬇️ CAMBIAR: Agregar parámetro sesion
        public FormUsuario(SesionLogin sesion)
        {
            InitializeComponent();

            this.sesionActiva = sesion; // ⬅️ NUEVO

            // ⬇️⬇️⬇️ NUEVO: Verificar que solo el Admin pueda acceder
            if (sesion.Rol != "Administrador")
            {
                MessageBox.Show(
                    "No tienes permisos para acceder a esta sección",
                    "Acceso Denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            dgvPersonas.DataSource = usuario.ReadDataFromJson();
            CargarDataGrid();
            dgvPersonas.CellClick += OnCellClick;
            IniciarContadorSesion();
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

            lblTiempoSesion.Text = $"Tiempo: {minutos:D2}:{segundos:D2}";
        }

        private void CerrarSesionPorExpiracion()
        {
            MessageBox.Show("La sesión ha expirado", "Sesión finalizada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnCerrarSesion_Click(null, null);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // ⬇️⬇️⬇️ CAMBIAR: Pasar true para que sea admin
            FormRegistro form1 = new FormRegistro(true);
            var result = form1.ShowDialog();
            if (result == DialogResult.OK)
            {
                CargarDataGrid();
            }
        }

        private void CargarDataGrid()
        {
            lista = usuario.ReadDataFromJson();
            dgvPersonas.Columns.Clear();
            dgvPersonas.DataSource = null;
            dgvPersonas.DataSource = lista;
            dgvPersonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ocultar columna de contraseña si existe
            if (dgvPersonas.Columns.Contains("Password"))
            {
                dgvPersonas.Columns["Password"].Visible = false;
            }

            if (!dgvPersonas.Columns.Contains("btnEditar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                btnEditar.HeaderText = "Editar";
                btnEditar.Name = "btnEditar";
                btnEditar.Text = "Editar";
                btnEditar.UseColumnTextForButtonValue = true;
                dgvPersonas.Columns.Add(btnEditar);
            }

            if (!dgvPersonas.Columns.Contains("btnEliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Name = "btnEliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dgvPersonas.Columns.Add(btnEliminar);
            }
        }

        private void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPersonas.Columns["btnEditar"].Index)
            {
                var usuarioSeleccionado = lista[e.RowIndex];
                // ⬇️⬇️⬇️ CAMBIAR: Pasar true para que sea admin
                FormRegistro form1 = new FormRegistro(usuarioSeleccionado, true);
                var result = form1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarDataGrid();
                }
            }

            if (e.RowIndex >= 0 && dgvPersonas.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                var usuarioSeleccionado = lista[e.RowIndex];

                // ⬇️⬇️⬇️ NUEVO: No permitir eliminar al admin actual
                if (usuarioSeleccionado.CorreoElectronico == sesionActiva.CorreoElectronico)
                {
                    MessageBox.Show(
                        "No puedes eliminar tu propio usuario",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmar = MessageBox.Show(
                    $"¿Seguro que quieres eliminar al usuario {usuarioSeleccionado.Nombre}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    lista.Remove(usuarioSeleccionado);
                    usuario.GuardarJson(lista);
                    CargarDataGrid();
                }
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}