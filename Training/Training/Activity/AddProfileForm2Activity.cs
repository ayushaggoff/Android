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

namespace Training.Activity
{
    [Activity(Label = "AddProfileForm2Activity")]
    public class AddProfileForm2Activity : AppCompatActivity
    {
        Button btnSubmit, btnCancel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddProfileFormlayout2);

            btnSubmit = FindViewById<Button>(Resource.Id.button2);
            btnCancel = FindViewById<Button>(Resource.Id.button1);

        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnSubmit.Click += this.BtnAddProfile_Click;
            this.btnCancel.Click += this.BtnAddProfile_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnSubmit.Click -= this.BtnAddProfile_Click;
            this.btnCancel.Click += this.BtnAddProfile_Click;
        }

        private void BtnAddProfile_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DashboardActivity));
             intent.AddFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }
    }
}