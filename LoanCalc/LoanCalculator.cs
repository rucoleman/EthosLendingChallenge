// <copyright file="LoanCalculator.cs" company="PlaceholderCompany">
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
        public const string DownpaymentMoreThanLoanAmountMessage = "Down payment must be less than loan amount";
        public const string NegativeNumberNotAllowedMessage = "Negative number not allowed";

        public string DoTheMath(LoanCalcInput input)
        {
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
            this.ValdateInput(input);

            int principal = input.Amount - input.DownPayment;
            int numpayments = input.Term * 12;
            double negnumpayments = numpayments * -1.0;
            double effectiveinterest = (input.Interest / 100.0) / 12;
            double monthlypayment = principal * (effectiveinterest / (1 - Math.Pow(1 + effectiveinterest, negnumpayments)));

            double totalPayments = monthlypayment * numpayments;
            double totalInterest = totalPayments - principal;

            string monthlypaymentFormatted = string.Format("{0:0.00}", monthlypayment);
            string totalInterestFormatted = string.Format("{0:0.00}", totalInterest);
            string totalPaymentsFormatted = string.Format("{0:0.00}", totalPayments);

            LoanCalcOutput loanCalcOutput = new LoanCalcOutput
            {
                MonthlyPayment = monthlypaymentFormatted,
                TotalInterest = totalInterestFormatted,
                TotalPayment = totalPaymentsFormatted
            };
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
                throw new ArgumentOutOfRangeException("input.downpayment", input.DownPayment, DownpaymentMoreThanLoanAmountMessage);
            }
        }
    }
}
