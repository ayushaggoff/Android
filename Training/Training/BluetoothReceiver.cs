using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Training
{
    [BroadcastReceiver(Name = "com.companyname.training.BluetoothReceiver", Enabled = true, Exported = true)]
    public class BluetoothReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Log.Debug("BLLLLLLLLLLLue", "Ayushhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhssssssssssssssssss");
            string action = intent.Action;
            Console.WriteLine("action is: " + action);
            Android.Bluetooth.BluetoothDevice device = (Android.Bluetooth.BluetoothDevice)intent.GetParcelableExtra("android.bluetooth.device.extra.DEVICE");
            Console.WriteLine("Value of extra DEVICE " + device);

        }
    }
}