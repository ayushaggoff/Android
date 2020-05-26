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
        Android.Support.V7.Widget.AppCompatImageButton btnHome;
        Android.Support.V7.Widget.AppCompatImageButton btnContact;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListViewLayout);

            myList = FindViewById<ListView>(Resource.Id.line1);
            btnHome = FindViewById<Android.Support.V7.Widget.AppCompatImageButton>(Resource.Id.imageButton1);
            btnContact = FindViewById<Android.Support.V7.Widget.AppCompatImageButton>(Resource.Id.imageButton2);
            myList.Adapter = new CustomLogListAdapter(LogListData.Log);
            myList.ItemClick += MyList_ItemClick;
        }

        private void MyList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           Toast.MakeText(this, LogListData.Log[e.Position].Status.ToString(), ToastLength.Short).Show();
        //    Toast.MakeText(this,(int)myList.GetItemIdAtPosition(e.Position), ToastLength.Short).Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            btnHome.Click += this.BtnHome_Click;
            btnContact.Click += this.BtnContact_Click;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DashboardActivity));
               StartActivity(intent);
        }
        private void BtnContact_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ContactListActivity));
            StartActivity(intent);
        }
    }
}