﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook.AppLinks;
using Xamarin.Facebook.Login;

namespace Training.Activity
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : AppCompatActivity
    {
        public static int ResultCode = 999;

        string Email;
        Button btnAddProfile;
        Button btnFragment,btnLogout;
        Button btnTab;
        TextView TextView_Name, TextView_Email;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            
            string email = pref.GetString("Email", String.Empty);
            string password = pref.GetString("Password", String.Empty);
            SetContentView(Resource.Layout.DashboardLayout);
            string datafromlogin = Intent.GetStringExtra("Name");
            TextView_Name = FindViewById<TextView>(Resource.Id.textview_name);
            btnLogout = FindViewById<Button>(Resource.Id.button4);
      

            TextView_Name.Text = datafromlogin;
      
            TextView_Email = FindViewById<TextView>(Resource.Id.textView_Email);
            btnAddProfile = FindViewById<Button>(Resource.Id.button1);
            btnFragment = FindViewById<Button>(Resource.Id.button2);
            btnTab = FindViewById<Button>(Resource.Id.button3);
            TextView_Email.Text = datafromlogin;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnAddProfile.Click += (obj, e) =>
                 {
                     StartActivityForResult(typeof(AddProfileActivity), ResultCode);
                 };

            this.btnLogout.Click += (obj, e) =>
            {
                LoginManager.Instance.LogOut();
               
            
            Intent intent = new Intent(this,typeof(LoginActivity));
                  
            StartActivity(intent);
             Finish();
    };


            this.btnFragment.Click += this.BtnFragment_Click;
            this.btnTab.Click += this.BtnTab_Click;


        }
        protected override void OnPause()
        {
            base.OnPause();
            //this.btnAddProfile.Click -= this.BtnAddProfile_Click;
        }

        private void BtnFragment_Click(object sender, System.EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(FragementBtnActivity));
            Intent intent = new Intent(this, typeof(LogListActivity));
            StartActivity(intent);
        }
        private void BtnAddProfile_Click(object sender, System.EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(AddProfileActivity));
            //StartActivity(intent);
            //   Intent intent = new Intent(this, typeof(AddProfileActivity));
            StartActivityForResult(typeof(AddProfileActivity), ResultCode);
            //StartActivityForResult(typeof(AddProfileActivity), 2);
        }

        private void BtnTab_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TabActivity));
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum]  Result resultCode, Intent data)
        {
            TextView_Name = FindViewById<TextView>(Resource.Id.textview_name);

            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == ResultCode)
            {
                //TextView_Name.Background = Color.Red;
                if (resultCode == Result.Ok)
                {
                //    String message = data.GetStringExtra("MESSAGE");
                    TextView_Name.Text = data.Data.ToString();
                }
                if (resultCode == Result.Canceled)
                {
                    
                    TextView_Name.Text = "Nothing is selected";
                }
            }
        }
    }
}