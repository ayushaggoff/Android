using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Training.Fragments;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;



namespace Training.Adapter
{
    class PageAdapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();
        public PageAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(V4Fragment fragment, string title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);
        }

        public override int Count
        {
            get
            {
                return fragments.Count;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragments[position];
        }


        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }
    }
}