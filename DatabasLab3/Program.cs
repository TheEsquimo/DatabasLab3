using System;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabasLab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new LabThreeDatabase(connectionString);
            client.RestaurantCollectionExampleFill();
            client.PrintAllDocuments();
            client.PrintCafes();
            client.XYZCoffeeBarStarIncrement();
            client.FourFiveSixCookiesShopNameChange();
            client.PrintFourOrMoreStarRestaurants();
        }
    }
}
