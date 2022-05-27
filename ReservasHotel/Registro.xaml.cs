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

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        MediaFile photo;
        string base64 = "";
        public Registro()
        {
            InitializeComponent();

            mensaje.IsVisible = false;


        }

        public void LimpiarCampos()
        {

            foreach (View view in MyForm.Children)
            {

                if (view is Entry)
                {
                    (view as Entry).Text = String.Empty;
                }
            }

        }

        private async void RegistraUsuario(object sender, EventArgs e)
        {
            Loading.IsRunning = true;
            mensaje.IsVisible = true;
            DateTime date = datePicker.Date;
            resgistra.IsEnabled = false;
            var servicio = new ServiciosWeb<usuario>();
            usuario user = new usuario
            {
                Nombre = nombre.Text,
                Apellido = apellido.Text,
                contrasenia = contrasenia.Text,
                correo = correo.Text,
                param = "6:" + correo.Text,
                tbl = 6,
                fecha_nacimiento = date,
                cedula = cedula.Text,
                imagen = base64,
                tipo = "0",
                activo = 1
            };

            if (!string.IsNullOrWhiteSpace(nombre.Text) && !string.IsNullOrWhiteSpace(apellido.Text) &&
                !string.IsNullOrWhiteSpace(contrasenia.Text) && !string.IsNullOrWhiteSpace(correo.Text) &&
               !string.IsNullOrWhiteSpace(cedula.Text) && base64 != "")
            {


                string retorno = await servicio.ConsumoApi(user, HttpMethod.Post, "Create");
                if (retorno != "existe")
                {
                    LimpiarCampos();
                    Photo.Source = "user.png";
                    DisplayAlert("Listo", "Usuario creado con éxito", "ok");
                    Loading.IsRunning = false;
                    mensaje.IsVisible = false;
                    resgistra.IsEnabled = true;
                }
                else
                {
                    DisplayAlert("Atención", "Verifica si tu usuario ya fue creado", "ok");
                    Loading.IsRunning = false;
                    mensaje.IsVisible = false;
                    resgistra.IsEnabled = true;

                }

            }
            else
            {

                DisplayAlert("Atención", "Ingresa todos los campos", "ok");
                Loading.IsRunning = false;
                mensaje.IsVisible = false;
                resgistra.IsEnabled = true;

            }



        }

        private async void CargaImagen(object sender, EventArgs e)
        {

            var action = await DisplayActionSheet("Como desear subir tu foto?", "Cancel", null, "Galeria", "Tomar foto");




            switch (action)
            {
                case "Galeria":
                    photo = await CrossMedia.Current.PickPhotoAsync();
                    break;
                case "Tomar foto":
                    photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                    break;
            }



            if (photo != null)
            {
                Photo.Source = ImageSource.FromStream(() =>
                {
                    return photo.GetStream();

                });
                var stream = photo.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                base64 = System.Convert.ToBase64String(bytes);

            }

        }


    }
}