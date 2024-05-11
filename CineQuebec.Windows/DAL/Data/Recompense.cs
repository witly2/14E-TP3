using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Recompense
    {
        [BsonId]
        public ObjectId Id { get; private set; }
        public Abonne Abonne { get; private set; }
        public TypeRecompense Type { get; private set; }
        public Projection? Projection { get; private set; }
        public DateTime? DateExpiration { get; private set; }
    }
}
