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
using Newtonsoft.Json;
using Training.Model;

namespace Training.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/LoginTheme")]
    public class LoginActivity : AppCompatActivity
    {
        Button btnLogin;
        EditText email, password;
        CheckBox mCbxRemMe;
      
        Android.App.AlertDialog.Builder dialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.LoginLayout);
            email = FindViewById<EditText>(Resource.Id.editText_email);
            password = FindViewById<EditText>(Resource.Id.editText_password);
            btnLogin = FindViewById<Button>(Resource.Id.buttonlogin);
            mCbxRemMe = FindViewById<CheckBox>(Resource.Id.cbxRememberMe);
            dialog = new Android.App.AlertDialog.Builder(this);

          //  btnLogin.Click += BtnLogin_Click;
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnLogin.Click += this.BtnLogin_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnLogin.Click -= this.BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(DashboardActivity));

            if (CheckValidation(email.Text, password.Text))
            {
                User user = new User()
                {
                    Email = email.Text,
                    Password = password.Text
                };

                intent.PutExtra("User", JsonConvert.SerializeObject(user));

                if (mCbxRemMe.Checked)
                {
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.PutString("Email", email.Text.Trim());
                    edit.PutString("Password", password.Text.Trim());
                    edit.Apply();
                }
                this.StartActivity(intent);
            }
        }
        public bool CheckValidation(string email,string password)
        {
            Android.App.AlertDialog alert = dialog.Create();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                alert.SetTitle("Alert");
                alert.SetMessage("Enter All Fields");
                alert.SetButton("OK", (c, ev) =>
                {
                    Toast.MakeText(this, "Enter All Fields", ToastLength.Long).Show();

                });
                alert.Show();
                return false;
            }
            else if (!Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
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
                return false;
            }
            else
            {
                return true;
                //alert.SetTitle("Login");
                //alert.SetMessage("Success");

                //alert.SetButton("OK", (c, ev) =>
                //{
                //    Intent intent = new Intent(this, typeof(VerificationCodeActivity));
                //    StartActivity(intent);
                    
                //   intent.PutExtra("email", userObj.Email);
                //    StartActivity(intent);
                //});
                //alert.SetButton2("CANCEL", (c, ev) =>
                //{

                //});
                //alert.Show();
            }
        }

    }
}