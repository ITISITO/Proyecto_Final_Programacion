using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_pedidos_restaurante_PF
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string archivoSesion = "sesion.json";

            // Si existe una sesión activa, abrir PanelAdmin
            if (File.Exists(archivoSesion))
            {
                string json = File.ReadAllText(archivoSesion);
                var sesion = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (sesion != null && sesion.ContainsKey("UsuarioLogueado"))
                {
                    Application.Run(new FormUsuario());
                    return;
                }
            }

            // Si no hay sesión activa, abrir FormLogin
            Application.Run(new FormLogin());
        }
    }
}
