using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CineQuebec.Windows.DAL.Data
{
    public class Abonne: Person
    {
        public DateTime DateAdhesion { get; set; }

        public Abonne()
        {
        }
    }
}
