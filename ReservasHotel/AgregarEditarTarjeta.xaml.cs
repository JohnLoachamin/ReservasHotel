using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class AgregarEditarTarjeta : ContentPage
    {
        List<CodTarjeta> tc;
        int id_binCapturado;
        public AgregarEditarTarjeta(TarjetaCredito elemento = null)
        {
            InitializeComponent();
            
            if (elemento != null)
            {
                numeroTarjeta.Text = elemento.numero;
                banco.Text = elemento.banco.ToString();
                tipo.Text = elemento.tipo_tarjeta;
                codSeguridad.Text = elemento.cod_seguridad.ToString();
                logo.Source = elemento.imagen;
                AgregarTarjeta.IsVisible = false;
                ActualizarTarjeta.IsVisible = true;
            }
        }

        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numeroTarjeta.Text.Length == 6)
            {
                var servicio = new ServiciosWeb<TarjetaCredito>();
                TarjetaCredito TC = new TarjetaCredito
                {
                    param = "1:" + numeroTarjeta.Text,
                    tbl = 0
                };
                string retorno = await servicio.ConsumoApi(TC, HttpMethod.Get, "Get");
                if (retorno != "0")
                {
                    tc = JsonConvert.DeserializeObject<List<CodTarjeta>>(retorno);
                    banco.Text = tc[0].banco;
                    tipo.Text = tc[0].tipo_tarjeta;
                    id_binCapturado = tc[0].id_bin;
                    logo.Source = tc[0].imagen;
                }
                else
                {

                }

            }
            else if (numeroTarjeta.Text.Length < 6)
            {
                banco.Text = null;
                tipo.Text = null;
            }
            if (numeroTarjeta.Text.Length == 10)
            {
                codSeguridad.Focus();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var usuarioStorage = Preferences.Get("usuario", "default_value");
            List<usuario> usuarioLogueado = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);
            var servicio = new ServiciosWeb<TarjetaCredito>();
            TarjetaCredito NewTc = new TarjetaCredito
            {
                numero = numeroTarjeta.Text,
                cod_seguridad = Convert.ToInt32(codSeguridad.Text),
                id_bin = tc[0].id_bin,
                tbl = 4,
                param = "1:" + numeroTarjeta.Text,
                id_usuario=usuarioLogueado[0].id_usuario
                
            };

            string retorno = await servicio.ConsumoApi(NewTc, HttpMethod.Get, "Create");
            if (retorno != "0" && retorno != "existe")
            {
                DisplayAlert("Listo", "Tarjeta Agregada con éxito", "ok");
            }
            else if (retorno == "existe")
            {
                DisplayAlert("Atención", "Ya has agregado la tarjeta anteriormente", "ok");
            }

            else
            {
                DisplayAlert("Atención", "No pudo ser agregada tu tarjeta", "ok");
            }
        }


    

        private async void ActualizarTarjeta_Clicked(object sender, EventArgs e)
        {


            var servicio = new ServiciosWeb<TarjetaCredito>();
            TarjetaCredito TC = new TarjetaCredito
            {
                tbl = 4,
                numero = numeroTarjeta.Text,
                cod_seguridad = Convert.ToInt32(codSeguridad.Text),
                id_bin = id_binCapturado
            };
            string retorno = await servicio.ConsumoApi(TC, HttpMethod.Put, "Update");
            if (retorno == "1")
            {

                DisplayAlert("Listo", "Datos actualizado con éxito", "ok");
               
            }
            else
            {
                DisplayAlert("Atención", "No se pudieron actualizar los datos", "ok");


            }



        }
    }
}