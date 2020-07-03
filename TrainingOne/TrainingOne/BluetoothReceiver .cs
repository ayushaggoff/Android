using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TrainingOne
{
    [BroadcastReceiver]
    public class BluetoothReceiver : BroadcastReceiver
    {
        BluetoothDevice device;
        MainActivity main = new MainActivity();
      

      public override void OnReceive(Context context, Intent intent)
      {
            if(Intent.ActionBootCompleted.Equals(intent.Action))
            {
                Toast.MakeText(context, "llllllllll Boot comp", ToastLength.Long).Show();
                Log.Debug("lol", "haaaaaaaaaaaaaaaaaaaa");
            }
            if (ConnectivityManager.ConnectivityAction.Equals(intent.Action))
            {
                Toast.MakeText(context, "Connectivity changed", ToastLength.Long).Show();
                Log.Debug("lol", "haaaaaaaaaaaaaaaaaaaa");
            }

            //string action = intent.Action;
            //Console.WriteLine("action is: " + action);
            //Log.Debug("lol","haaaaaaaaaaaaaaaaaaaa");
            //  Toast.MakeText(main, "Device is disconnected", ToastLength.Long).Show();

            //  Android.Bluetooth.BluetoothDevice device = (Android.Bluetooth.BluetoothDevice)intent.GetParcelableExtra("android.bluetooth.device.extra.DEVICE");
            //  Toast.MakeText(main, "Device is disconnected", ToastLength.Long).Show();

            //  Console.WriteLine("Value of extra DEVICE " + device);
            //Console.WriteLine("HAHAHAHAHA AAITTTTTTTTTTTTTTTTT " );
        }
    }
}