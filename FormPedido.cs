using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    public partial class FormPedido : Form
    {
        private Plato plato = new Plato();
        private Pedido pedido = new Pedido();
        private List<Plato> listaPlatosDisponibles = new List<Plato>();
        private List<Plato> platosSeleccionados = new List<Plato>();
        private Pedido pedidoEditar = null;
        private bool esEdicion = false;

        // Constructor para nuevo pedido
        public FormPedido()
        {
            InitializeComponent();
            InicializarFormulario();
            CargarPlatosDisponibles();
        }

        // Constructor para editar pedido existente
        public FormPedido(Pedido pedidoExistente)
        {
            InitializeComponent();
            this.pedidoEditar = pedidoExistente;
            this.esEdicion = true;
            InicializarFormulario();
            CargarPlatosDisponibles();
            CargarDatosPedido();
        }

        private void InicializarFormulario()
        {
            // Configurar NumericUpDown para cantidad
            numCantidad.Minimum = 1;
            numCantidad.Maximum = 99;
            numCantidad.Value = 1;

            // Limpiar label total
            lblTotal.Text = "Total: $0.00";
        }

        private void CargarPlatosDisponibles()
        {
            // Cargar todos los platos disponibles
            listaPlatosDisponibles = plato.ReadDataFromJson()
                .Where(p => p.Disponible)
                .ToList();

            lstPlatosDisponibles.Items.Clear();

            foreach (var platoItem in listaPlatosDisponibles)
            {
                lstPlatosDisponibles.Items.Add($"{platoItem.Nombre} - ${platoItem.Precio:F2} ({platoItem.Categoria})");
            }
        }

        private void CargarDatosPedido()
        {
            if (pedidoEditar == null) return;

            // Cargar datos básicos
            txtNumeroMesa.Text = pedidoEditar.NumeroMesa.ToString();
            txtCliente.Text = pedidoEditar.NombreCliente;
            txtObservaciones.Text = pedidoEditar.Observaciones;

            // Cargar platos seleccionados
            platosSeleccionados = new List<Plato>(pedidoEditar.Platos);
            ActualizarListaPlatosSeleccionados();
            CalcularTotal();
        }

        private void ActualizarListaPlatosSeleccionados()
        {
            lstPlatosSeleccionados.Items.Clear();

            foreach (var platoItem in platosSeleccionados)
            {
                lstPlatosSeleccionados.Items.Add($"{platoItem.Cantidad}x {platoItem.Nombre} - ${(platoItem.Precio * platoItem.Cantidad):F2}");
            }
        }

        private void CalcularTotal()
        {
            double total = 0;

            foreach (var platoItem in platosSeleccionados)
            {
                total += platoItem.Precio * platoItem.Cantidad;
            }

            lblTotal.Text = $"Total: ${total:F2}";
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroMesa.Text))
            {
                MessageBox.Show("El número de mesa es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroMesa.Focus();
                return false;
            }

            if (!int.TryParse(txtNumeroMesa.Text, out int numeroMesa) || numeroMesa <= 0)
            {
                MessageBox.Show("El número de mesa debe ser un número válido mayor a 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroMesa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show("El nombre del cliente es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCliente.Focus();
                return false;
            }

            if (platosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un plato al pedido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar que haya un plato seleccionado
            if (lstPlatosDisponibles.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un plato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el plato seleccionado
            int index = lstPlatosDisponibles.SelectedIndex;
            Plato platoSeleccionado = listaPlatosDisponibles[index];

            // Verificar si el plato ya está en la lista
            var platoExistente = platosSeleccionados.FirstOrDefault(p => p.Id == platoSeleccionado.Id);

            if (platoExistente != null)
            {
                // Si ya existe, sumar la cantidad
                platoExistente.Cantidad += (int)numCantidad.Value;
            }
            else
            {
                // Si no existe, crear una copia y agregarla
                Plato nuevoPlatoSeleccionado = new Plato
                {
                    Id = platoSeleccionado.Id,
                    Nombre = platoSeleccionado.Nombre,
                    Categoria = platoSeleccionado.Categoria,
                    Precio = platoSeleccionado.Precio,
                    Descripcion = platoSeleccionado.Descripcion,
                    Disponible = platoSeleccionado.Disponible,
                    Cantidad = (int)numCantidad.Value
                };

                platosSeleccionados.Add(nuevoPlatoSeleccionado);
            }

            // Actualizar la lista visual y el total
            ActualizarListaPlatosSeleccionados();
            CalcularTotal();

            // Resetear cantidad
            numCantidad.Value = 1;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            List<Pedido> listaPedidos = pedido.ReadDataFromJson();

            if (esEdicion)
            {
                // Actualizar pedido existente
                var index = listaPedidos.FindIndex(p => p.Id == pedidoEditar.Id);
                if (index != -1)
                {
                    listaPedidos[index].NumeroMesa = int.Parse(txtNumeroMesa.Text);
                    listaPedidos[index].NombreCliente = txtCliente.Text.Trim();
                    listaPedidos[index].Observaciones = txtObservaciones.Text.Trim();
                    listaPedidos[index].Platos = new List<Plato>(platosSeleccionados);
                    listaPedidos[index].CalcularTotal();
                }
            }
            else
            {
                // Crear nuevo pedido
                Pedido nuevoPedido = new Pedido(
                    int.Parse(txtNumeroMesa.Text),
                    txtCliente.Text.Trim(),
                    new List<Plato>(platosSeleccionados),
                    txtObservaciones.Text.Trim()
                );

                listaPedidos.Add(nuevoPedido);
            }

            // Guardar en JSON
            pedido.GuardarJson(listaPedidos);

            MessageBox.Show(
                esEdicion ? "Pedido actualizado exitosamente" : "Pedido creado exitosamente",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnQuitar_Click_1(object sender, EventArgs e)
        {
            // Validar que haya un plato seleccionado en la lista de pedido
            if (lstPlatosSeleccionados.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un plato del pedido para quitar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el índice y quitar el plato
            int index = lstPlatosSeleccionados.SelectedIndex;
            platosSeleccionados.RemoveAt(index);

            // Actualizar lista y total
            ActualizarListaPlatosSeleccionados();
            CalcularTotal();
        }
    }
}
