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
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : AppCompatActivity
    {
        string Email;
        Button btnAddProfile;
        TextView EditText_Email;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string email = pref.GetString("Email", String.Empty);
            string password = pref.GetString("Password", String.Empty);
            SetContentView(Resource.Layout.DashboardLayout);
            EditText_Email = FindViewById<TextView>(Resource.Id.textView_Email);
            btnAddProfile = FindViewById<Button>(Resource.Id.button1);
            EditText_Email.Text = email;
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnAddProfile.Click += this.BtnAddProfile_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnAddProfile.Click -= this.BtnAddProfile_Click;
        }

        private void BtnAddProfile_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddProfileActivity));
            StartActivity(intent);
        }
    }
}