using Newtonsoft.Json;
using Plugin.FacebookClient;
using SocialMediaAuthentication.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ReservasHotel
{
    public partial class MainPage : MasterDetailPage
    {
    
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);



            this.Master = new Master();
            this.Detail = new NavigationPage(new Detail());
            App.MasterDet = this;


         
        }

    

      


    
       
    }
}
