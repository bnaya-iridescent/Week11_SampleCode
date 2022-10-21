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
using System.Web;
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
            

        }

        private void loadimage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@"C:\506Project\KIT506_Week11_Sample (2)\KIT506_Week11_Sample\KIT206_Week10_Sample\photos\"+ imagePath + ".jpg");
            bitmap.EndInit();
            researcherImage.Source = bitmap;
        }

        private void sampleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DetailsPanel.DataContext = e.AddedItems[0];
                Researcher r = (Researcher)e.AddedItems[0];
                loadimage(Convert.ToString(r.ID));
            }

        }

        //Part of task 3.4
        private void button_showPublications(object sender, RoutedEventArgs e)
        {
            
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

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            report.Show();
        }
    }
}
