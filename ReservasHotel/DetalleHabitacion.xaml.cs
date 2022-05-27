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
    public partial class DetalleHabitacion : ContentPage
    {
        Habitacion hb = new Habitacion();
        ObservableCollection<TarjetaCredito> _post;
        List<usuario> usuarioLogueado;

        public DetalleHabitacion(Habitacion obj)
        {
            InitializeComponent();

            datePickerEnd.MinimumDate= DateTime.Now;


            var usuarioStorage = Preferences.Get("usuario", "default_value");
            usuarioLogueado = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);

            hb = obj;
            NombreHabitacion.Text = obj.descripcion;
            ImgHabitacion.Source = obj.imagen;
            numeroHabitacion.Text = obj.nroHabitacion.ToString();
            Numpiso.Text = obj.nroPiso.ToString();
            precio.Text = obj.precio.ToString();
            capacidad.Text = obj.capacidad.ToString();
            

            ObtenerTarjetas();

        }

        public async void ObtenerTarjetas()
        {
            var servicio = new ServiciosWeb<TarjetaCredito>();
            TarjetaCredito TC = new TarjetaCredito
            {
                param = "3:"+usuarioLogueado[0].id_usuario,
                tbl = 4
            };
            string retorno = await servicio.ConsumoApi(TC, HttpMethod.Get, "Get");
            if (retorno != "0")
            {
                List<TarjetaCredito> posts = JsonConvert.DeserializeObject<List<TarjetaCredito>>(retorno);
                _post = new ObservableCollection<TarjetaCredito>(posts);
                cmb.ItemsSource = _post;
            }
            else
            {
            }

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            DateTime date = datePickerInit.Date;
            var time = timePickerInit.Time;

            //2022-05-03 06:17:00

            var fecha_reserva = DateTime.Parse(date.ToString("d") + " " + time);


            DateTime dateEnd = datePickerEnd.Date;
            var timeEnd = timePickerEnd.Time;


            var fecha_final = DateTime.Parse(dateEnd.ToString("d") + " " + timeEnd);

            TarjetaCredito selectedItem = (TarjetaCredito)cmb.SelectedItem;

            if (selectedItem != null)
            {
                var usuarioStorage = Preferences.Get("usuario", "default_value");
                List<usuario> usuarioLogueado = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);
                var servicio = new ServiciosWeb<reserva>();
                reserva NuevaReserva = new reserva
                {
                    fecha_reserva = fecha_reserva,
                    fecha_finalizacion = fecha_final,
                    id_habitacion = hb.id_h,
                    id_tarjeta = Convert.ToInt32(selectedItem.id_tarjeta),
                    descripcion = NombreHabitacion.Text,
                    precio=precio.Text,
                    capacidad=capacidad.Text,
                    //param = "6:" + correo.Text,
                    tbl = 3,
                    id_usuario=usuarioLogueado[0].id_usuario,
                    correo =usuarioLogueado[0].correo,
                    Nombre = usuarioLogueado[0].Nombre,
                    Apellido = usuarioLogueado[0].Apellido,
                    enviocorreo ="1"

                };
                string retorno = await servicio.ConsumoApi(NuevaReserva, HttpMethod.Post, "ValidaFechas");
                if (retorno != "existe")
                {

                    DisplayAlert("Listo", "Reserva realizada con éxito", "ok");

                }
                else
                {
                    DisplayAlert("Atención", "No es posible reservar en el periodo establecido.", "ok");


                }
            }
            else
            {
                DisplayAlert("Atención", "No tienes una tarjeta agregada,Agrega una Por favor !!", "ok");

            }

        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            TarjetaCredito selectedItem = (TarjetaCredito)cmb.SelectedItem;




        }
    }
}