using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormGestionPlatos : Form
    {
        private Plato plato = new Plato();
        private List<Plato> listaPlatos = new List<Plato>();
        private Plato platoSeleccionado = null;

        public FormGestionPlatos()
        {
            InitializeComponent();
            CargarCategorias();
            ConfigurarDataGridView();
            CargarListaPlatos();
            LimpiarCampos();
        }

        private void CargarCategorias()
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.AddRange(new object[]
            {
                "Entrada",
                "Plato Fuerte",
                "Postre",
                "Bebida"
            });
        }

        private void ConfigurarDataGridView()
        {
            // Configurar el DataGridView
            dgvPlatos.AutoGenerateColumns = false;
            dgvPlatos.AllowUserToAddRows = false;
            dgvPlatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlatos.MultiSelect = false;
            dgvPlatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Limpiar columnas existentes
            dgvPlatos.Columns.Clear();

            // Agregar columnas manualmente
            dgvPlatos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Name = "Nombre"
            });

            dgvPlatos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Categoría",
                DataPropertyName = "Categoria",
                Name = "Categoria"
            });

            dgvPlatos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Name = "Precio",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } // Formato moneda
            });

            dgvPlatos.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Disponible",
                DataPropertyName = "Disponible",
                Name = "Disponible"
            });

            dgvPlatos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Name = "Descripcion"
            });
        }

        private void CargarListaPlatos()
        {
            listaPlatos = plato.ReadDataFromJson();
            dgvPlatos.DataSource = null;
            dgvPlatos.DataSource = listaPlatos;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtPrecio.Clear();
            txtDescripcion.Clear();
            chkDisponible.Checked = true;
            platoSeleccionado = null;

            btnAgregar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del plato es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoría", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El precio es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (!double.TryParse(txtPrecio.Text, out double precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número mayor a 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            return true;
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            Plato nuevoPlato = new Plato
            {
                Nombre = txtNombre.Text.Trim(),
                Categoria = cmbCategoria.SelectedItem.ToString(),
                Precio = double.Parse(txtPrecio.Text),
                Descripcion = txtDescripcion.Text.Trim(),
                Disponible = chkDisponible.Checked
            };

            // ⬇️⬇️⬇️ AGREGAR: Asignar ID automáticamente
            nuevoPlato.Id = listaPlatos.Count > 0 ? listaPlatos.Max(p => p.Id) + 1 : 1;

            listaPlatos.Add(nuevoPlato);
            plato.GuardarJson(listaPlatos);

            MessageBox.Show("Plato agregado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarListaPlatos();
            LimpiarCampos();
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (platoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un plato para actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarFormulario())
                return;

            var index = listaPlatos.FindIndex(p => p.Id == platoSeleccionado.Id);
            if (index != -1)
            {
                listaPlatos[index].Nombre = txtNombre.Text.Trim();
                listaPlatos[index].Categoria = cmbCategoria.SelectedItem.ToString();
                listaPlatos[index].Precio = double.Parse(txtPrecio.Text);
                listaPlatos[index].Descripcion = txtDescripcion.Text.Trim();
                listaPlatos[index].Disponible = chkDisponible.Checked;

                plato.GuardarJson(listaPlatos);

                MessageBox.Show("Plato actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarListaPlatos();
                LimpiarCampos();
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (platoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un plato para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmar = MessageBox.Show(
                $"¿Seguro que deseas eliminar el plato '{platoSeleccionado.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar == DialogResult.Yes)
            {
                listaPlatos.RemoveAll(p => p.Id == platoSeleccionado.Id);
                plato.GuardarJson(listaPlatos);

                MessageBox.Show("Plato eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarListaPlatos();
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvPlatos_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvPlatos.SelectedRows.Count == 0)
                return;

            int index = dgvPlatos.SelectedRows[0].Index;
            platoSeleccionado = listaPlatos[index];

            txtNombre.Text = platoSeleccionado.Nombre;
            cmbCategoria.SelectedItem = platoSeleccionado.Categoria;
            txtPrecio.Text = platoSeleccionado.Precio.ToString();
            txtDescripcion.Text = platoSeleccionado.Descripcion;
            chkDisponible.Checked = platoSeleccionado.Disponible;

            btnAgregar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDisponible_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
