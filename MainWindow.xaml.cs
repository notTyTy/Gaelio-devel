using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
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
            ListboxA.Items.Clear(); // Clears the previous sorted data from listboxes
            ListboxB.Items.Clear();


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
            ResetTimers(); // Resets timers for a better aesthetic
            LoadData();
            ShowAllSensorData();
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
            for (int i = 0; i < linkedList.Count(); i++)
            {
                listboxName.Items.Add(new
                {
                    GetSensorData = linkedList.ElementAt(i)
                });
            }
        }
        #endregion
        #region Sort and Search Methods

        // 4.7 Create a method called SelectionSort which has a single input parameter of type LinkedList,
        // while the calling code argument is linkedlist name. The method code must follow the pseudo code supplied 
        // return type is boolean
        private bool SelectionSort(LinkedList<double> linkedlist)
        {
            int min;
            int max = NumberOfNodes(linkedlist);

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
            return true;
        }

        private bool InsertionSort(LinkedList<double> linkedList)
        {
            int max = NumberOfNodes(linkedList);
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (linkedList.ElementAt(j - 1) > linkedList.ElementAt(j))
                    {
                        LinkedListNode<double> current = linkedList.Find(linkedList.ElementAt(j));
                        LinkedListNode<double> currentLess = linkedList.Find(linkedList.ElementAt(j - 1));

                        double temp = currentLess.Value;
                        currentLess.Value = current.Value;
                        current.Value = temp;

                        //TODO INSERTION SWAP
                        // add swap code here by swapping previous value with current value

                    }
                }
            }
            return true;
        }
        #endregion

        #region UI Button Methods
        // 4.11 Create button click methods that will search the linked list for an integer value entered into a textbox on the form.

        // 4.11 Method for sensor A/B and binary search iterative
        // 4.11 Method for sensor A/B and binary search recursive

        // 4.12	Create button click methods that will sort the LinkedList using the Selection and Insertion methods

        // TODO REFACTOR

        private void SortMethod(ListBox listbox, LinkedList<double> linkedList, string sortType, TextBox textBox)
        {

            listbox.Items.Clear();
            Stopwatch stopwatch = new();
            stopwatch.Start();


            if (sortType == "SelectionSort")
            {
                SelectionSort(linkedList);
                stopwatch.Stop();
            }
            else if (sortType == "InsertionSort" )
            {
                SelectionSort(linkedList);
                stopwatch.Stop();
            }
            // add more depending on the sort method
            textBox.Text = $"{stopwatch.ElapsedMilliseconds} ms";
            DisplayListboxData(linkedList, listbox);
        }
        private void SelectionSortABtn_Click(object sender, RoutedEventArgs e)
        {
            SortMethod(ListboxA, linkedListA, "SelectionSort", SelectionSortATime);
        }
        private void InsertionSortABtn_Click(object sender, RoutedEventArgs e)
        {
            SortMethod(ListboxA, linkedListA, "InsertionSort", InsertionSortATime);
        }

        private void SelectionSortBBtn_Click(object sender, RoutedEventArgs e)
        {
            ListboxB.Items.Clear();

            Stopwatch stopwatch = new();
            stopwatch.Start();

            if (SelectionSort(linkedListB))
            {
                stopwatch.Stop();
                SelectionSortBTime.Text = $"{stopwatch.ElapsedMilliseconds} ms";
            }
            DisplayListboxData(linkedListB, ListboxB);
        }
        private void InsertionSortBBtn_Click(object sender, RoutedEventArgs e)
        {
            ListboxB.Items.Clear();

            Stopwatch stopwatch = new();
            stopwatch.Start();

            if (InsertionSort(linkedListB))
            {
                stopwatch.Stop();
                InsertionSortBTime.Text = $"{stopwatch.ElapsedMilliseconds} ms";
            }
            DisplayListboxData(linkedListB, ListboxB);
        }

        #endregion

        private void ResetTimers()
        {
            List<TextBox> textboxes = new List<TextBox> { SelectionSortATime, SelectionSortBTime, InsertionSortATime, InsertionSortBTime };
            foreach (TextBox textBox in textboxes)
            {
                textBox.Text = "0 ms";
            }
        }
    }
}