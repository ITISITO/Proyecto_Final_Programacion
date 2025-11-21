using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sistema_de_pedidos_restaurante_PF
{
    public class Plato
    {
        private string archivo = "platos.json";

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public bool Disponible { get; set; }

        public Plato()
        {
            Id = Guid.NewGuid().ToString();
            Cantidad = 1;
            Disponible = true;
        }

        public Plato(string nombre, string categoria, double precio, string descripcion = "")
        {
            Id = Guid.NewGuid().ToString();
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Descripcion = descripcion;
            Cantidad = 1;
            Disponible = true;
        }

        public void GuardarJson(List<Plato> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(archivo, json);
        }

        public List<Plato> ReadDataFromJson()
        {
            if (File.Exists(archivo))
            {
                string datosJson = File.ReadAllText(archivo);
                var lista = JsonConvert.DeserializeObject<List<Plato>>(datosJson);
                return lista ?? new List<Plato>();
            }
            return new List<Plato>();
        }

        public void CrearPlatosEjemplo()
        {
            var listaPlatos = new List<Plato>
            {
                new Plato("Hamburguesa Clásica", "Plato Fuerte", 8.50, "Hamburguesa con queso"),
                new Plato("Pizza Margarita", "Plato Fuerte", 12.00, "Pizza con tomate y queso"),
                new Plato("Ensalada César", "Entrada", 6.50, "Lechuga romana con pollo"),
                new Plato("Coca Cola", "Bebida", 2.50, "Refresco 500ml"),
                new Plato("Agua Mineral", "Bebida", 1.50, "Agua natural 500ml"),
                new Plato("Tiramisu", "Postre", 5.00, "Postre italiano"),
                new Plato("Helado", "Postre", 3.50, "Helado de vainilla"),
                new Plato("Pasta Carbonara", "Plato Fuerte", 10.00, "Pasta con salsa"),
                new Plato("Alitas de Pollo", "Entrada", 7.00, "Alitas BBQ"),
                new Plato("Limonada", "Bebida", 2.00, "Limonada natural")
            };

            GuardarJson(listaPlatos);
        }
    }
}