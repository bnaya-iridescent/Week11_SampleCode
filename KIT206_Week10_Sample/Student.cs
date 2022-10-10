using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Week9
{
    class Student : Employee
    {
        public string degree { get; set; }
        public int supervisorId { get; set; }

        public Staff GetSupervisor()
        {
            Boss b = new Boss();
            Staff supervisor = b.GetStaff(supervisorId);
            return supervisor;
        }
    }
}
