using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Week9
{
    public class Publication
    {
        public string doi { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public int year { get; set; }
        public OutputType type { get; set; }
        public string citeAs { get; set; }
        public DateTime available { get; set; }

        public Employee researcher { get; set; }

        public int researcherId { get; set; }

        public int Age()
        {
            return 1;
        }
    }

    public enum OutputType { Conference, Journal, Other }
}
