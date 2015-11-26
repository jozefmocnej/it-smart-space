﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartSpaceWeb
{
    public static class DocumentDBRepository<T>
    {
        //Use the Database if it exists, if not create a new Database
        private static Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = DatabaseId }).Result;
            }

            return db;
        }

        //Use the DocumentCollection if it exists, if not create a new Collection
        private static DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == CollectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                var collectionSpec = new DocumentCollection { Id = CollectionId };
                var requestOptions = new RequestOptions { OfferType = "S1" };

                col = Client.CreateDocumentCollectionAsync(databaseLink, collectionSpec, requestOptions).Result;
            }

            return col;
        }

        //Expose the "database" value from configuration as a property for internal use
        private static string databaseId;
        private static String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(databaseId))
                {
                    databaseId = ConfigurationManager.AppSettings["database"];
                }

                return databaseId;
            }
        }

        //Expose the "collection" value from configuration as a property for internal use
        private static string collectionId;
        private static String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {
                    collectionId = ConfigurationManager.AppSettings["collection"];
                }

                return collectionId;
            }
        }

        //Use the ReadOrCreateDatabase function to get a reference to the database.
        private static Database database;
        private static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = ReadOrCreateDatabase();
                }

                return database;
            }
        }

        //Use the ReadOrCreateCollection function to get a reference to the collection.
        private static DocumentCollection collection;
        private static DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        //This property establishes a new connection to DocumentDB the first time it is used, 
        //and then reuses this instance for the duration of the application avoiding the
        //overhead of instantiating a new instance of DocumentClient with each request
        private static DocumentClient client;
        private static DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }

                return client;
            }
        }

        public static IEnumerable<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate)
                .AsEnumerable();
        }

        public static void CreateQuery(Expression<Func<T, bool>> predicate)
        {

         /*  var items = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate)
                .AsEnumerable();

            */
            
            //var orders = from o in Client.CreateDocumentQuery<Models.Sensor>(Collection.SelfLink)
             //.Where(o => o.Timestamp.Epoch >= DateTime.UtcNow.AddDays(-7).ToEpoch()).AsEnumerable().Select<Models.Sensor>;

          /*  var orderss = from o in Client.CreateDocumentQuery<Models.Sensor>(Collection.SelfLink)
            where o.Timestamp.Epoch >= DateTime.Now.AddDays(-7).ToEpoch()
            select o;*/

         /*   foreach(){

            string a = ("SELECT * FROM c WHERE c.type ='humidity'");
            var query = Client.CreateDocumentQuery(Collection.SelfLink, a);
            var family = query.AsEnumerable().LastOrDefault();
            return null;
                }
            */
           /*string sql = String.Format("SELECT * FROM c ",
                 DateTime.UtcNow.AddDays(-7).ToEpoch());

            IEnumerable <T> sensors = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, sql).AsEnumerable<T>();


            IEnumerable<T> sensorsa = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                        .Where(predicate)
                        .AsEnumerable();

            */
           
           
        }

        public static async Task<Document> CreateItemAsync(T item)
        {
            return await Client.CreateDocumentAsync(Collection.SelfLink, item);
        }

        public static T GetItem(Expression<Func<T, bool>> predicate)
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                        .Where(predicate)
                        .AsEnumerable()
                        .FirstOrDefault();
        }

        public static async Task<Document> UpdateItemAsync(string id, T item)
        {
            Document doc = GetDocument(id);
            return await Client.ReplaceDocumentAsync(doc.SelfLink, item);
        }

        private static Document GetDocument(string id)
        {
            return Client.CreateDocumentQuery(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public static async Task DeleteItemAsync(string id)
        {
            Document doc = GetDocument(id);
            await client.DeleteDocumentAsync(doc.SelfLink);
        }

        //public static async Task CreateTriggerAsync(string id, string body, TriggerType trigType, TriggerOperation trigOperation)
        //{
        //    Trigger trig = new Trigger()
        //    {
        //        Id = id,
        //        Body = body,
        //        TriggerType = trigType,
        //        TriggerOperation = trigOperation
        //    };
            
        //    var trigger = await Client.CreateTriggerAsync(Collection.SelfLink, trig);
        //    var test = trigger.Resource.ResourceId;
    }


    //testing classes, FIXME implement me better
  

    public static class Extensions
    {
        public static int ToEpoch(this DateTime date)
        {
            if (date == null) return int.MinValue;
            DateTime epoch = new DateTime(1970, 1, 1);
            TimeSpan epochTimeSpan = date - epoch;
            return (int)epochTimeSpan.TotalSeconds;
        }
    }

    
}