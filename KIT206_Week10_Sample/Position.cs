using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Research
{
    public class Position
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public EmploymentLevel level { get; set; }
        public int researcherID { get; set; }

        public string Title()
        {
            return "title";
        }

        public string ToTitle(EmploymentLevel level)
        {
            return Title();
        }
    }

}