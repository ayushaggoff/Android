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

         //   TextView tabOne = (TextView)LayoutInflater.From(this).Inflate(Resource.Layout.custom_tab, null);
         ////   tabOne.SetText();
         //   tabOne.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.iconHome, 0, 0);
         //   tabLayout.GetTabAt(0).SetCustomView(tabOne);

            if (viewPager.Adapter == null)
            {
                setupViewPager(viewPager);
            }
            else
            {
                viewPager.Adapter.NotifyDataSetChanged();
            }

            tabLayout.SetupWithViewPager(viewPager);
            createTabIcons();
        }

        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new PageAdapter(SupportFragmentManager);
            adapter.AddFragment(new Fragment1(), "First Fragment");
            adapter.AddFragment(new Fragment2(), "Second Fragment");
            adapter.AddFragment(new Fragment3(), "Third Fragment");
            viewPager.Adapter = adapter;
            viewPager.Adapter.NotifyDataSetChanged();
            //viewpager.OffscreenPageLimit(4);


        }

        private void createTabIcons()
        {

            TextView tabOne = (TextView)LayoutInflater.From(this).Inflate(Resource.Layout.custom_tab, null);
         
            tabOne.Text = "Tab 1";
            tabOne.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.iconHome, 0, 0);
            tabLayout.GetTabAt(0).SetCustomView(tabOne);

            TextView tabTwo = (TextView)LayoutInflater.From(this).Inflate(Resource.Layout.custom_tab, null);
            tabTwo.Text = "Tab 2";
            tabTwo.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.iconPlanner, 0, 0);
            tabLayout.GetTabAt(1).SetCustomView(tabTwo);

            TextView tabThree = (TextView)LayoutInflater.From(this).Inflate(Resource.Layout.custom_tab, null);
            //tabThree.setText("Tab 3");
            tabThree.Text = "Tab 3";
            tabThree.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.iconsUserMenu, 0, 0);
            tabLayout.GetTabAt(2).SetCustomView(tabThree);
        }

    }
   
}

