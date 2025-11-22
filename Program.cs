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

            Usuario usuarioEjemplo = new Usuario();
            usuarioEjemplo.CrearUsuariosEjemplo();

            Plato platoEjemplo = new Plato();
            if (platoEjemplo.ReadDataFromJson().Count == 0)
            {
                platoEjemplo.CrearPlatosEjemplo();
            }

            string archivoSesion = "sesion.json";

            if (File.Exists(archivoSesion))
            {
                string json = File.ReadAllText(archivoSesion);
                var sesion = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (sesion != null && sesion.ContainsKey("UsuarioLogueado"))
                {
                    // ⬇️⬇️⬇️ CAMBIAR ESTA LÍNEA
                    Application.Run(new PanelPedidos(new SesionLogin
                    {
                        Nombre = sesion["UsuarioLogueado"],
                        CorreoElectronico = sesion["CorreoElectronico"],
                        Rol = sesion["Rol"],
                        FechaInicio = DateTime.Parse(sesion["FechaInicio"])
                    }));
                    return;
                }
            }

            // Si no hay sesión activa, abrir FormLogin
            Application.Run(new FormLogin());
        }
    }
}
