using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ReservasHotel
{
    interface gun<M>
    {

    }
    class ServiciosWeb<R>
    {
        public string Url { get; set; }
        HttpClient client { get; set; }
        R obj { get; set; }

        public ServiciosWeb()
        {
            //Url = "http://reservahotel.epizy.com/api/servicios/";
            // Url = "http://apihotel.onlinewebshop.net/api/servicios/";
            // Url = "https://reservahotelapi.000webhostapp.com/api/servicios/";
            //Url= "http://jasc.hyperphp.com/api/servicios/";
            Url = "https://cocotog.com/sisreha/api/index.php/servicios/";
            this.client = new HttpClient();
        }

        public async Task<string> ConsumoApi(R obj, HttpMethod metodo,string accion)
        {
            var profiles = Connectivity.ConnectionProfiles;
            //

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                var request = new HttpRequestMessage
                {
                    Method = metodo,
                    RequestUri = new Uri(Url + accion),
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                var respuesta = await response.Content.ReadAsStringAsync();
                return respuesta;
            }

            else
            {
                return "noInternet";
            }
        }
    }
}
