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
    public partial class PanelPedidos : Form
    {
        private SesionLogin sesionActiva;
        private Timer temporizador;
        private int tiempoRestante; // En segundos

        private Pedido pedido = new Pedido();
        private List<Pedido> ListaPedidos = new List<Pedido>();
        public PanelPedidos(SesionLogin sesion)
        {
            InitializeComponent();
        }
    }
}
