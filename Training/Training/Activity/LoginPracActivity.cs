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
using Xamarin.Essentials;

namespace Training.Activity
{
    [Activity(Label = "LoginPracActivity", Theme = "@style/LoginTheme", MainLauncher =false)]
    public class LoginPracActivity : AppCompatActivity
    {
        string Password, username;
        EditText EditTxt_Password, EditTxt_Username;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginPractLayout);
            EditTxt_Username = FindViewById<EditText>(Resource.Id.editText_email);
            EditTxt_Password = FindViewById<EditText>(Resource.Id.editText_password);
            // Create your application here
        }
        //protected override void OnSaveInstanceState(Bundle outState)
        //{
        //    base.OnSaveInstanceState(outState);
          
        //    outState.PutString("email",EditTxt_Username.Text);
        //    outState.PutString("password", EditTxt_Password.Text);
        //   // outState.PutString("password","1");
        //}
        //protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        //{
        //    base.OnRestoreInstanceState(savedInstanceState);
        //         EditTxt_Username.Text= savedInstanceState.GetString("email");
        //    EditTxt_Password.Text= savedInstanceState.GetString("password");
        //   // Toast.MakeText(this, savedInstanceState.GetString("password"), ToastLength.Long);   
        //}
    }
}