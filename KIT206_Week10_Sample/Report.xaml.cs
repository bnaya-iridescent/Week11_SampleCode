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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private const string STAFF_LIST_KEY = "researcherList";
        private ResearcherController ResearcherController;

        public Report()
        {
            InitializeComponent();
            ResearcherController = (ResearcherController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;
        }

        // when level is selected, call filter from controller class
        private void FilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResearcherController.FilterBasedOnPerformance(ERDAdapter.ParseEnum<PerformanceLevel>(FilterListLevel.SelectedValue.ToString()));

        }

        private void Copy_Email_Button_Click(object sender, RoutedEventArgs e)
        {
            ResearcherController.CopyEmailsOfViewableList();
        }
    }

}
