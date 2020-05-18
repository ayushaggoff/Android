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
    [Activity(Label = "AddProfileActivity")]
    public class AddProfileActivity : AppCompatActivity
    {
        string firstName;
        string lastName;
        Button btnNext;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddProfileFormLayout);

            btnNext = FindViewById<Button>(Resource.Id.button1);
            
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btnNext.Click += this.BtnAddProfile_Click;
        }
        protected override void OnPause()
        {
            base.OnPause();
            this.btnNext.Click -= this.BtnAddProfile_Click;
        }

        private void BtnAddProfile_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddProfileForm1Activity));
            StartActivity(intent);
        }

    }
}