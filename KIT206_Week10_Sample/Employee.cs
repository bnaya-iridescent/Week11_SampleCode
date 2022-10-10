﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIT206_Week9
{

    //As an example, this includes an additional 'gender' called Any that could be used in a GUI drop-down list.
    //The filtering could then be modified that if Gender.Any is selected that the full list is returned with no filtering performed.
    public enum Gender { All, Student, A, B, C, D, E };

    /// <summary>
    /// A class baring a striking resemblance to a university researcher
    /// </summary>
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Gender Gender { get; set; }
        public string Campus { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string Photo { get; set; }

        public int SupervisorId { get; set; }

        public string Supervisor
        {
            get { return Name; }
        }

        public string Degree { get; set; }

        public DateTime UtasStartDate { get; set; }

        public DateTime CurrentStart { get; set; }

        public List<Publication> Skills { get; set; }

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

        public int SkillCount
        {
            get { return Skills == null ? 0 : Skills.Count(); }
        }


        //The SkillCount out of 10, expressed as a percentage
        public double SkillPercent
        {
            //This is equivalent to SkillCount / 10.0 * 100
            get { return SkillCount * 10.0; }
        }

        //This is likely the solution you will have devised
        public DateTime MostRecentTraining
        {
            get
            {
                var skillDates = from Publication s in Skills
                                 orderby s.available descending
                                 select s.available;
                return skillDates.First();
            }
        }
        
        public override string ToString()
        {
            //For the purposes of this week's demonstration this returns only the name
            return Name;
        }
    }
}