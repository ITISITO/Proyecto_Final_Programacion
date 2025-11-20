using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sistema_de_pedidos_restaurante_PF
{
    public class Usuario
    {
        private string archivo = "usuarios.json";

        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; } // ⬅️ AGREGAR ESTO

        // Constructor vacío
        public Usuario()
        {
        }

        // Constructor con parámetros
        public Usuario(string nombre, string correo, string password, string rol = "Mesero")
        {
            Nombre = nombre;
            CorreoElectronico = correo;
            Password = password;
            Rol = rol;
        }

        public void GuardarJson(List<Usuario> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(archivo, json);
        }

        public List<Usuario> ReadDataFromJson()
        {
            if (File.Exists(archivo))
            {
                string datosJson = File.ReadAllText(archivo);
                var lista = JsonConvert.DeserializeObject<List<Usuario>>(datosJson);
                return lista ?? new List<Usuario>();
            }
            return new List<Usuario>();
        }

        // ⬇️ AGREGAR ESTE MÉTODO
        public void CrearUsuariosEjemplo()
        {
            var listaUsuarios = new List<Usuario>
            {
                new Usuario("Administrador", "admin@restaurante.com", "admin123", "Administrador"),
                new Usuario("Juan Pérez", "juan@restaurante.com", "mesero123", "Mesero"),
                new Usuario("María García", "maria@restaurante.com", "cocinera123", "Cocinero")
            };

            GuardarJson(listaUsuarios);
        }
    }

    // ⬇️ AGREGAR ESTA CLASE COMPLETA
    public class SesionLogin
    {
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}