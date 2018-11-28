using System;
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
    class NoteEditAdapter : BaseAdapter<Note>
    {
        string title;
        string content;
        Activity context;
        public NoteEditAdapter(Activity context, string title, string content) : base()
        {
            this.context = context;
            this.title = title;
            this.content = content;
        }

        public override Note this[int position]
        {
            get { return null; }
        }

        public override int Count { get { return 1; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.edit_note_layout, null);
            view.FindViewById<EditText>(Resource.Id.textInputEditText1).Text = title;
            view.FindViewById<EditText>(Resource.Id.textInputEditText2).Text = content;
            return view;
        }
    }
}