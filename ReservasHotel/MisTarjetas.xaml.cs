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
    public partial class MisTarjetas : ContentPage
    {

        ObservableCollection<TarjetaCredito> _post;
        public MisTarjetas()
        {
            InitializeComponent();
            ObtenerMisTarjetas();
        }


        public async void ObtenerMisTarjetas()
        {
            var usuarioStorage = Preferences.Get("usuario", "default_value");
            List<usuario> usuarioLogueado = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);
            var servicio = new ServiciosWeb<TarjetaCredito>();
            TarjetaCredito user = new TarjetaCredito
            {
                tbls = "0,4",
                param="3:"+usuarioLogueado[0].id_usuario
            };
            string retorno = await servicio.ConsumoApi(user, HttpMethod.Get, "GetJoin");
            if (retorno != "0")
            {

                List<TarjetaCredito> posts = JsonConvert.DeserializeObject<List<TarjetaCredito>>(retorno);
                _post = new ObservableCollection<TarjetaCredito>(posts);
                ListaTarjetas.ItemsSource = _post;

            }
        }

        private async void ListaTarjetas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var action = await DisplayActionSheet("Que acción deseas realizar?", "Cancel", null, "Editar", "Eliminar");
            TarjetaCredito obj = new TarjetaCredito();
            obj = (TarjetaCredito)ListaTarjetas.SelectedItem;
            switch (action)
            {
                case "Editar":
                    await Navigation.PushAsync(new AgregarEditarTarjeta(obj));
                    break;
                case "Eliminar":
                    var servicio = new ServiciosWeb<TarjetaCredito>();
                    TarjetaCredito TC = new TarjetaCredito
                    {
                        tbl = 4,
                        param="0:"+obj.id_tarjeta


                    };
                    string retorno = await servicio.ConsumoApi(TC, HttpMethod.Delete, "Delete");
                    if (retorno != "existe")
                    {
                        DisplayAlert("Listo", "Tarjeta eliminada con éxito", "ok");
                        ObtenerMisTarjetas();
                    }
                    else
                    {
                        DisplayAlert("Atención", "No se pudo eliminar tarjeta", "ok");
                    }
                    break;
            }
        }
    }
}