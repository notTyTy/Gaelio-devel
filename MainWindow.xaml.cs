using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Galileo6;

// 4.1 Create two data structures using the LinkedList<T>. Must be type double
namespace Gaelio_devel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static LinkedList<double> linkedListA = new();
        static LinkedList<double> linkedListB = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Global Methods
        // 4.2 Create a method called “LoadData” which will populate both LinkedLists. The return type is void.
        public void LoadData()
        {
            linkedListA.Clear();
            linkedListB.Clear();


            // TODO these are placeholders
            double sigma = SigmaUpDown.Value;
            double mu = MuUpDown.Value;
            ReadData readData = new();

            // 4.2 Create the appropriate loop construct to populate the two LinkedList
            // 4.2 The LinkedList size will be hardcoded inside the method and must be equal to 400. 
            for (int i = 0; i < 400; i++)
            {
                linkedListA.AddLast(readData.SensorA(sigma, mu));
                linkedListB.AddLast(readData.SensorB(sigma, mu));
            }
        }
        public void ShowAllSensorData()
        {

            LinkedListView.Items.Clear();
            for (int i = 0; i < (linkedListA.Count | linkedListB.Count); i++)
            {
                LinkedListView.Items.Add(new
                {
                    GetSensorA = linkedListA.ElementAt(i),
                    GetSensorB = linkedListB.ElementAt(i)
                });
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            TextBoxTest.Text = NumberOfNodes(linkedListA).ToString();
            SelectionSort(linkedListA); //TODO move this to dedicated button
            SelectionSort(linkedListB);
            DisplayListboxData(linkedListA, ListboxA);
            DisplayListboxData(linkedListB, ListboxB);

        }
        #endregion
        #region Utility Methods

        // 4.5 Create a method called “NumberOfNodes”
        // that will return an integer which is the number of nodes(elements) in a LinkedList.
        private int NumberOfNodes(LinkedList<double> listSize)
        {
            return listSize.Count();
        }


        // 4.6 Create a method called "DisplayListboxData" that will display
        // the content of a linkedlist inside the appropriate listbox.
        private void DisplayListboxData(LinkedList<double> linkedList, ListBox listboxName)
        {
            listboxName.Items.Clear();
            LinkedList<double> linkedListSorted = new();

            for (int i = 0; i < linkedList.Count(); i++)
            {
                listboxName.Items.Add(new
                {
                    GetSensorData = linkedList.ElementAt(i)
                });
            }

            // TODO implement the click event for displaying data
        }
        #endregion
        #region Sort and Search Methods

        // 4.7 Create a method called SelectionSort which has a single input parameter of type LinkedList,
        // while the calling code argument is linkedlist name. The method code must follow the pseudo code supplied 
        // return type is boolean
        private bool SelectionSort(LinkedList<double> linkedlist)
        {
            int min;
            int max = linkedlist.Count();

            for (int i = 0; i < max; i++)
            {
                min = i;
                for (int j = i + 1; j < max; j++)
                {
                    if (linkedlist.ElementAt(j) < linkedlist.ElementAt(min))
                    {
                        min = j;
                    }
                }
                LinkedListNode<double> currentMin = linkedlist.Find(linkedlist.ElementAt(min));
                LinkedListNode<double> currentI = linkedlist.Find(linkedlist.ElementAt(i));
                
                double temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;

            }
            // TODO unsure about bool return
            return true;
        }

        #region UI Button Methods
        // 4.11 Create button click methods that will search the linked list for an integer value entered into a textbox on the form.

        // 4.11 Method for sensor A/B and binary search iterative 



        // TODO make a single method for both
        // 4.11 Method for sensor A/B and binary search recursive

        #endregion

        #endregion

    }
}