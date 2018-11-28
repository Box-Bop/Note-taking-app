using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Android.Widget.AdapterView;

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


            var postListView = FindViewById<ListView>(Resource.Id.listView1);
            var addPostTitleEditText = FindViewById<EditText>(Resource.Id.editText1);
            var addPostContentEditText = FindViewById<EditText>(Resource.Id.editText2);
            var addPostButton = FindViewById<Button>(Resource.Id.button1);

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var posts = databaseService.GetAllPosts();

            postListView.Adapter = new CustomAdapter(this, posts.ToList());

            postListView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                var stockID = posts.ToList()[e.Position];
                //var itemToDelete = stockListView.GetItemAtPosition(e.Position);
                databaseService.DeletePost(stockID.Id);

                posts = databaseService.GetAllPosts();
                postListView.Adapter = new CustomAdapter(this, posts.ToList());
            };

            addPostButton.Click += delegate
            {
                var postTitle = addPostTitleEditText.Text;
                var postContent = addPostContentEditText.Text;
                databaseService.AddPost(postTitle, postContent);

                posts = databaseService.GetAllPosts();
                postListView.Adapter = new CustomAdapter(this, posts.ToList());

                addPostTitleEditText.Text = "";
            };
        }
    }
}