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
using Training.Fragments;

namespace Training.Activity
{
    [Activity(Label = "FragementBtnActivity")]
    public class FragementBtnActivity : AppCompatActivity
    {
        Button btn_Fragment1;
        Button btn_Fragment2;
        Button btn_Fragment3;
        Button btn_Fragment4;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FramentBtnlayout);
            btn_Fragment1 = FindViewById<Button>(Resource.Id.button1);
            btn_Fragment2 = FindViewById<Button>(Resource.Id.button2);
            btn_Fragment3 = FindViewById<Button>(Resource.Id.button3);
            btn_Fragment4 = FindViewById<Button>(Resource.Id.add);
           
          
            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.btn_Fragment1.Click += this.btn_Fragment1_Click;
            this.btn_Fragment2.Click += this.btn_Fragment2_Click;
            this.btn_Fragment3.Click += this.btn_Fragment3_Click;
            this.btn_Fragment4.Click += this.btn_Fragment4_Click;

        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        protected void btn_Fragment1_Click(object sender, System.EventArgs e)
        {
            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, new Fragment1(), "fragment1");
            trans.Commit();
        }
        protected void btn_Fragment2_Click(object sender, System.EventArgs e)
        {
            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, new Fragment2(), "fragment2");
            trans.Commit();
        }
        protected void btn_Fragment3_Click(object sender, System.EventArgs e)
        {
            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, new Fragment3(), "fragment3");
            trans.Commit();
        }protected void btn_Fragment4_Click(object sender, System.EventArgs e)
        {
            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, new Fragment4(), "fragment4");
            trans.Commit();
        }
    }
}