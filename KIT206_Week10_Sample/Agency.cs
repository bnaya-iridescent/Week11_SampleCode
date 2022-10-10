using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error


namespace KIT206_Week9
{
    abstract class Agency
    {
        //If including error reporting within this class (as this sample does) then you'll need a way
        //to control whether the errors are actually shown or silently ignored, since once you have
        //connected the GUI to the Boss object then the GUI designer will execute its code, which
        //will try to connect to the database to load live data into the GUI at design time.
        private static bool reportingErrors = false;

        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 8 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //For step 2.2 in Week 8 tutorial
        public static List<Employee> LoadAll()
        {
            List<Employee> staff = new List<Employee>();
            List<Student> students = new List<Student>();
            List<Staff> staffs = new List<Staff>();

            List<Publication> publications = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;
            MySqlDataReader publicationResearcherRdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, email, campus, unit, level, type, utas_start, current_start, degree, supervisor_id from researcher order by family_name", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Gender g = new Gender();
                    if (rdr.IsDBNull(7)) {
                        g = Gender.Student;
                    } else if(rdr.GetString(7) == "A") {
                        g = Gender.A;
                    } else if(rdr.GetString(7) == "B") {
                        g = Gender.B;
                    } else if(rdr.GetString(7) == "C") {
                        g = Gender.C;
                    } else if (rdr.GetString(7) == "D") {
                        g = Gender.D;
                    } else if (rdr.GetString(7) == "E") {
                        g = Gender.E;
                    }
                    //Note that in your assignment you will need to inspect the *type* of the
                    //employee/researcher before deciding which kind of concrete class to create.
                    staff.Add(
                        new Employee { ID = rdr.GetInt32(0), 
                            Name = rdr.GetString(2) + ", " + rdr.GetString(1) + " (" + rdr.GetString(3) + ")" ,
                            Email = rdr.GetString(4),
                            Campus = "School of "+rdr.GetString(6)+", "+ rdr.GetString(5)+" Campus",
                            Gender = g,
                            Type = rdr.GetString(8),
                            CurrentStart = Convert.ToDateTime(rdr.GetString(10)),
                            UtasStartDate = Convert.ToDateTime(rdr.GetString(9)),
                            Degree = rdr.IsDBNull(11)?"-":rdr.GetString(11),
                            SupervisorId = rdr.IsDBNull(11)?0:rdr.GetInt32(12)

                        });

                    if (rdr.GetString(8) =="Staff")
                    {
                        Staff st = new Staff();
                        st.Name = rdr.GetString(2) + ", " + rdr.GetString(1) + " (" + rdr.GetString(3) + ")";
                        st.Email = rdr.GetString(4);
                        st.Campus = "School of " + rdr.GetString(6) + ", " + rdr.GetString(5) + " Campus";
                        st.Gender = g;
                        staffs.Add(st);
                    }
                    else if (rdr.GetString(8) == "Student")
                    {
                        Student st = new Student();
                        st.Name = rdr.GetString(2) + ", " + rdr.GetString(1) + " (" + rdr.GetString(3) + ")";
                        st.Email = rdr.GetString(4);
                        st.Campus = "School of " + rdr.GetString(6) + ", " + rdr.GetString(5) + " Campus";
                        st.Gender = g;
                        students.Add(st);
                    }
                }

                MySqlCommand publicationResearcher = new MySqlCommand
                    ("select rp.researcher_id, rp.doi, p.title, p.authors, p.year. p.type, p.cite_as, available from researcher_publication rp join publication p on rp.doi = p.doi", conn);
                publicationResearcherRdr = publicationResearcher.ExecuteReader();

                while (rdr.Read())
                {
                    publications.Add(
                        new Publication
                        {
                            researcherId = publicationResearcherRdr.GetInt32(0),
                            doi = publicationResearcherRdr.GetString(1),
                            title = publicationResearcherRdr.GetString(2),
                            authors = publicationResearcherRdr.GetString(3),
                            //year = publicationResearcherRdr.GetString(4),
                            //type = publicationResearcherRdr.GetString(5),
                            citeAs = publicationResearcherRdr.GetString(6),
                            available= Convert.ToDateTime(publicationResearcherRdr.GetString(7))
                        });
                }

            }
            catch (MySqlException e)
            {
                ReportError("loading staff", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff;
        }

        //For step 2.3 in Week 8 tutorial
        public static List<Publication> LoadTrainingSessions(int id)
        {
            List<Publication> work = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select title, year, type, available " +
                                                    "from publication as pub, researcher_publication as respub " +
                                                    "where pub.doi=respub.doi and researcher_id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    work.Add(new Publication
                    {
                        title = rdr.GetString(0),
                        year = rdr.GetInt32(1),
                        type = ParseEnum<OutputType>(rdr.GetString(2)),
                        available = rdr.GetDateTime(3)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading training sessions", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return work;
        }

        /// <summary>
        /// In a more complete application this error would be logged to a file
        /// and the error reported back to the original caller, who is closer
        /// to the GUI and hence better able to produce the error message box
        /// (which would not show the actual error details like this does).
        /// </summary>
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
