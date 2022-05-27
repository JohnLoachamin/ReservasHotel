using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class perfil : ContentPage
    {
        List<usuario> posts;
        string base64;
        MediaFile photo;
        public perfil()
        {
            InitializeComponent();
            

            var usuarioStorage = Preferences.Get("usuario", "default_value");
            try
            {
                posts = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);
                //  string fecha = posts[0].fecha_nacimiento.ToString();
                Photo.Source = posts[0].imagen+"?x=10000000";
                nombreC.Text = posts[0].Nombre;
                cedula.Text = posts[0].cedula;
                correo.Text = posts[0].correo;
                apellidoC.Text = posts[0].Apellido;
                fechaNacimiento.Text = posts[0].fecha_nacimiento.ToString().Split()[0];
            }
            catch (Exception ex)
            {

            }
        }

        void EntryCompleted(object sender, EventArgs e)
        {
            DisplayAlert("","sss","ok");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (BtnAccion.Text != "Editar")
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (BtnAccion.Text == "Editar")
            {

                BtnAccion.Text = "ENVIAR DATOS";
                nombreC.IsVisible = false;
                nombreEdit.IsVisible = true;
                nombreEdit.Text = nombreC.Text;
                cedula.IsVisible = false;
                cedulaEdit.IsVisible = true;
                cedulaEdit.Text = cedula.Text;
                correo.IsVisible = false;
                correoEdit.IsVisible = true;
                correoEdit.Text = correo.Text;
                apellidoC.IsVisible = false;
                apellidoEdit.IsVisible = true;
                apellidoEdit.Text = apellidoC.Text;

                fechaNacimiento.IsVisible = false;
                datePicker.IsVisible = true;
                datePicker.Date = DateTime.Parse(fechaNacimiento.Text);
                contenedor.BackgroundColor = Color.Transparent;


            }
            else
            {
                // BtnAccion.Text = "EDITAR";
                Loading.IsRunning = true;
                mensaje.IsVisible = true;
                BtnAccion.IsEnabled = false;
                DateTime date = datePicker.Date;
                var servicio = new ServiciosWeb<usuario>();
                usuario user = new usuario
                {
                    Nombre = nombreEdit.Text,
                    cedula = cedulaEdit.Text,
                    correo = correoEdit.Text,
                    imagen = base64,
                    id_usuario = posts[0].id_usuario,
                    param = "0:" + posts[0].id_usuario,
                    tbl = 6,
                    fecha_nacimiento = date,
                    Apellido = apellidoEdit.Text

                };

                string retorno = await servicio.ConsumoApi(user, HttpMethod.Put, "Update");
                if (retorno == "1")
                {

                    var usuarioStorage = Preferences.Get("usuario", "default_value");
                    List<usuario> usuarioLogueado = JsonConvert.DeserializeObject<List<usuario>>(usuarioStorage);

                    var servicioResuqest = new ServiciosWeb<usuario>();
                    usuario userUpdated = new usuario
                    {
                        param = "0:" + usuarioLogueado[0].id_usuario,
                        tbl = 6
                    };
                    string retornoUpdate = await servicioResuqest.ConsumoApi(userUpdated, HttpMethod.Get, "Get");
                    if (retorno != "0")
                    {
                        Preferences.Remove("usuario");
                        /// DisplayAlert("", "Usuario existe", "ok");
                        //string json = JsonConvert.SerializeObject(retorno);
                        Preferences.Set("usuario", retornoUpdate);
                        DisplayAlert("Listo", "Datos actualizados con éxito", "ok");
                        Loading.IsRunning = false;
                        mensaje.IsVisible = false;
                        BtnAccion.IsEnabled = true;

                    }
                    else
                    {
                        DisplayAlert("Atención", "No se pudieron actualizar los datos", "ok");
                        Loading.IsRunning = false;
                        mensaje.IsVisible = false;
                        BtnAccion.IsEnabled = true;
                    }

                        

                }
                else
                {
                    DisplayAlert("Atención", "No se pudieron actualizar los datos", "ok");
                    Loading.IsRunning = false;
                    mensaje.IsVisible = false;
                    BtnAccion.IsEnabled = true;


                }

            }


        }
    }
}