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
        EditText edittext_Fname;
        Button btnNext;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPersonalDetailFormLayout);
            edittext_Fname = FindViewById<EditText>(Resource.Id.editTextFirstName);
            btnNext = FindViewById<Button>(Resource.Id.button2);
            
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
            //    Intent intent = new Intent(this, typeof(AddProfileForm1Activity));
            //    StartActivity(intent);
            String message = edittext_Fname.Text;

            Intent intent1 = new Intent();
            intent1.SetData(Android.Net.Uri.Parse(edittext_Fname.Text));
            SetResult(Result.Ok, intent1);
            this.Finish();

          //  Intent intent = new Intent();
           // intent.PutExtra("MESSAGE", message);
            //intent.SetData(Android.Net.Uri.Parse(message));
                                //SetResult(2,intent);  

          //  SetResult(Result.Ok, intent);
          //  StartActivityForResult(intent, 2);
        
       //Finish();//finishing activity  
        }
    }

}