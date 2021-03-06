﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Training.Activity
{
    [Activity(Label = "VerificationCodeActivity")]
    public class VerificationCodeActivity : LoginActivity
    {
        string Email;
        TextView EditText_Email;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string email = pref.GetString("Email", String.Empty);
            string password = pref.GetString("Password", String.Empty);
            SetContentView(Resource.Layout.VerifyEmail);
            EditText_Email = FindViewById<TextView>(Resource.Id.textView_Email);
            EditText_Email.Text = email;
        }
    }
}