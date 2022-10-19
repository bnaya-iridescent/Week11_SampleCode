using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control;

namespace Research
{
    class Student : Researcher
    {
        public string degree { get; set; }
        public int supervisorId { get; set; }

        public Staff GetSupervisor()
        {
            ResearcherController b = new ResearcherController();
            Staff supervisor = b.GetStaff(supervisorId);
            return supervisor;
        }
    }
}
