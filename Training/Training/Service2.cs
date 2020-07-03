using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    [Service]
    public class Service2 : Service
    {
        private Timer _check_timer_data;

        public void DebugBGService()
        {
            _check_timer_data = new Timer((o) => { Log.Debug("Ay", "HELLO'''' Start service"); },
                null, 0, 2000);
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug("LL", "my bg services started");
            //   DebugBGService();

            BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            // GetDefaultAdapter();
            if (mBluetoothAdapter == null)
            {
                // Device does not support Bluetooth
            }
            else if (!mBluetoothAdapter.IsEnabled)
            {
                Toast.MakeText(this, "Bluetooth Is OFFFFFFFFFFFFFFF .", ToastLength.Long).Show();

                // Bluetooth is not enabled :)
            }
            else
            {
                Toast.MakeText(this, "Bluetooth Is onnnnnnnnnnnnnn .", ToastLength.Long).Show();

                // Bluetooth is enabled 
            }

            Toast.MakeText(this, "Service started by user.", ToastLength.Long).Show();
            return StartCommandResult.Sticky;
        }
        public override bool StopService(Intent name)
        {

            return base.StopService(name);
        }


    }
}