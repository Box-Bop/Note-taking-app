using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Notes
{
    class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();
        }

        public void CreateTableWithData()
        {
            db.CreateTable<Note>();
            if (db.Table<Note>().Count() == 0)
            {
                var newPost = new Note();
                newPost.NoteTitle = "SQLite notes";
                newPost.NoteContent = "Make an SQLite note taking app";
                newPost.PostTime = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                db.Insert(newPost);
            }
        }

        public void AddPost(string title, string content)
        {
            var newPost = new Note();
            newPost.NoteTitle = "SQLite notes";
            newPost.NoteContent = "Make an SQLite note taking app";
            newPost.PostTime = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            db.Insert(newPost);
        }

        public void DeletePost(long id)
        {
            db.Delete<Note>(id);
        }

        public TableQuery<Note> GetAllPosts()
        {
            var table = db.Table<Note>();
            return table;
        }
    }
}