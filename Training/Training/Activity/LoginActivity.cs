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
        Button buttonSubmit;
        Android.App.AlertDialog.Builder dialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
            buttonSubmit = FindViewById<Button>(Resource.Id.buttonsubmit);
            dialog = new Android.App.AlertDialog.Builder(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.FindViewById<Button>(Resource.Id.buttonsubmit).Click += this.CheckValidation;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.FindViewById<Button>(Resource.Id.buttonsubmit).Click -= this.CheckValidation;
        }
        public void CheckValidation(object sender, EventArgs e)
        {
            User userObj = new User();
            EditText EditText_Email = FindViewById<EditText>(Resource.Id.editText_email);
            EditText EditText_Password = FindViewById<EditText>(Resource.Id.editText_password);
            userObj.Email = EditText_Email.Text.ToString();
            userObj.Password = EditText_Password.Text.ToString();
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(("" + userObj.Email).Trim());
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
            else if (!isValid)
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
                  

                });
                alert.SetButton2("CANCEL", (c, ev) =>
                {

                });
                alert.Show();

            }
        }
    }
}