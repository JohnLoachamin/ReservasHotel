using System;
using System.Collections.Generic;
using System.Text;

namespace ReservasHotel
{
   public class usuario
    {
        public int tbl { get; set; }
        public int id_usuario { get; set; }
        public string param { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string contrasenia { get; set; }

        public string tipo { get; set; }

        public int activo { get; set; }

        public string correo { get; set; }
        public string cedula { get; set; }
      
        public string imagen { get; set; }
        public DateTime fecha_nacimiento { get; set; }

    }
}
