
using SocialMediaAuthentication.ViewModels;
using Xamarin.Forms;

namespace SocialMediaAuthentication.Views
{
    public partial class SocialLoginPage : ContentPage
    {
        private SocialLoginPageViewModel sm;
        public SocialLoginPage()
        {
            InitializeComponent();
            sm =new  SocialLoginPageViewModel();
            BindingContext = sm;
        }
    }
}
