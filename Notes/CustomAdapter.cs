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

namespace Notes
{
    class CustomAdapter : BaseAdapter<Note>
    {
        List<Note> items;
        Activity context;
        public CustomAdapter(Activity context, List<Note> items) : base()
        {
            this.context = context;
            this.items = items;

        }

        public override Note this[int position]
        {
            get { return items[position]; }
        }

        public override int Count { get { return items.Count; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.note_layout, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = items[position].NoteTitle;
            view.FindViewById<TextView>(Resource.Id.textView2).Text = items[position].NoteContent;
            view.FindViewById<TextView>(Resource.Id.textView3).Text = items[position].PostTime;
            return view;
        }
    }
}