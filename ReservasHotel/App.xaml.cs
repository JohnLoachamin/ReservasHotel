using SocialMediaAuthentication.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservasHotel
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }
        public App()
        {

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                var profiles = Connectivity.ConnectionProfiles;
                VerificaInternet(profiles);
                Task.Run(async () =>
                {
                    // CoinList = await RestService.GetCoins();

                });
                return true;
            });

        }


        private async void VerificaInternet(IEnumerable<ConnectionProfile> profiles)
        {
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                // Active Wi-Fi connection.
            }
            else
            {
                
                //DisplayAlert("","no existe red","ok");
                DependencyService.Get<Mensaje>().ShortAlert("No hay internet");
            }
        }

        protected override void OnStart()
        {
            try
            {

               var sesion = Preferences.Get("inicioSesion", 0);

                if (sesion == 0)
                {
                    MainPage = new NavigationPage(new login());
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                   
                }
           
            }
            catch (Exception ex)
            {


            }

            //DisplayAlert("kkk", myValue, "ssss");
            
             
             
       
             
             
             
             



        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
