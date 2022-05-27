using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservasHotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
        }

        private async void bntPerfil_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new perfil());
        }

        private async void bntTarjetas_Clicked(object sender, EventArgs e)
        {

            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new MisTarjetas());

        }

        private async void bntCerrarSesion_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("inicioSesion");
            Preferences.Remove("usuario");
           
            await Navigation.PushAsync(new login());
        }

        private async void bntAcercade_Clicked(object sender, EventArgs e)
        {

            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new AcercaDe());

        }

    
        private async void bntReservas_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Reservas());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new MainPage());
        }

        private async void bntNTarjetas_Clicked(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new AgregarEditarTarjeta ());
        }
    }
}