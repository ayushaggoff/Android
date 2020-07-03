using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Bluetooth;

namespace TrainingOne
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
//        BroadcastReceiver BluetoothReceivers = new BluetoothReceiver();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //IntentFilter filter = new IntentFilter();
            //filter.AddAction(BluetoothDevice.ActionAclConnected);
            //filter.AddAction(BluetoothDevice.ActionFound);
            //filter.AddAction(BluetoothDevice.ActionAclDisconnected);
            //filter.AddAction(BluetoothDevice.ActionAclDisconnectRequested);
            //filter.AddAction(BluetoothDevice.ActionPairingRequest);

            //this.RegisterReceiver(BluetoothReceivers, new IntentFilter(BluetoothDevice.ActionFound));

            //  RegisterReceiver(BluetoothReceivers, new IntentFilter(BluetoothDevice.ActionFound));

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

}
