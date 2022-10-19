using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Research;

namespace Control
{
    class PublicationController
    {
        
        private List<Researcher> researcherList;
        
        private List<Staff> staffs;
        
        private List<Student> students;
        public List<Researcher> Workers { get { return researcherList; } set { } }
       
        private ObservableCollection<Researcher> viewableStaff;
        public ObservableCollection<Researcher> VisibleWorkers { get { return viewableStaff; } set { } }

        public PublicationController()
        {
           
        } 


    }
}
