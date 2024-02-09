﻿using System.Collections.Generic;
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

        // Create a method called InsertionSort which has a single input parameter of type LinkedList, while
        // the calling code argument is the linkedList name. The method code must follow the pseudo code supplied.
        // return type is boolean
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


                    }
                }
            }
            return true;
        }

        // 4.9 Create a method called BinarySearchIterative which has the parameters LinkedList, SearchValue, Minimum and Maximum
        // This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value.
        // The calling code argument is the linkedList name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied.


        // TODO change order of overload method variables
        private int BinarySearchIterative(LinkedList<double> linkedList, TextBox searchValue, int minimum, int maximum)
        {
            // todo
            double value = double.Parse(searchValue.Text);

            while (minimum <= maximum)
            {
                int middle = (minimum + maximum) / 2;
                if (value == linkedList.ElementAt(middle))
                {
                    return ++middle;
                }
                else if (value < linkedList.ElementAt(middle))
                {
                    maximum = middle - 1;

                }
                else
                {
                    minimum = middle + 1;
                }
            }
            return minimum;
        }

        // 4.9 Create a method called BinarySearchIterative which has the following four parameters
        // LinkedList, SearchValue, Minimum and Maximum. This method will return an integer of the list
        // element from a successful search of the nearest neighbour value. The calling code is the 
        // linked list name, search value, minimum list size and the number of nodes in the list.
        // The method code must follow the pseudo code supplied
        private int BinarySearchRecursive(LinkedList<double> linkedList, TextBox searchValue, int minimum, int maximum)
        {
            int value = int.Parse(searchValue.Text);

            if (minimum <= maximum - 1)
            {
                int middle = minimum + maximum / 2;
                if (value == linkedList.ElementAt(middle))
                {
                    return middle;
                }
                else if (value < linkedList.ElementAt(middle))
                {
                    return BinarySearchRecursive(linkedList, searchValue, minimum, middle - 1);
                }
                else
                {
                    return BinarySearchRecursive(linkedList, searchValue, middle + 1, maximum);

                }
            }
            return minimum;
        }


        #endregion

        #region UI Button Methods
        // 4.11 Create button click methods that will search the linked list for an integer value entered
        // into a textbox on the form.

        // 4.11 Method for sensor A/B and binary search iterative
        private void IterativeSearchABtn_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new();
            
            stopwatch.Start();
            int value = BinarySearchIterative(linkedListA, SearchInputA, 0, linkedListA.Count());
            stopwatch.Stop();

            IterativeSearchATicks.Text = $"{stopwatch.ElapsedTicks} ticks";

            ListboxA.SelectedIndex = value;
            if (double.Parse(SearchInputA.Text) < 0)
            {
                ListboxA.SelectedIndex--;
            }

            ListboxA.ScrollIntoView(ListboxA.SelectedItem);
        }

        private void IterativeSearchBBtn_Click(object sender, RoutedEventArgs e)
        {
            int value = BinarySearchIterative(linkedListB, SearchInputB, 0, NumberOfNodes(linkedListB));
            ListboxB.SelectedIndex = value;
        }

        // 4.11 Method for sensor A/B and binary search recursive
        private void RecursiveSearchABtn_Click(object sender, RoutedEventArgs e)
        {
            int value = BinarySearchRecursive(linkedListA, SearchInputA, 0, NumberOfNodes(linkedListA));
            ListboxA.SelectedIndex = value;
        }

        private void RecursiveSearchBBtn_Click(object sender, RoutedEventArgs e)
        {
            int value = BinarySearchRecursive(linkedListB, SearchInputB, 0, NumberOfNodes(linkedListB));
            ListboxB.SelectedIndex = value;
        }





        // 4.12	Create button click methods that will sort the LinkedList using the Selection and Insertion methods
        // I've created a single overloaded method that is called within each button

        private void SortMethod(ListBox listbox, LinkedList<double> linkedList, string sortType, TextBox textBox)
        {
            listbox.Items.Clear();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            {
                // The SelectionSort(linkedList) bool check is here to ensure the bool is used to stop the time, can not include it in the overload method
                // as it is ALWAYS true, without a timer delay 
                if (sortType == "SelectionSort" && SelectionSort(linkedList))
                {
                    stopwatch.Stop();
                }
                else if (sortType == "InsertionSort" && InsertionSort(linkedList))
                {
                    stopwatch.Stop();
                }
            }
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
            SortMethod(ListboxB, linkedListB, "SelectionSort", SelectionSortBTime);
        }
        private void InsertionSortBBtn_Click(object sender, RoutedEventArgs e)
        {
            SortMethod(ListboxB, linkedListB, "InsertionSort", InsertionSortBTime);
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