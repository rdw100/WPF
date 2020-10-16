using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.TPL.Demo
{
    /// <summary>
    /// Interactive form logic for two responsive buttons on a XAML form 
    /// (MainWindow.xaml): a concurrent operation for an increment counter 
    /// and an asynchronous themed operation named.
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow main;
        int counter;

        /// <summary>Initializes a new instance of the <see cref="MainWindow" /> class.</summary>
        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        /// <summary>Handles the Click event of the btnCount control.  
        /// Increments a counter to demonstrate UI responsiveness during
        /// long process execution.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnCount_Click(object sender, RoutedEventArgs e)
        {
            lblCountMessage.Content = $"{++counter}";
        }

        /// <summary>Handles the ClickAsync event of the btnCook control.  
        /// Processes a task using the WhenAny options that completes when 
        /// all tasks in the argument have completed.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void btnCook_ClickAsync(object sender, RoutedEventArgs e)
        {
            btnCook.IsEnabled = false;
            lblCookMessage.Content = "Starting breakfast ...";

            Task<Coffee> coffeeTask = BrewCoffeeAsync(12);
            Task<Egg> eggsTask = ScrambleEggsAsync(16);
            Task<Bacon> baconTask = FryBaconAsync(24);
            Task<Toast> toastTask = ToastBreadAsync(8);

            // The breakfast task is comprised of many tasks.
            var breakfastTasks = new List<Task> { coffeeTask, eggsTask, baconTask, toastTask };            

            // WhenAny process task awaits an ordered task completion.
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);

                if (finishedTask == coffeeTask)
                {
                    main.lblCoffee.Content = String.Format("Coffee brewed.");
                }
                else if (finishedTask == eggsTask)
                {
                    main.lblEggs.Content = String.Format("Eggs scrambled.");
                }
                else if (finishedTask == baconTask)
                {
                    main.lblBacon.Content = String.Format("Bacon crisped.");
                }
                else if (finishedTask == toastTask)
                {
                    main.lblToast.Content = "Toast toasted.";
                }

                breakfastTasks.Remove(finishedTask);
            }

            lblCookMessage.Content = "Breakfast complete.";

            btnCook.IsEnabled = true;
        }

        /// <summary>Brews the coffee asynchronously.</summary>
        /// <param name="cups">The number of cups.</param>
        /// <returns>
        ///   Returns brewed coffee.
        /// </returns>
        private static async Task<Coffee> BrewCoffeeAsync(int cups)
        {
            main.lblCoffee.Content = "... Start coffee";
            await Task.Delay(1500);
            for (int cup = 0; cup < cups; cup++)
            {
                main.lblCoffee.Content = String.Format($"... brewing {cup} cup(s) of coffee");
                await Task.Delay(1000);
            }
            await Task.Delay(1500);

            return new Coffee();
        }

        /// <summary>Toasts the bread asynchronously.</summary>
        /// <param name="slices">The total slices of bread.</param>
        /// <returns>
        ///   Returns toasted toast.
        /// </returns>
        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            main.lblToast.Content = "... Start toasting";
            for (int slice = 0; slice < slices; slice++)
            {
                main.lblToast.Content = String.Format($"... putting {slice} of {slices} slices of bread in the toaster");
                await Task.Delay(1000);
            }
            await Task.Delay(3000);

            return new Toast();
        }

        /// <summary>Fries the bacon asynchronously.</summary>
        /// <param name="slices">The total slices of bacon.</param>
        /// <returns>
        ///   Returns crispy bacon.
        /// </returns>
        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            main.lblBacon.Content = String.Format($"... putting {slices} slices of bacon in the pan");
            main.lblBacon.Content = String.Format("... cooking first side of bacon");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                main.lblBacon.Content = String.Format($"... turning {slice} of {slices} slice(s) of bacon");
                await Task.Delay(500);
            }
            main.lblBacon.Content = String.Format("... cooking the second side of bacon");

            return new Bacon();
        }

        /// <summary>Fries the eggs asynchronously.</summary>
        /// <param name="eggs">The number of eggs being prepared.</param>
        /// <returns>
        ///   Returns scrambled eggs.
        /// </returns>
        private static async Task<Egg> ScrambleEggsAsync(int eggs)
        {
            main.lblEggs.Content = String.Format("... heating the skillet");
            await Task.Delay(3000);
            for (int egg = 0; egg < eggs; egg++)
            {
                main.lblEggs.Content = String.Format($"... whisking {egg} egg(s).");
                await Task.Delay(750);
            }            
            main.lblEggs.Content = String.Format("... cooking scrambled eggs");
            await Task.Delay(3000);

            return new Egg();
        }
    }

    /// <summary>
    ///   Empty class for the purpose of demonstration, contain no properties, and serve no other purpose.
    /// </summary>
    internal class Coffee
    {
    }

    /// <summary>
    ///   Empty class for the purpose of demonstration, contain no properties, and serve no other purpose.
    /// </summary>
    internal class Egg
    {
    }

    /// <summary>
    ///   Empty class for the purpose of demonstration, contain no properties, and serve no other purpose.
    /// </summary>
    internal class Bacon
    {
    }

    /// <summary>
    ///   Empty class for the purpose of demonstration, contain no properties, and serve no other purpose.
    /// </summary>
    internal class Toast
    {
    }
}