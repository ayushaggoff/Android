using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;

namespace Training.Activity
{
    [Activity(Label = "TwitterActivity",MainLauncher =false)]
    public class TwitterActivity : AppCompatActivity
    {
        int count = 1;
        Button btn_twitter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TwitterLayout);
            // Create your application here
            btn_twitter = FindViewById<Button>(Resource.Id.loginButton1);
            btn_twitter.Click += Btn_twitter_Click;
        }

        private void Btn_twitter_Click(object sender, EventArgs e)
        {
            
            var auth = new OAuth1Authenticator(
                                consumerKey: "oT9PQ1bWS5opHVGolCbkDAUEX",
                                consumerSecret: "652Tmze5cpPZyomaW4TAud5zDvX45ZHaothgbfSje4I6CMUZKs",
                                requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                                authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                                accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                                callbackUrl: new Uri("http://mobile.twitter.com"));

  


    auth.Completed += twitter_auth_Completed;
            StartActivity(auth.GetUI(this));
        }

        private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                Toast.MakeText(this, "Logged-In", ToastLength.Short).Show();
                //Finish();
            }
        }
    }
}