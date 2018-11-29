using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Android.Widget.AdapterView;
using Android.Content;
using System.Collections.Generic;

namespace Notes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            var noteListView = FindViewById<ListView>(Resource.Id.listView1);
            var addNoteEditText = FindViewById<EditText>(Resource.Id.editText1);
            var addNoteContentEditText = FindViewById<EditText>(Resource.Id.editText2);
            var addNoteButton = FindViewById<Button>(Resource.Id.button1);

            DatabaseService.CreateDatabase();
            DatabaseService.CreateTableWithData();
            var notes = DatabaseService.GetAllNotes();

            noteListView.Adapter = new CustomAdapter(this, notes.ToList());

            noteListView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("\"" + notes.ToList()[e.Position].NoteTitle + "\"");
                alert.SetMessage("Choose what you want to do:");
                alert.SetButton("Delete", (c, ev) =>
                {
                    var noteID = notes.ToList()[e.Position];
                    //var itemToDelete = stockListView.GetItemAtPosition(e.Position);
                    DatabaseService.DeleteNote(noteID.Id);

                    notes = DatabaseService.GetAllNotes();
                    noteListView.Adapter = new CustomAdapter(this, notes.ToList());
                });
                //Just a blank button so that "edit" and "delete" would be on the left and right.
                alert.SetButton2("\u200B            ", (c, ev) =>
                {
                });
                alert.SetButton3("Edit", (c, ev) =>
                {
                    var intent = new Intent(this, typeof(EditNote));
                    intent.PutExtra("EditTitle", notes.ToList()[e.Position].NoteTitle);
                    intent.PutExtra("EditContent", notes.ToList()[e.Position].NoteContent);
                    intent.PutExtra("NoteID", notes.ToList()[e.Position].Id);
                    StartActivity(intent);
                });
                alert.Show();
            };

            addNoteButton.Click += delegate
            {
                var noteTitle = addNoteEditText.Text;
                var noteContent = addNoteContentEditText.Text;
                DatabaseService.AddNote(noteTitle, noteContent);

                notes = DatabaseService.GetAllNotes();
                noteListView.Adapter = new CustomAdapter(this, notes.ToList());

                addNoteEditText.Text = "";
                addNoteContentEditText.Text = "";
            };
        }
        protected override void OnPostResume()
        {
            base.OnPostResume();
            var notes = DatabaseService.GetAllNotes();
            var noteListView = FindViewById<ListView>(Resource.Id.listView1);
            noteListView.Adapter = new CustomAdapter(this, notes.ToList());
        }
    }
}