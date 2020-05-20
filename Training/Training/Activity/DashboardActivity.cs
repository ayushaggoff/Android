using System;
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

namespace Training.Activity
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : AppCompatActivity
    {
        public static int ResultCode = 999;

        string Email;
        Button btnAddProfile;
        Button btnFragment;
        TextView TextView_Name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string email = pref.GetString("Email", String.Empty);
            string password = pref.GetString("Password", String.Empty);
            SetContentView(Resource.Layout.DashboardLayout);
            //EditText_Email = FindViewById<TextView>(Resource.Id.textView_Email);
            btnAddProfile = FindViewById<Button>(Resource.Id.button1);
            btnFragment = FindViewById<Button>(Resource.Id.button2);
          //  EditText_Email.Text = email;
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnAddProfile.Click += (obj, e) =>
                 {
                     StartActivityForResult(typeof(AddProfileActivity), ResultCode);
                 };
            this.btnFragment.Click += this.BtnFragment_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            //this.btnAddProfile.Click -= this.BtnAddProfile_Click;
        }

        private void BtnFragment_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(FragementBtnActivity));
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