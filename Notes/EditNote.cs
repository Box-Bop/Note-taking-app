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

namespace Notes
{
    [Activity(Label = "EditNote")]
    class EditNote : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.edit_note_layout);
            var title = Intent.GetStringExtra("EditTitle");
            var content = Intent.GetStringExtra("EditContent");
            NoteEdit(title, content);
            var confirmButton = FindViewById<Button>(Resource.Id.button1);
            confirmButton.Click += ConfirmButton_Click;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void NoteEdit(string title, string content)
        {
            var titleEditText = FindViewById<EditText>(Resource.Id.textInputEditText1).Text = title;
            var contentEditText = FindViewById<EditText>(Resource.Id.textInputEditText2).Text = content;
        }
    }
}