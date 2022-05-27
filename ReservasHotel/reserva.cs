using System;
using System.Collections.Generic;
using System.Text;

namespace ReservasHotel
{
    public class reserva
    {

        public DateTime fecha_reserva { get; set; }
        public DateTime fecha_finalizacion { get; set; }
        public string param { get; set; }

        public int id_usuario { get; set; }

        public string imgHabitacion { get; set;  }

        public int tbl { get; set; }
        public int id_habitacion { get; set; }
        public int id_tarjeta { get; set; }

        public string observacionReserva { get; set; }

        public string imagen { get; set; }

        public string observacionesHabitacion { get; set; }

        public string numero { get; set; }
        public string banco { get; set; }

        public string tipo_tarjeta { get; set; }

        public string descripcion { get; set; }


        public string capacidad { get; set; }

        public string precio { get; set; }

        public string nroHabitacion { get; set; }

        public string nroPiso { get; set; }

        public string correo { get; set; }

        public string enviocorreo { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }


    }
}
