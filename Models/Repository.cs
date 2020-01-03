using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;

namespace Silver
{
    public class Repository
    {
        private LiteDatabase database;

        public Repository()
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                Path.DirectorySeparatorChar + "liveinmegacity" +
                Path.DirectorySeparatorChar + "Silver";
            string fileName = directory + Path.DirectorySeparatorChar + "data.silver";

            Directory.CreateDirectory(directory);
            database = new LiteDatabase(fileName);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity, new()
        {
            var collection = database.GetCollection<T>();
            return collection.FindAll();
        }

        public T Get<T>(T entity) where T : Entity, new()
        {
            var collection = database.GetCollection<T>();
            return collection.FindById(entity.Id);
        }

        public void Upsert<T>(T entity) where T : Entity, new()
        {
            var collection = database.GetCollection<T>();
            entity.IsChanged = false;
            collection.Upsert(entity);
        }

        public void Remove<T>(T entity) where T : Entity, new()
        {
            var collection = database.GetCollection<T>();
            collection.Delete(entity.Id);
        }
    }
}
