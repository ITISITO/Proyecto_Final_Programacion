using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormUsuario : Form
    {

        private Usuario usuario = new Usuario();

        private List<Usuario> lista = new List<Usuario>();

        public FormUsuario()
        {
            InitializeComponent();

            dataGridView1.DataSource = usuario.ReadDataFromJson();
            Console.WriteLine(usuario.ReadDataFromJson());

            CargarDataGrid();
            //CargarPeliculas();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegistro form1 = new FormRegistro();//Crear la instancia

            var result = form1.ShowDialog();//Para manipular un solo formulario
            if (result == DialogResult.OK)
            {
                CargarDataGrid();
            }

        }//Fin del button1_Click

        private void CargarDataGrid()
        {
            lista = usuario.ReadDataFromJson();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            if (!dataGridView1.Columns.Contains("Editar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                btnEditar.HeaderText = "Editar";
                btnEditar.Name = "btnEditar";
                btnEditar.Text = "Editar";
                btnEditar.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnEditar);

            }

            if (!dataGridView1.Columns.Contains("Eliminar")) // HOYYYYYYYYYYYYYYYY 1 OCTUBRE
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Name = "btnEliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnEliminar);

            }


        }//Fin CargarDataGrid

        private void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["btnEditar"].Index)
            {
                var usuarioSeleccionado = lista[e.RowIndex];
                FormRegistro form1 = new FormRegistro(usuarioSeleccionado);
                var result = form1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarDataGrid(); // Actualizar la visualización al editar
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEliminar") // Hoyyyyyy 
            {
                var usuarioSeleccionado = lista[e.RowIndex];

                DialogResult confirmar = MessageBox.Show($"Seguro que quieres eliminar al usuario {usuarioSeleccionado.Nombre}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    lista.Remove(usuarioSeleccionado);
                    usuario.GuardarJson(lista);
                    CargarDataGrid();
                }
            }
        }
    }
}
