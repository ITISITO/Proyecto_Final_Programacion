using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormRegistro : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> lista = new List<Usuario>();
        private Usuario usuarioSeleccionado = new Usuario();
        private bool esAdminGestionando = false;

        // Constructor 1: EDITAR usuario (desde FormUsuario - Admin)
        public FormRegistro(Usuario usuarioSeleccionado, bool esAdmin = true)
        {
            InitializeComponent();
            Height = 500;
            Width = 500;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Sizable; // ⬅️ CAMBIAR (era None)
            this.StartPosition = FormStartPosition.CenterParent; // ⬅️ AGREGAR

            lista = usuario.ReadDataFromJson();
            this.usuarioSeleccionado = usuarioSeleccionado;
            this.esAdminGestionando = esAdmin;

            txtNombre.Text = usuarioSeleccionado.Nombre;
            txtCorreoElec.Text = usuarioSeleccionado.CorreoElectronico;
            txtPassword.Text = usuarioSeleccionado.Password;
            txtConfirmarPass.Text = usuarioSeleccionado.Password;

            // Controlar selector de rol
            if (esAdmin)
            {
                CargarRoles();
                try
                {
                    cmbRol.Text = usuarioSeleccionado.Rol;
                    cmbRol.Visible = true;
                }
                catch { }
            }
            else
            {
                try
                {
                    cmbRol.Visible = false;
                }
                catch { }
            }
        }

        // Constructor 2: AUTO-REGISTRO (desde Login)
        public FormRegistro()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.esAdminGestionando = false;

            // Ocultar selector de rol
            try
            {
                cmbRol.Visible = false;
                // Ocultar label de rol si existe
                foreach (Control c in Controls)
                {
                    if (c is Label && c.Text.Contains("Rol"))
                    {
                        c.Visible = false;
                    }
                }
            }
            catch { }
        }

        // Constructor 3: ADMIN crea nuevo usuario
        public FormRegistro(bool esAdmin)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.esAdminGestionando = esAdmin;

            if (esAdmin)
            {
                CargarRoles();
                try
                {
                    cmbRol.Visible = true;
                }
                catch { }
            }
            else
            {
                try
                {
                    cmbRol.Visible = false;
                }
                catch { }
            }
        }

        private void CargarRoles()
        {
            //agregar roles al ComboBox
            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(new object[]
            {
                "Admin",
                "Cocinero",
                "Mesero"
            });

        }

        private void LimpiarLabelsError()
        {
            var etiquetasError = Controls.OfType<Label>().Where(l => l.ForeColor == Color.Red).ToList();
            foreach (var lbl in etiquetasError)
            {
                Controls.Remove(lbl);
                lbl.Dispose();
            }
        }

        private void CrearLabel(System.Windows.Forms.TextBox textbox, string mensaje)
        {
            Label lblError = new Label();
            lblError.Text = mensaje;
            lblError.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            lblError.ForeColor = Color.Red;
            lblError.AutoSize = true;
            Controls.Add(lblError);
        }

        private bool ValidarFormulario()
        {
            bool isValid = true;
            LimpiarLabelsError();

            foreach (var control in Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;

                    if (textBox.Text == "")
                    {
                        CrearLabel(textBox, $"El campo {textBox.Tag} es requerido");
                        isValid = false;
                    }
                    else if (textBox == txtCorreoElec)
                    {
                        string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                        if (!Regex.IsMatch(textBox.Text, patronCorreo))
                        {
                            CrearLabel(textBox, "El correo electrónico no es válido");
                            isValid = false;
                        }
                    }
                    else if (textBox == txtPassword)
                    {
                        if (textBox.Text.Length < 6)
                        {
                            CrearLabel(textBox, "La contraseña debe tener mínimo 6 caracteres");
                            isValid = false;
                        }
                        else if (txtConfirmarPass.Text != txtPassword.Text)
                        {
                            CrearLabel(txtConfirmarPass, "Las contraseñas no coinciden");
                            isValid = false;
                        }
                    }
                }
            }

            // Validar rol solo si es admin y el ComboBox existe
            try
            {
                if (esAdminGestionando && cmbRol.Visible && string.IsNullOrEmpty(cmbRol.Text))
                {
                    MessageBox.Show("Debe seleccionar un rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isValid = false;
                }
            }
            catch { }

            return isValid;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var isValid = ValidarFormulario();

            if (!isValid)
            {
                MessageBox.Show("El formulario tiene errores");
                return;
            }

            lista = usuario.ReadDataFromJson();

            bool esEdicion = !string.IsNullOrEmpty(usuarioSeleccionado?.CorreoElectronico);

            bool correoExiste = lista.Any(u =>
                u.CorreoElectronico.Equals(txtCorreoElec.Text, StringComparison.OrdinalIgnoreCase) &&
                (!esEdicion || u.CorreoElectronico != usuarioSeleccionado.CorreoElectronico));

            if (correoExiste)
            {
                MessageBox.Show("El correo electrónico ya está registrado. Por favor, use otro.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (esEdicion)
            {
                var index = lista.FindIndex(u => u.CorreoElectronico == usuarioSeleccionado.CorreoElectronico);
                if (index != -1)
                {
                    lista[index].Nombre = txtNombre.Text;
                    lista[index].CorreoElectronico = txtCorreoElec.Text;
                    lista[index].Password = txtPassword.Text;

                    // Solo cambiar rol si es admin
                    try
                    {
                        if (esAdminGestionando && cmbRol.Visible)
                        {
                            lista[index].Rol = cmbRol.Text;
                        }
                    }
                    catch { }
                }
            }
            else
            {
                // Asignar rol según contexto
                string rolAsignado = "Mesero"; // Por defecto

                // Si es Admin, usar el rol seleccionado
                try
                {
                    if (esAdminGestionando && cmbRol.Visible && !string.IsNullOrEmpty(cmbRol.Text))
                    {
                        rolAsignado = cmbRol.Text;
                    }
                }
                catch { }

                Usuario nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreoElec.Text,
                    Password = txtPassword.Text,
                    Rol = rolAsignado
                };
                lista.Add(nuevoUsuario);
            }

            usuario.GuardarJson(lista);

            MessageBox.Show("Formulario enviado con éxito");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

