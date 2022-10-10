using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Week9
{
    class Boss
    {
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Employee> staff;
        
        private List<Staff> staffs;
        
        private List<Student> students;
        public List<Employee> Workers { get { return staff; } set { } }
       
        private ObservableCollection<Employee> viewableStaff;
        public ObservableCollection<Employee> VisibleWorkers { get { return viewableStaff; } set { } }

        public Boss()
        {
            staff = Agency.LoadAll();
            viewableStaff = new ObservableCollection<Employee>(staff); //this list we will modify later

            //Part of step 2.3.2 from Week 8 tutorial
            foreach (Employee e in staff)
            {
                e.Skills = Agency.LoadTrainingSessions(e.ID);
            }
        }

        public ObservableCollection<Employee> GetViewableList()
        {
            return VisibleWorkers;
        }

        //This version of Filter modifies the viewable list instead of returning a new list,
        //but the procedure is almost the same
        public void Filter(Gender gender)
        {
            if (gender.ToString() != "All")
            {
                var selected = from Employee e in staff
                                where e.Gender == gender
                                select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            
        }

        public Staff GetStaff(int supervisorId)
        {
            Staff supervisor = staff.Where(s => s.ID == supervisorId) as Staff;
            return supervisor;
        }
    }
}
