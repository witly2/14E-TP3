using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public string Username { get; set; }
        public DateTime DateAdhesion { get; set; }
        public string Email { get; set; }
        public byte[] Password {  get; set; }
        public byte[] Salt { get; set; }


        public Abonne()
        {
        }
    }
}
