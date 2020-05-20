using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;


using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Training.Adapter;
using Training.Fragments;

namespace Training.Activity
{
    [Activity(Label = "TabActivity")]
    public class TabActivity : AppCompatActivity
    {
        Toolbar toolbar;
        TabLayout tabLayout;
        ViewPager viewPager;
        //PagerAdapter adapter;
        TabItem tabChats;
        TabItem tabStatus;
        TabItem tabCalls;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TabLayout);
            // toolbar = FindViewById(Resource.Id.toolbar);
            tabChats = FindViewById<TabItem>(Resource.Id.tabChats);
            tabStatus = FindViewById<TabItem>(Resource.Id.tabStatus);
            tabCalls = FindViewById<TabItem>(Resource.Id.tabCalls);
            tabLayout = FindViewById<TabLayout>(Resource.Id.tabLayout);
            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager1);
            // Create your application here
            // viewPager.Adapter = new MyPageAdapter(SupportFragmentManager);
            tabLayout.SetupWithViewPager(viewPager);
            // ViewPagerAdapter viewPagerAdapter = new ViewPagerAdapter(SupportFragmentManager);



            if (viewPager.Adapter == null)
            {
                setupViewPager(viewPager);


            }
            else
            {
                viewPager.Adapter.NotifyDataSetChanged();
            }

            tabLayout.SetupWithViewPager(viewPager);
        }
        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new Fragment1(), "First Fragment");
            adapter.AddFragment(new Fragment2(), "Second Fragment");
            adapter.AddFragment(new Fragment3(), "Third Fragment");
            viewPager.Adapter = adapter;
            viewPager.Adapter.NotifyDataSetChanged();
            //viewpager.OffscreenPageLimit(4);


        }

    }
    class Adapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();
        public Adapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
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

