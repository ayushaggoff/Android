using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Training.Fragments;

namespace Training.Adapter
{
    class MyPageAdapter : FragmentPagerAdapter
    {
        int count;
        Context context;

        public MyPageAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            count = 3;
        }

        public override int Count
        {
            get
            {
                return count;
            }
        }

        

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment=new Fragment2();
                    break;
                case 1:
                    fragment=new Fragment1();
                    break;
                case 2:
                    fragment= new Fragment3();
                    break;
               
            }
                    return fragment;
        }
    }
}