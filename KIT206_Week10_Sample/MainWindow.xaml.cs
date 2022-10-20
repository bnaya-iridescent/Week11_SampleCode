using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Control;
using Research;
using Database;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Part of task 3.4. If a resource really does need to be shared across many different views
        //then consider putting this code (and that for 3.4 below) into the App class, with a public
        //property to access the shared resource.
        private const string STAFF_LIST_KEY = "researcherList";
        private ResearcherController ResearcherController;

        public MainWindow()
        {
            InitializeComponent();
            ResearcherController = (ResearcherController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
            //MyList.Items.Filter = NameFilter;
        }

        private void sampleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //After Task 4 done, this is not really needed
                //MessageBox.Show("The selected item is: " + e.AddedItems[0]);
                //Part of task 4
                DetailsPanel.DataContext = e.AddedItems[0];         
            }
        }

        private void sampleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The text entered is: " + FilterByName.Text);
        }

        private void sampleTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sampleButton_Click(sender, e);
            }
        }

        //Part of task 3.4
        private void btnDeleteOne_Click(object sender, RoutedEventArgs e)
        {

            DetailsPanel.DataContext = new { Name = "Fred", SkillCount = 5 };


            if (ResearcherController.VisibleWorkers.Count > 0)
            {
                Researcher theRemoved = ResearcherController.VisibleWorkers[0]; //this is just to keep the GUI tidy (after Task 4 implemented)
                ResearcherController.VisibleWorkers.RemoveAt(0); //the actual removal step
                //completing keeping the GUI tidy (something similar may be required in the assignment)
                if (DetailsPanel.DataContext == theRemoved)
                {
                    DetailsPanel.DataContext = null;
                }
            }
        }

        private void ExampleUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        // when level is selected, call filter from controller class
        private void FilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResearcherController.FilterLevel(ERDAdapter.ParseEnum<EmploymentLevel>(FilterListLevel.SelectedValue.ToString()));

        }

        // when name is entered to search, call filter from controller class
        private void FilterByName_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            ResearcherController.FilterName(FilterByName.ToString());

        }

        private bool NameFilter(object obj)
        {
            var Filterobj = obj as Researcher;
            return Filterobj.Name.ToLower().Contains(FilterByName.Text.ToLower());
        }

        private void FilterByName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (FilterByName.Text == null)
            {
                MyList.Items.Filter = null;
            }
            else
            {
                MyList.Items.Filter = NameFilter;
            }
        }



    }
}
