using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DatabasLab3
{
    internal class LabThreeDatabase
    {
        private string connectionString;
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> collection;

        public LabThreeDatabase(string connectionString)
        {
            this.connectionString = connectionString;
            client = new MongoClient(connectionString);
            db = client.GetDatabase("Lab3");
            collection = db.GetCollection<BsonDocument>("restaurants");
        }

        public void RestaurantCollectionExampleFill()
        {
            var sunBakeryTrattoria = new Restaurant
            {
                _id = new ObjectId("5c39f9b5df831369c19b6bca"),
                Name = "Sun Bakery Trattoria",
                Stars = 4,
                Categories = new List<string> {"Pizza", "Pasta", "Italian", "Coffee", "Sandwiches"}
            };

            var blueBagelsGrill = new Restaurant
            {
                _id = new ObjectId("5c39f9b5df831369c19b6bcb"),
                Name = "Blue Bagels Grill",
                Stars = 3,
                Categories = new List<string> { "Bagels", "Cookies", "Sandwiches"}
            };

            var hotBakeryCafe = new Restaurant
            {
                _id = new ObjectId("5c39f9b5df831369c19b6bcc"),
                Name = "Hot Bakery Cafe",
                Stars = 4,
                Categories = new List<string> { "Bakery", "Cafe", "Coffee", "Dessert" }
            };

            var xyzCoffeeBar = new Restaurant
            {
                _id = new ObjectId("5c39f9b5df831369c19b6bcd"),
                Name = "XYZ Coffee Bar",
                Stars = 5,
                Categories = new List<string> { "Coffee", "Cafe", "Bakery", "Chocolates"}
            };

            var fourFiveSixCookiesShop = new Restaurant
            {
                _id = new ObjectId("5c39f9b5df831369c19b6bce"),
                Name = "456 Cookies Shop",
                Stars = 4,
                Categories = new List<string> { "Bakery", "Cookies", "Cake", "Coffee"}
            };

            var restaurants = new List<BsonDocument>();
            restaurants.Add(sunBakeryTrattoria.ToBsonDocument());
            restaurants.Add(blueBagelsGrill.ToBsonDocument());
            restaurants.Add(hotBakeryCafe.ToBsonDocument());
            restaurants.Add(xyzCoffeeBar.ToBsonDocument());
            restaurants.Add(fourFiveSixCookiesShop.ToBsonDocument());
            collection.InsertMany(restaurants);
        }

        public void PrintAllDocuments()
        {
            var documents = collection.Find<BsonDocument>(new BsonDocument());
            foreach(var document in documents.ToEnumerable())
            {
                Console.WriteLine(document);
            }
        }

        public void PrintCafes()
        {

        }

        public void DeleteDatabase()
        {
            client.DropDatabase("Lab3");
        }
    }
}