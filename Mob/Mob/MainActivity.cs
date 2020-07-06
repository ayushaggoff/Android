using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Gms.Vision.Faces;
using Android.Gms.Vision;
using Android.Util;
using Android.Graphics.Drawables;

namespace Mob
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MyBroadcastReceiver broadcastReceiver;
        ImageView imageView;
        Button btnProcess;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            broadcastReceiver = new MyBroadcastReceiver();

            imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            btnProcess = FindViewById<Button>(Resource.Id.btnProcess);
            
           
        }
        protected override void OnStart()
        {
            base.OnStart();
            IntentFilter intentFilter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");
            RegisterReceiver(broadcastReceiver, intentFilter);
        }
        protected override void OnStop()
        {
            base.OnStop();
        //  UnregisterReceiver(broadcastReceiver);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(broadcastReceiver);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}