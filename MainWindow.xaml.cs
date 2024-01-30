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


        // 4.2 Create a method called “LoadData” which will populate both LinkedLists. The return type is void.
        void LoadData()
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
        void ShowAllSensorData()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
            TextBoxTest.Text = NumberOfNodes(linkedListA).ToString();
        }

        // 4.5 Create a method called “NumberOfNodes”
        // that will return an integer which is the number of nodes(elements) in a LinkedList.
        private int NumberOfNodes(LinkedList<double> listSize)
        {
            return listSize.Count();
        }
    }
}