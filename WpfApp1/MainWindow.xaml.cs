using System;
using System.Collections.Generic;
using System.Windows;
using MathNet.Numerics.Statistics;

namespace ExpenseForecastingTool
{
    public partial class MainWindow : Window
    {
        // Lists to store historical and future dates and expenses
        private List<DateTime> historicalDates = new List<DateTime>();
        private List<double> historicalExpenses = new List<double>();
        private List<DateTime> futureDates = new List<DateTime>();

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for adding historical expenses.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            // Parsing input and adding to lists
            if (DateTime.TryParse(ExpenseDatePicker.Text, out DateTime date) && double.TryParse(ExpenseTextBox.Text, out double expense))
            {
                historicalDates.Add(date);
                historicalExpenses.Add(expense);

                HistoricalExpensesListBox.Items.Add($"{date.ToShortDateString()}: {expense}");

                ExpenseDatePicker.Text = "";
                ExpenseTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid date and expense.");
            }
        }

        /// <summary>
        /// Event handler for adding future dates.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AddFutureDateButton_Click(object sender, RoutedEventArgs e)
        {
            // Parsing input and adding to future dates list
            if (DateTime.TryParse(FutureDatePicker.Text, out DateTime date))
            {
                futureDates.Add(date);

                FutureDatePicker.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid future date.");
            }
        }

        /// <summary>
        /// Event handler for analyzing historical data and making predictions.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            // Check for sufficient data
            if (historicalDates.Count < 2)
            {
                MessageBox.Show("Insufficient historical data. Please enter at least two data points.");
                return;
            }

            // Check for future dates
            if (futureDates.Count == 0)
            {
                MessageBox.Show("No future dates provided. Please enter at least one future date.");
                return;
            }

            // Perform data analysis and prediction
            var historicalArray = historicalExpenses.ToArray();
            var historicalMean = historicalArray.Mean();
            var historicalStandardDeviation = historicalArray.StandardDeviation();

            var result = "";
            foreach (var futureDate in futureDates)
            {
                // Generate a random expense prediction within a certain range
                var predictedExpense = GenerateRandomExpensePrediction(historicalMean, historicalStandardDeviation);
                result += $"{futureDate.ToShortDateString()}: {predictedExpense}\n";
            }

            // Display predictions
            ExpensePredictionsListBox.Items.Clear();
            ExpensePredictionsListBox.Items.Add(result);
        }

        /// <summary>
        /// Method to generate a random expense prediction.
        /// </summary>
        /// <param name="mean">The mean value of historical expenses.</param>
        /// <param name="stdDev">The standard deviation of historical expenses.</param>
        /// <returns>A random expense prediction.</returns>
        private double GenerateRandomExpensePrediction(double mean, double stdDev)
        {
            // You can use more sophisticated algorithms and data processing techniques here to generate accurate predictions
            var random = new Random();
            return random.NextDouble() * stdDev + mean;
        }
    }
}
