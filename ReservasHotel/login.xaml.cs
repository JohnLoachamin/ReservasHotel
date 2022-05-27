using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {

        public login()
        {
            InitializeComponent();
            mensaje.IsVisible = false;
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void BtnValidar(object sender, EventArgs e)
        {
            Loading.IsRunning = true;
            mensaje.IsVisible = true;



            var servicio = new ServiciosWeb<usuario>();
            usuario user = new usuario
            {
                param = "5:" + contrasenia.Text + ",6:" + correo.Text,
                tbl = 6
            };



            if (!string.IsNullOrWhiteSpace(contrasenia.Text) && !string.IsNullOrWhiteSpace(correo.Text))
            {

                string retorno = await servicio.ConsumoApi(user, HttpMethod.Get, "Get");
                if (retorno != "0")
                {
                    /// DisplayAlert("", "Usuario existe", "ok");
                    //string json = JsonConvert.SerializeObject(retorno);
                    Preferences.Set("usuario", retorno);
                    Preferences.Set("inicioSesion", 1);
                    await Navigation.PushAsync(new MainPage());



                    Loading.IsRunning = false;
                    mensaje.IsVisible = false;
                }
                else
                {
                    DisplayAlert("", "Usuario no existe", "ok");
                    Preferences.Set("inicioSesion", 0);
                    Loading.IsRunning = false;
                    mensaje.IsVisible = false;

                }
            }
            else
            {
                DisplayAlert("Atención", "Debes ingresar tus datos", "ok");
                Loading.IsRunning = false;
                mensaje.IsVisible = false;
            }

        }

        private async void BtnIrAregistrar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }



    }
}