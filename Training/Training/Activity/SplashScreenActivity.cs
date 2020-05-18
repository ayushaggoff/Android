using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Training.Model;

namespace Training.Activity
{
    [Activity(Label = "SplashScreenActivity", MainLauncher=true, Theme="@style/Theme.Splash", NoHistory=true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
//SetContentView(Resource.Layout.LoginLayout);
            Thread.Sleep(40);
            //Start Activity1 Activity  
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string email = pref.GetString("Email", String.Empty);
            string password = pref.GetString("Password", String.Empty);

            if (email == String.Empty || password == String.Empty)
            {
                //No saved credentials, take user to login screen  
                Intent intent = new Intent(this, typeof(LoginActivity));
                this.StartActivity(intent);
            }
            else
            {
                //There are saved credentials  

                if (email == "ayush@gmail.com" && password == "123456")
                {
                    //Successful take the user to application  
                    Intent intent = new Intent(this, typeof(DashboardActivity));

                    User user = new User()
                    {
                        Email = "ayush@gmail.com",
                        Password = "123456"
                    };

                    intent.PutExtra("User", JsonConvert.SerializeObject(user));

                    this.StartActivity(intent);
                }

                else
                {
                    //Unsuccesful, take user to login screen  
                    Intent intent = new Intent(this, typeof(LoginActivity));
                    this.StartActivity(intent);
                    this.Finish();
                }
            }
        }
    }
}
