using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using ReservasHotel.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(MensajeAndroid))]
namespace ReservasHotel.Droid
{
    class MensajeAndroid : Mensaje
    {
        [Obsolete]
        public void LongAlert(string mensaje)
        {
            Toast.MakeText(Application.Context, Html.FromHtml("<font color='#FF0000' ><b>" + mensaje + "</b></font>"), ToastLength.Long).Show();
        }

        [Obsolete]
        public void ShortAlert(string mensaje)
        {
            Toast.MakeText(Application.Context, Html.FromHtml("<font color='#FF0000' ><b>" + mensaje + "</b></font>"), ToastLength.Short).Show();
        }
    }
}