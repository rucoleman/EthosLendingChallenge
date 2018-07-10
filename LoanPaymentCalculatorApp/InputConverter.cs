// <copyright file="InputConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanPaymentCalculatorApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LoanCalc;

    internal class InputConverter
    {
        public const string LoanAmountBadFormatMessage = "Loan amount not formatted correctly";
        public const string InterestRateBadFormatMessage = "Interest rate not formatted correctly";
        public const string DownPaymentBadFormatMessage = "Down payment not formatted correctly";
        public const string LoanTermBadFormatMessage = "Loan term not formatted correctly";

        private const string Amount = "amount";
        private const string Rate = "interest";
        private const string Down = "downpayment";
        private const string Term = "term";

        private Dictionary<string, string> dict;

        public InputConverter(Dictionary<string, string> dict)
        {
            this.dict = dict;
        }

        public LoanCalcInput ToLoanCalcInput()
        {
            if (!int.TryParse(this.dict[Amount], out int parsedLoanAmount))
            {
                throw new ArgumentOutOfRangeException(Amount, this.dict[Amount], LoanAmountBadFormatMessage);
            }

            this.GetRateFromDict(out float parsedRate);

            if (!int.TryParse(this.dict[Down], out int parsedDownPayment))
            {
                throw new ArgumentOutOfRangeException(Down, this.dict[Down], DownPaymentBadFormatMessage);
            }

            if (!int.TryParse(this.dict[Term], out int parsedTerm))
            {
                throw new ArgumentOutOfRangeException(Term, this.dict[Term], LoanTermBadFormatMessage);
            }

            return new LoanCalcInput()
            {
                Amount = parsedLoanAmount,
                Interest = parsedRate,
                DownPayment = parsedDownPayment,
                Term = parsedTerm
            };
        }

        private void GetRateFromDict(out float parsedRate)
        {
            var rate = this.dict[Rate];
            var givenAsPercentage = false;
            if (rate.EndsWith("%"))
            {
                rate = rate.Remove(rate.Length - 1);
                givenAsPercentage = true;
            }

            if (!float.TryParse(rate, out parsedRate))
            {
                throw new ArgumentOutOfRangeException(Rate, this.dict[Rate], InterestRateBadFormatMessage);
            }

            if (!givenAsPercentage)
            {
                parsedRate *= 100;
            }
        }
    }
}
