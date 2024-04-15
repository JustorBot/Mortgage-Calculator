using System;
using System.Collections.Generic;

namespace Mortgage
{
    public class MortgageCalculator
    {
        // Method to calculate the monthly repayment amount
        public double CalculateMonthlyRepayment(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            double monthlyInterestRate = annualInterestRate / 12 / 100;
            int numberOfPayments = loanTermYears * 12;

            double monthlyRepayment = loanAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, numberOfPayments)) /
                                      (Math.Pow(1 + monthlyInterestRate, numberOfPayments) - 1);
            return monthlyRepayment;
        }

        // Method to calculate the total amount of interest paid over the life of the loan
        public double CalculateTotalInterestPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numberOfPayments = loanTermYears * 12;

            double totalInterestPaid = (monthlyRepayment * numberOfPayments) - loanAmount;
            return totalInterestPaid;
        }

        // Method to calculate the total amount paid over the life of the loan
        public double CalculateTotalAmountPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numberOfPayments = loanTermYears * 12;

            double totalAmountPaid = monthlyRepayment * numberOfPayments;
            return totalAmountPaid;
        }

        // Method to generate an amortization schedule
        public List<AmortizationScheduleEntry> GenerateAmortizationSchedule(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            List<AmortizationScheduleEntry> schedule = new List<AmortizationScheduleEntry>();

            double monthlyInterestRate = annualInterestRate / 12 / 100;
            int numberOfPayments = loanTermYears * 12;

            double monthlyRepayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            double remainingBalance = loanAmount;

            for (int i = 1; i <= numberOfPayments; i++)
            {
                double interestPaid = remainingBalance * monthlyInterestRate;
                double principalPaid = monthlyRepayment - interestPaid;
                remainingBalance -= principalPaid;

                AmortizationScheduleEntry entry = new AmortizationScheduleEntry()
                {
                    PaymentNumber = i,
                    PaymentAmount = monthlyRepayment,
                    InterestPaid = interestPaid,
                    PrincipalPaid = principalPaid,
                    RemainingBalance = remainingBalance
                };

                schedule.Add(entry);
            }

            return schedule;
        }
    }

    // Class to represent an entry in the amortization schedule
    public class AmortizationScheduleEntry
    {
        public int PaymentNumber { get; set; }
        public double PaymentAmount { get; set; }
        public double InterestPaid { get; set; }
        public double PrincipalPaid { get; set; }
        public double RemainingBalance { get; set; }
    }
}
