using MongoDB.Bson;
using System.Collections.Generic;

namespace DatabasLab3
{
    internal class Restaurant
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public List<string> Categories { get; set; }

    }
}