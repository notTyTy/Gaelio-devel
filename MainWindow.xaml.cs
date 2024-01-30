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
            double sigma = 10;
            double mu = 5;
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

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            TextBoxTest.Text = NumberOfNodes(linkedListA).ToString();
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

            // TODO implement the click event for displaying data
        }
        #endregion
        #region Sort and Search Methods

        private bool SelectionSort(LinkedList<double> linkedlist)
        {
            int min = 0;
            int max = linkedlist.Count();
            bool flag = false;

            for (int i = 0; i < max; i++)
            {
                min = i;
                for (int j = i + 1; j > max; j++)
                {
                    if (linkedlist.ElementAt(i) < linkedlist.ElementAt(min))
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
            // unsure about bool return
            return true;
        }


        // 4.7 Create a method called SelectionSort which has a single input parameter of type LinkedList,
        // while the calling code argument is linkedlist name. The method code must follow the pseudo code supplied 
        // return type is boolean
        /*
         integer min => 0
        integer max => numberOfNodes(list)
        for ( i = 0 to max - 1 )
        min => i
        for ( j = i + 1 to max )
        if (list element(j) < list element(min))
        min => j
        END for
        // Supplied C# code
        LinkedListNode<double> currentMin = list.Find(list.ElementAt(min))
        LinkedListNode<double> currentI = list.Find(list.ElementAt(i))
        // End of supplied C# code
        var temp = currentMin.Value
        currentMin.Value = currentI.Value
        currentI.Value = temp
        END for
        */












        #endregion


    }
}