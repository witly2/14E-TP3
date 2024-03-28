using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film
    {
        public ObjectId Id { get; set; }
        public string OriginalTitle { get; set; }
        public string FrenchTitle { get; set; }
        public string Description { get; set; }
        public ushort Duration { get; set; }
        public DateTime InternationalReleaseDate { get; set; }
        public ushort Rating { get; set; }
        public string OriginalLanguage { get; set; }

        public Film()
        {

        }
    }
}
