using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Research;
using Database;

namespace Control
{
    //public enum EmploymentLevel { All, Student, A, B, C, D, E };

    class ResearcherController
    {
        //The example shown here follows the pattern discussed in the Module 6 summary slides,
        //maintaining two collections, a master and a 'viewable' one (which is the 'view model'
        //in Microsoft's Model-View-ViewModel approach to Model-View-Controller)
        private List<Researcher> researcherList;
        public List<Researcher> Workers { get { return researcherList; } set { } }
       
        private ObservableCollection<Researcher> viewableStaff;
        public ObservableCollection<Researcher> VisibleWorkers { get { return viewableStaff; } set { } }

        public ResearcherController()
        {
            researcherList = ERDAdapter.LoadAll();
            viewableStaff = new ObservableCollection<Researcher>(researcherList); //this list we will modify later

            // load publication list for each researcher
            foreach (Researcher e in researcherList)
            {
                e.PublicationList = ERDAdapter.LoadAllPublications(e.ID);
            }
        }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleWorkers;
        }

        internal void CopyEmailsOfViewableList()
        {
            String emails = "";
            foreach (Researcher e in viewableStaff)
            {
                emails = emails + e.Email + ";";
            }
            if (emails != "")
            {
                Clipboard.SetText(emails);
                MessageBox.Show("Emails Copied");
            }
            else
            {
                MessageBox.Show("No email to copy");
            }
            
        }

        //Filter based on EmploymentLevel
        public void FilterLevel(EmploymentLevel level)
        {
            if (level.ToString() != "All")
            {
                var selected = from Researcher e in researcherList
                                where e.EmploymentLevel == level
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            else
            {
                var selected = from Researcher e in researcherList
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            
        }

        public void FilterBasedOnPerformance(PerformanceLevel level)
        {
            if (level == PerformanceLevel.Poor)
            {
                var selected = from Researcher e in researcherList
                               where e.Performance <= 70
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            else if (level == PerformanceLevel.Below_Expectations)
            {
                var selected = from Researcher e in researcherList
                               where e.Performance > 70 && e.Performance < 110
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            else if (level == PerformanceLevel.Meeting_Minimum)
            {
                var selected = from Researcher e in researcherList
                               where e.Performance >= 110 && e.Performance <200
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
            else if (level == PerformanceLevel.Star_Performers)
            {
                var selected = from Researcher e in researcherList
                               where e.Performance >= 200
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }

        }

        //Filter based on Name
        public void FilterName(String name)
        {
            if (name != "")
            {
                var selected = from Researcher e in researcherList
                               where e.FamilyName.ToLower().Contains(name.ToLower()) || e.GivenName.ToLower().Contains(name.ToLower()) 
                               select e;
                viewableStaff.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }

        }



        public Staff GetStaff(int supervisorId)
        {
            Staff supervisor = researcherList.Where(s => s.ID == supervisorId) as Staff;
            return supervisor;
        }
    }
}
