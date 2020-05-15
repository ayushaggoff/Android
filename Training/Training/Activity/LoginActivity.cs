using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Training.Model;

namespace Training.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        Button buttonlogin;
        Android.App.AlertDialog.Builder dialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
             buttonlogin = FindViewById<Button>(Resource.Id.buttonlogin);
            dialog = new Android.App.AlertDialog.Builder(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.buttonlogin.Click += this.CheckValidation;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.FindViewById<Button>(Resource.Id.buttonlogin).Click -= this.CheckValidation;
        }
        public void CheckValidation(object sender, EventArgs e)
        {
            User userObj = new User();
            EditText EditText_Email = FindViewById<EditText>(Resource.Id.editText_email);
            EditText EditText_Password = FindViewById<EditText>(Resource.Id.editText_password);
            userObj.Email = EditText_Email.Text.ToString();
            userObj.Password = EditText_Password.Text.ToString();
            var emailvalidate = isValidEmail(userObj.Email);

            Android.App.AlertDialog alert = dialog.Create();

            if (userObj.Email == "" || userObj.Password == "")
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter All Fields");

                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();
            }
            else if (emailvalidate == false)
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter Valid Email");

                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();
            }
            else
            {
                alert.SetTitle("Login");
                alert.SetMessage("Success");

                alert.SetButton("OK", (c, ev) =>
                {
                    Intent intent = new Intent(this, typeof(VerificationCodeActivity));
                    StartActivity(intent);
                    
                   intent.PutExtra("email", userObj.Email);
                    StartActivity(intent);
                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();

            }
        }
        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

    }
}