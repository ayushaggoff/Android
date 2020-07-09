using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Training
{
    [Service]
    public class TryService : Service
    {
        BluetoothReceiver bluetoothReceiver;

        MediaPlayer player;
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
           
             IntentFilter intentFilter = new IntentFilter("android.bluetooth.adapter.action.STATE_CHANGED");

           intentFilter.AddAction("android.bluetooth.adapter.action.STATE_CHANGED" );
            intentFilter.AddAction("android.bluetooth.adapter.action.CONNECTION_STATE_CHANGED");

            intentFilter.AddAction("android.bluetooth.adapter.action.ACL_CONNECTED");

            intentFilter.AddAction("android.bluetooth.adapter.action.ACL_DISCONNECTED");

            RegisterReceiver(bluetoothReceiver, intentFilter);
            //player = MediaPlayer.Create(this, Settings.System.DefaultAlarmAlertUri);
            //player.Looping = true;
            //player.Start();


            //return StartCommandResult.Sticky;

            //BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            //    // GetDefaultAdapter();
            //    if (mBluetoothAdapter == null)
            //    {
            //        // Device does not support Bluetooth
            //    }
            //    else if (!mBluetoothAdapter.IsEnabled)
            //    {
            //        Toast.MakeText(this, "Bluetooth Is OFFFFFFFFFFFFFFF .", ToastLength.Long).Show();

            //        // Bluetooth is not enabled :)
            //    }
            //    else
            //    {
            //        Toast.MakeText(this, "Bluetooth Is onnnnnnnnnnnnnn .", ToastLength.Long).Show();

            //        // Bluetooth is enabled 
            //    }

            //    Toast.MakeText(this, "Service started by user.", ToastLength.Long).Show();
            
            return StartCommandResult.Sticky;

        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            player.Stop();
        }
    }
}