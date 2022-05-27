using System;
using System.Collections.Generic;
using System.Text;

namespace ReservasHotel
{
   public  class Habitacion
    {
        public int id_h { get; set; }
        public int id_tipo { get; set; }
        public string imagen  {get; set; }



        public string observaciones { get; set; }

        public int nroHabitacion { get; set; }

        public int nroPiso { get; set; }

        public string descripcion { get; set; }
        public int capacidad { get; set; }

        public int precio { get; set; }


        public string tbls { get; set; }

        public string param { get; set; }

    }
}
