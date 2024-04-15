using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mortgage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mortgage
{
    public partial class Form1 : Form
    {
        private MortgageCalculator calculator;

        public Form1()
        {
            InitializeComponent();
            calculator = new MortgageCalculator();
        }

        // Inside the button1_Click event handler, update the ListBox with the results
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get input values from text boxes
                double loanAmount = double.Parse(textBox1.Text);
                double annualInterestRate = double.Parse(textBox2.Text);
                int loanTermYears = int.Parse(textBox3.Text);

                // Calculate mortgage details
                double monthlyRepayment = calculator.CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
                double totalInterestPaid = calculator.CalculateTotalInterestPaid(loanAmount, annualInterestRate, loanTermYears);
                double totalAmountPaid = calculator.CalculateTotalAmountPaid(loanAmount, annualInterestRate, loanTermYears);

                // Display the output in the ListBox
                listBox1.Items.Clear();
                listBox1.Items.Add($"Monthly Repayment: {monthlyRepayment:C}");
                listBox1.Items.Add($"Total Interest Paid: {totalInterestPaid:C}");
                listBox1.Items.Add($"Total Amount Paid: {totalAmountPaid:C}");

                // Generate amortization schedule
                List<AmortizationScheduleEntry> amortizationSchedule = calculator.GenerateAmortizationSchedule(loanAmount, annualInterestRate, loanTermYears);

                // Clear existing items and add new ones
                listView1.Clear();
                listView1.View = View.Details;
                listView1.Columns.Add("Payment Number", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Payment Amount", 150, HorizontalAlignment.Left);
                listView1.Columns.Add("Interest Paid", 150, HorizontalAlignment.Left);
                listView1.Columns.Add("Principal Paid", 150, HorizontalAlignment.Left);
                listView1.Columns.Add("Remaining Balance", 150, HorizontalAlignment.Left);

                // Add each entry in the amortization schedule as a row in the table
                foreach (var entry in amortizationSchedule)
                {
                    ListViewItem item = new ListViewItem(entry.PaymentNumber.ToString());
                    item.SubItems.Add(entry.PaymentAmount.ToString("C"));
                    item.SubItems.Add(entry.InterestPaid.ToString("C"));
                    item.SubItems.Add(entry.PrincipalPaid.ToString("C"));
                    item.SubItems.Add(entry.RemainingBalance.ToString("C"));
                    listView1.Items.Add(item);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numerical values for Loan Amount, Annual Interest Rate, and Loan Term.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

    }
}