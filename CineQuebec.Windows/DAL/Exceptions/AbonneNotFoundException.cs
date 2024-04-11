using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Exceptions
{
    public class AbonneNotFoundException:Exception
    {
        public AbonneNotFoundException(string message):base(message) 
        {
            
        }
    }
}
