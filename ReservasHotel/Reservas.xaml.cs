using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservas : ContentPage
    {
        List<usuario> posts;
        ObservableCollection<reserva> _post;
        public Reservas()
        {
            InitializeComponent();
            ObtenerReservas();
        }


        public async void ObtenerReservas()
        {
            var usuarioStorage = Preferences.Get("usuario", "default_value");
            posts = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);

            var servicio = new ServiciosWeb<reserva>();
            reserva hotel = new reserva
            {
                param = posts[0].id_usuario.ToString()
                
            };

            string retorno = await servicio.ConsumoApi(hotel, HttpMethod.Get, "GetJoinMultiple");
            if (retorno != "0")
            {
                List<reserva> posts = JsonConvert.DeserializeObject<List<reserva>>(retorno);
                _post = new ObservableCollection<reserva>(posts);

                ListaReservas.ItemsSource = _post;



            }

        }
    }
}