using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcercaDe : ContentPage
    {
        public AcercaDe()
        {
            InitializeComponent();
            ObtenerInfoHotel();

        }


        public async void ObtenerInfoHotel()
        {
            var servicio = new ServiciosWeb<Hotel>();
            Hotel hotel = new Hotel
            {
                tbl = "2",
                attrs="*"
            };

            string retorno = await servicio.ConsumoApi(hotel, HttpMethod.Get, "Get");
            if (retorno != "0")
            {
                

                List<Hotel> posts = JsonConvert.DeserializeObject<List<Hotel>>(retorno);
                nombreHotel.Text = posts[0].nombre_hotel;
                ImagenHotel.Source = posts[0].logo;
                descripHotel.Text = posts[0].descripcion;
                dirHotel.Text = posts[0].direccion;
                contactHotel.Text = posts[0].contacto;

                //_post = new ObservableCollection<Habitacion>(posts);

            }

        }
    }
}