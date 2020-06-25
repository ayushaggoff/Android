using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Messaging;
using Android.Content;
using Android.Graphics;
using Firebase.Iid;
using Android.Util;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Xamarin.Facebook.AppLinks;
using Xamarin.Facebook.Login;
using Android.Gms.Common;
using Android.Content.PM;

namespace Training.Activity
{
    [Activity(Label = "DashboardActivity",LaunchMode =LaunchMode.SingleTask)]
    public class DashboardActivity : AppCompatActivity
    {
        public static int ResultCode = 999;

        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        TextView msgText;


        string email;
        Button btnAddProfile;
        Button btnFragment,btnLogout;
        Button btnTab;
        TextView TextView_Name, TextView_Email;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            SetContentView(Resource.Layout.DashboardLayout);
            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };

            var subscribeButton = FindViewById<Button>(Resource.Id.subscribeButton);
            subscribeButton.Click += delegate {
                FirebaseMessaging.Instance.SubscribeToTopic("news");
                Log.Debug(TAG, "Subscribed to remote notifications");
            };


            msgText = FindViewById<TextView>(Resource.Id.textView1);
             email = pref.GetString("Email", String.Empty);
          
            IsPlayServicesAvailable();
            CreateNotificationChannel();

            string password = pref.GetString("Password", String.Empty);
            string datafromlogin = Intent.GetStringExtra("Name");
            TextView_Name = FindViewById<TextView>(Resource.Id.textview_name);
            btnLogout = FindViewById<Button>(Resource.Id.button4);
      

            TextView_Name.Text = datafromlogin;
      
            TextView_Email = FindViewById<TextView>(Resource.Id.textView_Email);
            btnAddProfile = FindViewById<Button>(Resource.Id.button1);
            btnFragment = FindViewById<Button>(Resource.Id.button2);
            btnTab = FindViewById<Button>(Resource.Id.button3);
            TextView_Email.Text = datafromlogin;

            string noticationmessage = Intent.GetStringExtra("notification");

            if (noticationmessage != null&& noticationmessage !="")
            {
                Toast.MakeText(this, noticationmessage, ToastLength.Long).Show();

            }















        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnAddProfile.Click += (obj, e) =>
                 {
                     StartActivityForResult(typeof(AddProfileActivity), ResultCode);
                 };
            this.btnLogout.Click += (obj, e) =>
            {
                LoginManager.Instance.LogOut();
                FirebaseAuth.Instance.SignOut();
                Intent intent = new Intent(this,typeof(LoginActivity));      
            StartActivity(intent);
             Finish();
    };
            this.btnFragment.Click += this.BtnFragment_Click;
            this.btnTab.Click += this.BtnTab_Click;

        }
        protected override void OnPause()
        {
            base.OnPause();
            //this.btnAddProfile.Click -= this.BtnAddProfile_Click;
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            var context = Android.App.Application.Context;
            var tostMessage = "///////////////ssssssssssssssssssssss///////////";
            var durtion = ToastLength.Long;


            Toast.MakeText(context, tostMessage, durtion).Show();
        }

        #region Push Notification
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.High)
            {
                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
        #endregion
        private void BtnFragment_Click(object sender, System.EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(FragementBtnActivity));
            Intent intent = new Intent(this, typeof(LogListActivity));
            StartActivity(intent);
        }
        private void BtnAddProfile_Click(object sender, System.EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(AddProfileActivity));
            //StartActivity(intent);
            //   Intent intent = new Intent(this, typeof(AddProfileActivity));
            StartActivityForResult(typeof(AddProfileActivity), ResultCode);
            //StartActivityForResult(typeof(AddProfileActivity), 2);
        }

        private void BtnTab_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TabActivity));
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum]  Result resultCode, Intent data)
        {
            TextView_Name = FindViewById<TextView>(Resource.Id.textview_name);

            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == ResultCode)
            {
                //TextView_Name.Background = Color.Red;
                if (resultCode == Result.Ok)
                {
                //    String message = data.GetStringExtra("MESSAGE");
                    TextView_Name.Text = data.Data.ToString();
                }
                if (resultCode == Result.Canceled)
                {
                    
                    TextView_Name.Text = "Nothing is selected";
                }
            }
        }
    }
}