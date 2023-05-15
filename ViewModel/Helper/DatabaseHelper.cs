using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace Evernote_Clone.ViewModel.Helper
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDB.db3");

        public static bool Insert<T>(T item) //we are creating a generic insert function
        {
            bool result = false;

            using(SQLiteConnection connection= new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>(); //t species that any type of table will be created 
                int rows= connection.Insert(item);
                if(rows>0)
                    result = true;
            }
            return result;
        }

        public static bool Update<T>(T item) 
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>(); 
                int rows = connection.Update(item);
                if (rows > 0)
                    result = true;
            }
            return result;
        }

        public static bool Delete<T>(T item) 
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>(); 
                int rows = connection.Delete(item);
                if (rows > 0)
                    result = true;
            }
            return result;
        }

        public static List<T> Read<T>() where T: new() //we are specifying that the abstract type T should have a parameterless constructor 
        {
            List<T> items;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>(); 
                items = connection.Table<T>().ToList();
                
            }
            return items;
        }
    }
}
