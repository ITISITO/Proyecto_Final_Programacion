using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_pedidos_restaurante_PF
{
    public class Usuario
    {
        private string archivo = "usuarios.json";

        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }


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
                return JsonConvert.DeserializeObject<List<Usuario>>(datosJson);
            }

            return new List<Usuario>();
        }
    }
}
