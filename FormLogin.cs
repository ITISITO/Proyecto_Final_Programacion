using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormLogin : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> listaUsuarios = new List<Usuario>();
        private string archivoSesion = "sesion.json";

        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";

            // Verificar si hay una sesión activa
            if (File.Exists(archivoSesion))
            {
                string json = File.ReadAllText(archivoSesion);
                var sesion = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (sesion != null && sesion.ContainsKey("UsuarioLogueado"))
                {
                    FormUsuario panel = new FormUsuario();
                    panel.Show();
                    this.Hide();
                    return;
                }
            }


            btnIniciarSesion.Click += btnIniciarSesion_Click;
            btnRegistrarse.Click += btnRegistrarse_Click;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            LimpiarLabelsError();

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                CrearLabelError(txtCorreo, "Ingrese su correo y contraseña");
                return;
            }

            listaUsuarios = usuario.ReadDataFromJson();

            var usuarioEncontrado = listaUsuarios.FirstOrDefault(u =>
                u.CorreoElectronico == txtCorreo.Text && u.Password == txtContrasena.Text);

            if (usuarioEncontrado != null)
            {
                // Crear archivo de sesión
                var sesion = new
                {
                    UsuarioLogueado = usuarioEncontrado.CorreoElectronico,
                    FechaInicio = DateTime.Now
                };

                File.WriteAllText(archivoSesion, JsonConvert.SerializeObject(sesion, Formatting.Indented));

                MessageBox.Show($"Bienvenido {usuarioEncontrado.Nombre}", "Inicio de sesión correcto");

                FormUsuario panel = new FormUsuario();
                panel.Show();
                this.Hide();
            }
            else
            {
                CrearLabelError(txtCorreo, "Correo o contraseña incorrectos");
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            FormRegistro registro = new FormRegistro();
            registro.ShowDialog();

        }

        private void CrearLabelError(Control control, string mensaje)
        {
            Label lblError = new Label();
            lblError.Text = mensaje;
            lblError.Location = new Point(control.Location.X, control.Location.Y + control.Height);
            lblError.ForeColor = Color.Red;
            lblError.AutoSize = true;
            Controls.Add(lblError);
        }

        private void LimpiarLabelsError()
        {
            var labelsError = Controls.OfType<Label>().Where(l => l.ForeColor == Color.Red).ToList();
            foreach (var lbl in labelsError)
            {
                Controls.Remove(lbl);
                lbl.Dispose();
            }
        }
    }
}
