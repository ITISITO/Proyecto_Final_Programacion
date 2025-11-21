using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sistema_de_pedidos_restaurante_PF
{
    public class Pedido
    {
        private string archivo = "pedidos.json";

        public string Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NombreCliente { get; set; }
        public List<Plato> Platos { get; set; }
        public double Total { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Observaciones { get; set; }

        public Pedido()
        {
            Id = Guid.NewGuid().ToString();
            Platos = new List<Plato>();
            FechaPedido = DateTime.Now;
            Estado = "En Preparación";
        }

        public Pedido(int numeroMesa, string nombreCliente, List<Plato> platos, string observaciones = "")
        {
            Id = Guid.NewGuid().ToString();
            NumeroMesa = numeroMesa;
            NombreCliente = nombreCliente;
            Platos = platos ?? new List<Plato>();
            Observaciones = observaciones;
            FechaPedido = DateTime.Now;
            Estado = "En Preparación";
            CalcularTotal();
        }

        public void CalcularTotal()
        {
            Total = 0;
            foreach (var plato in Platos)
            {
                Total += plato.Precio * plato.Cantidad;
            }
        }

        public void GuardarJson(List<Pedido> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(archivo, json);
        }

        public List<Pedido> ReadDataFromJson()
        {
            if (File.Exists(archivo))
            {
                string datosJson = File.ReadAllText(archivo);
                var lista = JsonConvert.DeserializeObject<List<Pedido>>(datosJson);
                return lista ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }

        public string ObtenerResumenPlatos()
        {
            if (Platos == null || Platos.Count == 0)
                return "Sin platos";

            var resumen = new List<string>();
            foreach (var plato in Platos)
            {
                resumen.Add($"{plato.Cantidad}x {plato.Nombre}");
            }
            return string.Join(", ", resumen);
        }
    }
}