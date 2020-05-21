using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Training.Adapter;
using Training.Src;

namespace Training.Activity
{
    [Activity(Label = "LogListActivity")]
    public class LogListActivity : AppCompatActivity
    {
        ListView myList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListViewLayout);

            myList = FindViewById<ListView>(Resource.Id.line1);
            myList.Adapter = new CustomLogListAdapter(LogListData.Log);
        }
    }
}