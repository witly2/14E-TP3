using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Projection
    {
        public ObjectId Id { get; }
        public DateTime DateHeureDebut { get; set; }
        public Salle Salle { get; set; }
        public Film Film { get; set; }

        public Projection() { }
    }
}
