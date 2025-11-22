using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sistema_de_pedidos_restaurante_PF
{
    public class Plato
    {
        private string archivo = "platos.json";

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public bool Disponible { get; set; }

        public Plato()
        {
            Cantidad = 1;
            Disponible = true;
        }

        public Plato(string nombre, string categoria, double precio, string descripcion = "")
        {
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
            var listaExistente = ReadDataFromJson(); // ⬅️ Lee los existentes

            var listaPlatos = new List<Plato>
    {
        // Platos Típicos Colombianos
        new Plato("Bandeja Paisa", "Plato Fuerte", 28000, "Carne, chicharrón, chorizo, arepa, arroz, fríjoles y huevo"),
        new Plato("Ajiaco Santafereño", "Plato Fuerte", 22000, "Sopa con pollo, papas, mazorca y guascas"),
        new Plato("Sancocho de Gallina", "Plato Fuerte", 20000, "Sopa con gallina criolla, yuca, plátano y papa"),
        new Plato("Arroz con Pollo", "Plato Fuerte", 18000, "Arroz amarillo con pollo y verduras"),
        new Plato("Lechona Tolimense", "Plato Fuerte", 25000, "Cerdo relleno con arroz y arveja"),
        new Plato("Tamal", "Entrada", 12000, "Tamal de cerdo y pollo envuelto en hoja de plátano"),
        new Plato("Empanadas", "Entrada", 8000, "3 empanadas de carne o pollo con ají"),
        new Plato("Arepa con Queso", "Entrada", 6000, "Arepa de maíz con queso costeño"),
        new Plato("Patacones", "Entrada", 7000, "Plátano verde frito con hogao"),
        
        // Comidas Rápidas
        new Plato("Hamburguesa Especial", "Comida Rápida", 15000, "Carne, queso, tocineta, lechuga y salsas"),
        new Plato("Perro Caliente", "Comida Rápida", 10000, "Salchicha con papas chip, salsas y queso"),
        new Plato("Salchipapas", "Comida Rápida", 12000, "Papas fritas con salchicha y salsas"),
        new Plato("Pizza Personal", "Comida Rápida", 16000, "Pizza individual de jamón y queso"),
        new Plato("Alitas de Pollo", "Comida Rápida", 18000, "8 alitas BBQ o picantes"),
        
        // Bebidas
        new Plato("Jugo Natural", "Bebida", 5000, "Lulo, mora, maracuyá o mango"),
        new Plato("Limonada Natural", "Bebida", 4000, "Limonada con hielo"),
        new Plato("Agua en Botella", "Bebida", 3000, "Agua natural 500ml"),
        new Plato("Gaseosa", "Bebida", 3500, "Coca Cola, Colombiana o Postobón"),
        new Plato("Café Tinto", "Bebida", 2000, "Café colombiano"),
        new Plato("Chocolate Caliente", "Bebida", 4500, "Chocolate con queso"),
        
        // Postres
        new Plato("Tres Leches", "Postre", 8000, "Torta de tres leches"),
        new Plato("Arequipe con Brevas", "Postre", 6000, "Postre tradicional colombiano"),
        new Plato("Helado", "Postre", 5000, "Helado de crema o frutas"),
        new Plato("Obleas", "Postre", 4000, "Obleas con arequipe y queso")
    };

            // Solo agrega los que NO existen (comparando por Nombre)
            foreach (var plato in listaPlatos)
            {
                if (!listaExistente.Any(p => p.Nombre == plato.Nombre))
                {
                    // ⬇️⬇️⬇️ REASIGNAR el Id correcto basándose en listaExistente
                    plato.Id = listaExistente.Count > 0 ? listaExistente.Max(p => p.Id) + 1 : 1;
                    listaExistente.Add(plato);
                }
            }

            GuardarJson(listaExistente);
        }
    }
}