﻿// <copyright file="LoanCalculator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanCalc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class LoanCalculator
    {
        public const string DownPaymentMoreThanLoanAmountMessage = "Down payment must be less than loan amount";
        public const string NegativeNumberNotAllowedMessage = "Negative number not allowed";

        public string DoTheMath(LoanCalcInput input)
        {
            // Check some typical error cases and throw
            this.ValdateInput(input);

            /*
                The following formula is implemeted here:

                M = payment amount
                P = principal (amount of money borrowed)
                J = effective interest rate
                N = total number of payments

                P = amount - downpayment
                M = P * (J / (1 - (1 + J)^-N))
                TotalPayments = M * N
                TotalInterest = TotalPayments - P
            */

            var principal = input.Amount - input.DownPayment;
            var numPayments = input.Term * 12;
            var negNumPayments = numPayments * -1.0;

            // The effective interest rate is expressed as a fraction and then divided by 12 (number of months in a year)
            var effectiveInterest = (input.Interest / 100.0) / 12;
            var monthlyPayment = principal * (effectiveInterest / (1 - Math.Pow(1 + effectiveInterest, negNumPayments)));

            var totalPayments = monthlyPayment * numPayments;
            var totalInterest = totalPayments - principal;

            // Initialize a temp object with properly formatted strings before we convert it to JSON.
            // The LoanCalcOutput class uses specific property name attributes as required in the problem statement.
            var loanCalcOutput = new LoanCalcOutput
            {
                MonthlyPayment = string.Format("{0:0.00}", monthlyPayment),
                TotalInterest = string.Format("{0:0.00}", totalInterest),
                TotalPayment = string.Format("{0:0.00}", totalPayments)
            };

            // Use JSON.net to create a JSON-formatted string of the above temporary object
            return JsonConvert.SerializeObject(loanCalcOutput);
        }

        private void ValdateInput(LoanCalcInput input)
        {
            if (input.Amount < 0)
            {
                throw new ArgumentOutOfRangeException("input.amount", input.Amount, NegativeNumberNotAllowedMessage);
            }

            if (input.DownPayment < 0)
            {
                throw new ArgumentOutOfRangeException("input.downpayment", input.DownPayment, NegativeNumberNotAllowedMessage);
            }

            if (input.Term < 0)
            {
                throw new ArgumentOutOfRangeException("input.term", input.Term, NegativeNumberNotAllowedMessage);
            }

            if (input.Interest < 0)
            {
                throw new ArgumentOutOfRangeException("input.interest", input.Interest, NegativeNumberNotAllowedMessage);
            }

            if (input.DownPayment > input.Amount)
            {
                throw new ArgumentOutOfRangeException("input.downpayment", input.DownPayment, DownPaymentMoreThanLoanAmountMessage);
            }
        }
    }
}
