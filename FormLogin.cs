using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormLogin : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> listaUsuarios = new List<Usuario>();
        private string archivoSesion = "SesionActiva.json";
        private int duracionMinutosSesion = 30;

        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sistema de Pedidos - Login";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Cargar usuarios de ejemplo si no existen
            listaUsuarios = usuario.ReadDataFromJson();
            if (listaUsuarios.Count == 0)
            {
                usuario.CrearUsuariosEjemplo();
                listaUsuarios = usuario.ReadDataFromJson();
            }

            VerificarSesionActiva();
        }

        private void VerificarSesionActiva()
        {
            if (File.Exists(archivoSesion))
            {
                try
                {
                    string json = File.ReadAllText(archivoSesion);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var sesion = JsonConvert.DeserializeObject<SesionLogin>(json);

                        // Verificar si la sesión no ha expirado
                        if (sesion != null && DateTime.Now < sesion.FechaInicio.AddMinutes(duracionMinutosSesion))
                        {
                            this.Hide();
                            PanelPedidos panel = new PanelPedidos(sesion);
                            panel.ShowDialog();

                            if (File.Exists(archivoSesion))
                            {
                                Application.Exit();
                            }
                            else
                            {
                                this.Show();
                                LimpiarCampos();
                            }
                        }
                        else
                        {
                            File.Delete(archivoSesion);
                        }
                    }
                }
                catch
                {
                    if (File.Exists(archivoSesion))
                        File.Delete(archivoSesion);
                }
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            LimpiarLabelsError();

            if (!ValidarCampos())
                return;

            string correo = txtCorreo.Text.Trim();
            string password = txtContrasena.Text;

            listaUsuarios = usuario.ReadDataFromJson();

            // ⬇️ USAR CorreoElectronico en lugar de Correo
            var usuarioEncontrado = listaUsuarios.FirstOrDefault(u =>
                u.CorreoElectronico.Equals(correo, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (usuarioEncontrado != null)
            {
                // Crear sesión con toda la información necesaria
                var sesion = new SesionLogin
                {
                    CorreoElectronico = usuarioEncontrado.CorreoElectronico,
                    Nombre = usuarioEncontrado.Nombre,
                    Rol = usuarioEncontrado.Rol,
                    FechaInicio = DateTime.Now
                };

                File.WriteAllText(archivoSesion, JsonConvert.SerializeObject(sesion, Formatting.Indented));

                MessageBox.Show(
                    $"¡Bienvenido {usuarioEncontrado.Nombre}!",
                    "Inicio exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Hide();
                PanelPedidos panel = new PanelPedidos(sesion);
                panel.ShowDialog();

                if (File.Exists(archivoSesion))
                {
                    Application.Exit();
                }
                else
                {
                    this.Show();
                    LimpiarCampos();
                }
            }
            else
            {
                CrearLabelError(txtContrasena, "Correo o contraseña incorrectos");
                MessageBox.Show(
                    "Correo o contraseña incorrectos.",
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                CrearLabelError(txtCorreo, "El correo es obligatorio");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                CrearLabelError(txtContrasena, "La contraseña es obligatoria");
                return false;
            }

            return true;
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro();
            formRegistro.FormClosed += (s, args) =>
            {
                listaUsuarios = usuario.ReadDataFromJson();
            };
            formRegistro.ShowDialog();
        }

        private void CrearLabelError(Control control, string mensaje)
        {
            Label lblError = new Label
            {
                Text = mensaje,
                Location = new Point(control.Location.X, control.Location.Y + control.Height + 2),
                ForeColor = Color.Red,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblError);
        }

        private void LimpiarLabelsError()
        {
            var labelsError = Controls.OfType<Label>()
                .Where(l => l.ForeColor == Color.Red)
                .ToList();

            foreach (var lbl in labelsError)
            {
                Controls.Remove(lbl);
                lbl.Dispose();
            }
        }

        private void LimpiarCampos()
        {
            txtCorreo.Clear();
            txtContrasena.Clear();
            LimpiarLabelsError();
        }
    }
}
