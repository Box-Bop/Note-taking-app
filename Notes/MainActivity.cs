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

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var posts = databaseService.GetAllPosts();

            noteListView.Adapter = new CustomAdapter(this, posts.ToList());

            noteListView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("\"" + posts.ToList()[e.Position].NoteTitle + "\"");
                alert.SetMessage("Choose what you want to do:");
                alert.SetButton("Edit", (c, ev) =>
                {
                    var intent = new Intent(this, typeof(EditNote));
                    intent.PutExtra("EditTitle", posts.ToList()[e.Position].NoteTitle);
                    intent.PutExtra("EditContent", posts.ToList()[e.Position].NoteContent);
                    StartActivity(intent);
                });
                alert.SetButton2("Delete", (c, ev) =>
                {
                    var stockID = posts.ToList()[e.Position];
                    //var itemToDelete = stockListView.GetItemAtPosition(e.Position);
                    databaseService.DeletePost(stockID.Id);

                    posts = databaseService.GetAllPosts();
                    noteListView.Adapter = new CustomAdapter(this, posts.ToList());
                });
                alert.Show();
            };

            addNoteButton.Click += delegate
            {
                var postTitle = addNoteEditText.Text;
                var postContent = addNoteContentEditText.Text;
                databaseService.AddPost(postTitle, postContent);

                posts = databaseService.GetAllPosts();
                noteListView.Adapter = new CustomAdapter(this, posts.ToList());

                addNoteEditText.Text = "";
                addNoteContentEditText.Text = "";
            };
        }
    }
}