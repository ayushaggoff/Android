using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Interop;
using Training.Model;
using static Android.Support.V7.Widget.RecyclerView;

namespace Training.Adapter
{
    class CustomLogListAdapter : BaseAdapter<LogList>
    {

        Context context;
        List<LogList> log;

        public CustomLogListAdapter(List<LogList> log)
        {
            this.log = log;
        }
        public override LogList this[int position]
        {
            get
            {
                return log[position];
            }
        }

        public override int Count
        {
            get
            {
                return log.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        { 
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CustomListViewLayout, parent, false);

                var date = view.FindViewById<TextView>(Resource.Id.Date);
                var status = view.FindViewById<TextView>(Resource.Id.textView1);
                var name = view.FindViewById<TextView>(Resource.Id.Name);
                var time = view.FindViewById<TextView>(Resource.Id.Time);
                
                view.Tag = new ViewHolder() { Date = date, Name = name, Status = status,Time=time };
            }

            var holder = (ViewHolder)view.Tag;
           
            holder.Name.Text = log[position].Name;
            holder.Date.Text= log[position].Date;
            holder.Status.Text= log[position].Status;
            holder.Time.Text= log[position].Time;
            
            SpannableString ss = new SpannableString(holder.Status.Text);
            ForegroundColorSpan fcRed = new ForegroundColorSpan(Color.Red);
            ForegroundColorSpan fcBlue = new ForegroundColorSpan(Color.Blue);
            ForegroundColorSpan fcOrange = new ForegroundColorSpan(Color.DarkOrange);
            ForegroundColorSpan fcGreen = new ForegroundColorSpan(Color.Green);
            var tempStatus = holder.Status.Text.ToLower();

            if (tempStatus.Contains("started"))
            {
               int start = tempStatus.IndexOf("not started");
               int  end = tempStatus.IndexOf("not started") + 11;
            ss.SetSpan(fcRed, start, end, SpanTypes.ExclusiveExclusive);
            }
            if (tempStatus.Contains("progress"))
            {
                int start = tempStatus.IndexOf("in progress");
                int end = tempStatus.IndexOf("in progress") + 11;
                ss.SetSpan(fcBlue, start, end, SpanTypes.ExclusiveExclusive);
            }
            if (tempStatus.Contains("hold"))
            {
                int start = tempStatus.IndexOf("on hold");
                int end = tempStatus.IndexOf("on hold") +7;
                ss.SetSpan(fcOrange, start, end, SpanTypes.ExclusiveExclusive);
            }
            if (tempStatus.Contains("completed"))
            {
                int start = tempStatus.IndexOf("completed");
                int end = tempStatus.IndexOf("completed") + 9;
                ss.SetSpan(fcGreen, start, end, SpanTypes.ExclusiveExclusive);
            }
            //  ss.SetSpan(fcBlue, 35, 46, SpanTypes.ExclusiveExclusive);
            holder.Status.TextFormatted = ss;
            return view;
        }

    }

    class ViewHolder : Java.Lang.Object
    {
        public TextView Name{get;set;}
        public TextView Status{get;set;}
        public TextView Date{get;set;}
        public TextView Time{get;set;}
    }
}