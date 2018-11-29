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
    static class DatabaseService
    {
        static SQLiteConnection db;

        public static void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();
        }

        public static void CreateTableWithData()
        {
            db.CreateTable<Note>();
            if (db.Table<Note>().Count() == 0)
            {
                var newNote = new Note();
                newNote.NoteTitle = "SQLite notes";
                newNote.NoteContent = "Make an SQLite note taking app";
                newNote.PostTime = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                db.Insert(newNote);
            }
        }

        public static void AddNote(string title, string content)
        {
            var newNote = new Note();
            newNote.NoteTitle = title;
            newNote.NoteContent = content;
            newNote.PostTime = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            db.Insert(newNote);
        }
        public static void EditNote(string newTitle, string newContent, long id)
        {
            var editedNote = new Note();
            editedNote.Id = id;
            editedNote.NoteTitle = newTitle;
            editedNote.NoteContent = newContent;
            editedNote.PostTime = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            db.Update(editedNote);
        }

        public static void DeleteNote(long id)
        {
            db.Delete<Note>(id);
        }

        public static TableQuery<Note> GetAllNotes()
        {
            var table = db.Table<Note>();
            return table;
        }
    }
}