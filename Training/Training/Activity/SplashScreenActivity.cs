using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android;
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
    [Activity(Label = "SplashScreenActivity", Theme="@style/Theme.Splash", NoHistory=true)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
                        await TryToGetPermission();

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


        #region permisson
        async Task TryToGetPermission()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermission();
                return;
            }
        }

        const int RequestLocationId = 0;

        readonly string[] PermissionGroupLocation =
       {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        async Task GetPermission()
        {
            string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                Toast.MakeText(this, "Loaction Permission Granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("The application need special permissions to continue");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionGroupLocation, RequestLocationId);
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionGroupLocation, RequestLocationId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();

                        }
                        else
                        {

                            Toast.MakeText(this, "Special permissions denied", ToastLength.Short).Show();

                        }
                    }
                    break;
            }

        }

        #endregion


    }
}
