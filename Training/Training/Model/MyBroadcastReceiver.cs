﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Training.Model
{
    public class MyBroadcastReceiver : BroadcastReceiver
    {
      
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received broadcast in MyBroadcastReceiver, " +
                                       " value received: " + intent.GetStringExtra("key"),
                                       ToastLength.Long).Show();
        }
    }
}