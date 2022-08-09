using ObjectsFall.Model;
using SQLite;
using System;
using System.IO;

namespace ObjectsFall.Database
{
    public  class ObjectsFallRepository
    {
        protected readonly SQLiteConnection _database;

        public static string DbPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ObjectsFall_Database.db");

        public ObjectsFallRepository()
        {
            _database = new SQLiteConnection(DbPath);
            _database.CreateTable<GameRecord>();
        }
    }
}