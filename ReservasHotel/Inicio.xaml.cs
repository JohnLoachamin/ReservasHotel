using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            Habitaciones();
        }

        ObservableCollection<Habitacion> _post;
        public async void Habitaciones()
        {


            var servicio = new ServiciosWeb<Habitacion>();
            Habitacion user = new Habitacion
            {
                tbls = "5,1"
            };

            string retorno = await servicio.ConsumoApi(user, HttpMethod.Get, "GetJoin");
            if (retorno != "0")
            {
                List<Habitacion> posts = JsonConvert.DeserializeObject<List<Habitacion>>(retorno);
                _post = new ObservableCollection<Habitacion>(posts);
                foreach (Habitacion H in _post)
                {


                    // H.foto = "data:image/jpg;base64" + H.foto;
                    /* ImageSource image =null;
                     byte[] bytes = Convert.FromBase64String(H.foto.ToString());
                     image = ImageSource.FromStream(() => new MemoryStream(bytes));
                     H.foto = image.ToString();*/



                }


                MyListView.ItemsSource = _post;

                //string base64Image = _post[0].foto;
                //byte[] Base64Stream = Convert.FromBase64String(base64Image);
                //imageS.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));




            }
            else
            {


            }
        }


        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            Habitacion obj = (Habitacion)MyListView.SelectedItem;
            await Navigation.PushAsync(new DetalleHabitacion(obj));
        }
    }
}