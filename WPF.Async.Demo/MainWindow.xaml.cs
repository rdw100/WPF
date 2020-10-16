using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Async.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow main;

        /// <summary>Initializes a new instance of the <see cref="MainWindow" /> class.
        /// This demonstration shows blocked.</summary>
        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        /// <summary>
        /// Executes code synchronously.
        /// </summary>
        /// <param name="sender">The main XAML form</param>
        /// <param name="e">The button click event</param>
        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            main.lblMessage.Content = "Reading file Synchronously...";
            int rows = GetRowCount();
            main.lblMessage.Content = rows.ToString() + " rows read synchronously.";
        }

        /// <summary>
        /// Get a row count from a flat file.
        /// </summary>
        /// <returns>Returns the number of rows in a flat file.</returns>
        private int GetRowCount()
        {
            int counter = 0;
            string line;
            string pathToFile = AppDomain.CurrentDomain.BaseDirectory + "\\assets\\iso_8859-1.txt";

            StreamReader file = new StreamReader(pathToFile);

            while ((line = file.ReadLine()) != null)
            {                
                counter++;
            }

            return counter;
        }

        /// <summary>
        /// Executes code asynchronously.
        /// </summary>
        /// <param name="sender">The main XAML form</param>
        /// <param name="e">The button click event</param>
        private async void AsyncButton_Click(object sender, RoutedEventArgs e)
        {
            main.lblMessage.Content = "Reading file Asynchronously...";
            int counter = await GetRowCountAsync();
            main.lblMessage.Content = counter.ToString() + " rows read asynchronously.";
        }

        /// <summary>
        /// Get a row count from a flat file.
        /// </summary>
        /// <returns>Returns the number of rows in a flat file.</returns>
        private async Task<int> GetRowCountAsync()
        {
            int counter = 0;
            string line;
            string pathToFile = AppDomain.CurrentDomain.BaseDirectory + "\\assets\\iso_8859-1.txt";

            StreamReader file = new StreamReader(pathToFile);

            while ((line = await file.ReadLineAsync()) != null)
            {
                counter++;
            }

            return counter;
        }
    }
}
