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

namespace Training.Src
{
  public class Service1:Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Toast.MakeText(this, "Service started by user.", ToastLength.Long).Show();
            return StartCommandResult.Sticky;
        }
        public override bool StopService(Intent name)
        {

            return base.StopService(name);
        }


    }
}