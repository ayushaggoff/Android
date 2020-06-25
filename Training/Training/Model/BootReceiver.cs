using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Training.Activity;

namespace Training.Model
{
    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new string[] { Intent.ActionBootCompleted, Intent.ActionLockedBootCompleted, "android.intent.action.QUICKBOOT_POWERON", "com.htc.intent.action.QUICKBOOT_POWERON" })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                if (intent.Action.Equals(Intent.ActionBootCompleted))
                {
                    Toast.MakeText(context, "Received intent!", ToastLength.Long).Show();
                    Intent i = new Intent(context, typeof(DashboardActivity));
                    i.AddFlags(ActivityFlags.NewTask);
                    context.StartActivity(i);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}