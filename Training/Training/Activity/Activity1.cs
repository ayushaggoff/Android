using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Training.Model;

namespace Training.Activity
{
    [Activity(Label = "Activity1", MainLauncher = true)]
  
    public class Activity1 : AppCompatActivity
    {
        Button Buttonbtnsendmessage;
        BroadcastReceiver broadcastReceiver;
        IntentFilter intentfilter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);


            //SetContentView(Resource.Layout.BroadcastTry);
            //Buttonbtnsendmessage = (Button)FindViewById(Resource.Id.button1);
            //myreceiver = new MyBroadcastReceiver();
            //intentfilter = new IntentFilter("MY_SPECIFIC_ACTION");
            //Buttonbtnsendmessage.Click += (sender, e) =>
            //{
            //    Intent i = new Intent("MY_SPECIFIC_ACTION");
            //    i.PutExtra("key", "some value from intent");
            //    //SendBroadcast(i);
            //    SendOrderedBroadcast(i, null);

            //};
        }

        protected override void OnStart()
        {
            base.OnStart();

            IntentFilter intentFilter = new IntentFilter("android.bluetooth.adapter.action.STATE_CHANGED");

            intentFilter.AddAction("android.bluetooth.adapter.action.STATE_CHANGED");
            intentFilter.AddAction("android.bluetooth.adapter.action.CONNECTION_STATE_CHANGED");

            intentFilter.AddAction("android.bluetooth.adapter.action.ACL_CONNECTED");

            intentFilter.AddAction("android.bluetooth.adapter.action.ACL_DISCONNECTED");

            RegisterReceiver(broadcastReceiver, intentFilter);
            StartService(new Intent(this,typeof(TryService)));
        }
        protected override void OnResume()
        {
            base.OnResume();

          //  RegisterReceiver(myreceiver, intentfilter);
        }
        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterReceiver(myreceiver);
        }

    }
}    