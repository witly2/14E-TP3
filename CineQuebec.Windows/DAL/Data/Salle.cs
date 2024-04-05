using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Salle
    {
        public ObjectId Id { get; }
        public int NumeroSalle { get; set; }
        public int NombrePlace { get; set; }

        public Salle() { }
    }
}
