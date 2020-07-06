using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Training
{
    [BroadcastReceiver( Enabled = true)]
    public class BluetoothReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (mBluetoothAdapter == null)
            {
                Toast.MakeText(context, "Bluetooth is NOT Supported", ToastLength.Short).Show();
            }
            else if (!mBluetoothAdapter.IsEnabled)
            {
                Toast.MakeText(context, "Bluetooth is OFF", ToastLength.Short).Show();            
                    }
            else
            {
                Toast.MakeText(context, "Bluetooth is ON", ToastLength.Short).Show();
            }

        }
    }
}