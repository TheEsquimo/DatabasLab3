using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
        private IMongoCollection<Restaurant> collection;

        public LabThreeDatabase(string connectionString)
        {
            this.connectionString = connectionString;
            client = new MongoClient(connectionString);
            db = client.GetDatabase("Lab3");
            collection = db.GetCollection<Restaurant>("restaurants");
        }

        public void RestaurantCollectionExampleFill()
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    _id = new ObjectId("5c39f9b5df831369c19b6bca"),
                    Name = "Sun Bakery Trattoria",
                    Stars = 4,
                    Categories = new List<string> {"Pizza", "Pasta", "Italian", "Coffee", "Sandwiches"}
                },

                new Restaurant
                {
                    _id = new ObjectId("5c39f9b5df831369c19b6bcb"),
                    Name = "Blue Bagels Grill",
                    Stars = 3,
                    Categories = new List<string> { "Bagels", "Cookies", "Sandwiches"}
                },

                new Restaurant
                {
                    _id = new ObjectId("5c39f9b5df831369c19b6bcc"),
                    Name = "Hot Bakery Cafe",
                    Stars = 4,
                    Categories = new List<string> { "Bakery", "Cafe", "Coffee", "Dessert" }
                },

                new Restaurant
                {
                    _id = new ObjectId("5c39f9b5df831369c19b6bcd"),
                    Name = "XYZ Coffee Bar",
                    Stars = 5,
                    Categories = new List<string> { "Coffee", "Cafe", "Bakery", "Chocolates"}
                },

                new Restaurant
                {
                    _id = new ObjectId("5c39f9b5df831369c19b6bce"),
                    Name = "456 Cookies Shop",
                    Stars = 4,
                    Categories = new List<string> { "Bakery", "Cookies", "Cake", "Coffee"}
                }
            };

            foreach(var restaurant in restaurants)
            {
                collection.InsertOne(restaurant);
            }
        }

        public void PrintAllDocuments()
        {
            var documents = collection.Find<Restaurant>(new BsonDocument());
            Console.WriteLine("\n=====ALL DOCUMENTS=====");
            foreach(var document in documents.ToEnumerable())
            {
                Console.WriteLine(document.ToBsonDocument());
            }
        }

        public void PrintCafes()
        {
            var cafeFilter = Builders<Restaurant>.Filter.Where(r => r.Categories.Contains("Cafe"));
            var projectionExcludeId = Builders<Restaurant>.Projection.Include("Name").Exclude("_id");
            var cafes = collection.Find(cafeFilter).Project(projectionExcludeId);
            Console.WriteLine("\n=====ALL CAFES=====");
            foreach(var cafe in cafes.ToEnumerable())
            {
                Console.WriteLine(cafe);
            }
        }

        public void XYZCoffeeBarStarIncrement()
        {
            var xyzCoffeeBarFilter = Builders<Restaurant>.Filter.Where(r => r.Name == "XYZ Coffee Bar");
            var incrementStarsUpdate = Builders<Restaurant>.Update.Inc((r => r.Stars), 1);
            collection.UpdateOne(xyzCoffeeBarFilter, incrementStarsUpdate);
            Console.WriteLine("\nIncremented XYZ Coffee Bar stars by one");

            PrintAllDocuments();
        }

        public void FourFiveSixCookiesShopNameChange()
        {
            var cookieShopFilter = Builders<Restaurant>.Filter.Where(r => r.Name == "456 Cookies Shop");
            var cookieShopChangeNameUpdate = Builders<Restaurant>.Update.Set(r => r.Name, "123 Cookies Heaven");
            collection.UpdateOne(cookieShopFilter, cookieShopChangeNameUpdate);
            Console.WriteLine("\nChanged name of 456 Cookies Shop to 123 Cookies Heaven");
            PrintAllDocuments();
        }

        public void PrintFourOrMoreStarRestaurants()
        {
            Console.WriteLine("\n=====Restaurants of Four or More Stars=====");
            var fourStarsFilter = Builders<Restaurant>.Filter.Where(r => r.Stars >= 4);
            var nameStarProjection = Builders<Restaurant>.Projection.Include(r => r.Name).Include(r => r.Stars).Exclude(r => r._id);
            var fourStarRestaurants = collection.Aggregate().Match(fourStarsFilter).Project(nameStarProjection).ToList();
            foreach(var restaurant in fourStarRestaurants)
            {
                Console.WriteLine(restaurant.ToBsonDocument());
            }
        }

        public void DeleteDatabase()
        {
            client.DropDatabase("Lab3");
        }
    }
}