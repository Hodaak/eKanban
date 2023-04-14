using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKanban_Andon
{
    public class WorkStation
    {
        public int ID;
        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
