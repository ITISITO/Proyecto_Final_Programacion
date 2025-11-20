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
    public partial class FormRegistro: Form
    {
        private Usuario usuario = new Usuario();

        private List<Usuario> lista = new List<Usuario>();

        private Usuario usuarioSeleccionado = new Usuario();

        public FormRegistro(Usuario usuarioSeleccionado)
        {
            Height = 500;
            Width = 500;
            MaximizeBox = true;
            FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();


            lista = usuario.ReadDataFromJson();

            txtNombre.Text = usuarioSeleccionado.Nombre;

            this.usuarioSeleccionado = usuarioSeleccionado;
            txtCorreoElec.Text = usuarioSeleccionado.CorreoElectronico;

            txtPassword.Text = usuarioSeleccionado.Password;

        }
        public FormRegistro()
        {
            InitializeComponent();
        }

        private void LimpiarLabelsError()
        {
            // Usamos esto para que las labels se actualicen y no queden en rojo cuando ya se corrija el error
            var etiquetasError = Controls.OfType<Label>().Where(l => l.ForeColor == Color.Red).ToList();
            foreach (var lbl in etiquetasError)
            {
                Controls.Remove(lbl);
                lbl.Dispose();
            }
        }//Fin

        private void CrearLabel(System.Windows.Forms.TextBox textbox, string mensaje)
        {
            Label lblError = new Label();
            lblError.Text = mensaje;
            lblError.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            lblError.ForeColor = Color.Red;
            lblError.AutoSize = true;
            Controls.Add(lblError);

        }//Fin

        private bool ValidarFormulario()
        {
            bool isValid = true;
            LimpiarLabelsError();

            foreach (var control in Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)control;

                    if (textBox.Text == "")
                    {
                        CrearLabel(textBox, $"El campo {textBox.Tag} es requerido");

                        isValid = false;

                    }

                    else if (textBox == txtCorreoElec) //Para validar correos
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
            return isValid;
        }// Fin ValidarFormulario

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var isValid = ValidarFormulario();

            if (!isValid)
            {
                MessageBox.Show("El formulario tiene errores");
                return;
            }

            lista = usuario.ReadDataFromJson();

            // Validar si esta editando o agregando un nuevo usuario
            bool esEdicion = !string.IsNullOrEmpty(usuarioSeleccionado?.CorreoElectronico);

            // Validar que no se duplique el correo 
            bool correoExiste = lista.Any(u =>
                u.CorreoElectronico.Equals(txtCorreoElec.Text, StringComparison.OrdinalIgnoreCase));

            if (correoExiste && !esEdicion)
            {
                MessageBox.Show("El correo electrónico ya está registrado. Por favor, use otro.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detenemos el guardado
            }

            // Si esta editando un usuario existente
            if (esEdicion)
            {
                var index = lista.FindIndex(u => u.CorreoElectronico == usuarioSeleccionado.CorreoElectronico);
                if (index != -1)
                {
                    lista[index].Nombre = txtNombre.Text;
                    lista[index].CorreoElectronico = txtCorreoElec.Text;
                    lista[index].Password = txtPassword.Text;
                }
            }
            else
            {
                // Agregar un nuevo usuario si no esta editando
                Usuario nuevoUsuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreoElec.Text,
                    Password = txtPassword.Text
                };
                lista.Add(nuevoUsuario);
            }

            // Guardar los datos actualizados en el json
            usuario.GuardarJson(lista);

            MessageBox.Show("Formulario enviado con éxito");
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
