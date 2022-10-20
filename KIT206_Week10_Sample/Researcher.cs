﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control;
using Database;

namespace Research
{

    
    public enum EmploymentLevel { All, Student, A, B, C, D, E };

    /// <summary>
    /// A class baring a striking resemblance to a university researcher
    /// </summary>
    public class Researcher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public EmploymentLevel EmploymentLevel { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }

        public int SupervisorId { get; set; }

        public string Supervisor
        {
            get 
            {
                string supName = "No Supervisor";
                if (SupervisorId != 0)
                {
                    List<Researcher> allResearchers = ERDAdapter.LoadAll();
                    var supervisor = from Researcher s in allResearchers
                                     where s.ID == SupervisorId
                                     select s.Name;
                    supName = supervisor.First();
                }
                return supName;
            }
        }

        public string Degree { get; set; }

        public DateTime UtasStartDate { get; set; }

        public DateTime CurrentStart { get; set; }

        public List<Publication> PublicationList { get; set; }

        public Position GetCurrentJob()
        {
            Position position = new Position();
            return position;
        }

        public string CurrentJobTitle()
        {
            return "test";
        }

        public DateTime CurrentJobStart()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime;
        }
        public Position GetEarliestJob()
        {
            Position position = new Position();
            return position;
        }

        public DateTime EarliestStart()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime;
        }

        public double Tenure()
        {
            double days =  (DateTime.Now - UtasStartDate ).TotalDays;
            return Math.Round(days / 365,2);
        }

        public double TenureCount
        {
            get { return Tenure(); }
        }

        public int PublicationsCount()
        {
            return 1;
        }

        public int PublicationCount
        {
            get { return PublicationList == null ? 0 : PublicationList.Count(); }
        }

        public double ThreeYearAverage
        {
            get { return ThreeYearAverageCalculate(); }
        }

        public double ThreeYearAverageCalculate()
        {
            double val = 0;
            int currentYear = DateTime.Today.Year;
            var lastThreeYearPublications = from Publication s in PublicationList
                                               where currentYear - s.year <= 3
                                               select s;
            val = (double)lastThreeYearPublications.Count() / 3;
            return Math.Round(val, 2);
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}
