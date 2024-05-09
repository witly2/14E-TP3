using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Exceptions
{
    public class PreferenceAlreadyExistsException: Exception
    {
        public PreferenceAlreadyExistsException(string message) : base(message) { }
    }
}
