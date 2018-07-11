// <copyright file="InputConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoanPaymentCalculatorApp
{
    using System;
    using System.Collections.Generic;
    using LoanCalc;

    internal class InputConverter
    {
        // Exception messages
        public const string LoanAmountBadFormatMessage = "Loan amount not formatted correctly";
        public const string InterestRateBadFormatMessage = "Interest rate not formatted correctly";
        public const string DownPaymentBadFormatMessage = "Down payment not formatted correctly";
        public const string LoanTermBadFormatMessage = "Loan term not formatted correctly";

        // Dictionary keys
        private const string AmountKey = "amount";
        private const string RateKey = "interest";
        private const string DownKey = "downpayment";
        private const string TermKey = "term";

        private Dictionary<string, string> dict;

        internal InputConverter(Dictionary<string, string> dict)
        {
            this.dict = dict;
        }

        internal LoanCalcInput ToLoanCalcInput()
        {
            if (!int.TryParse(this.dict[AmountKey], out int parsedLoanAmount))
            {
                throw new ArgumentOutOfRangeException(AmountKey, this.dict[AmountKey], LoanAmountBadFormatMessage);
            }

            this.GetRateFromDict(out float parsedRate);

            if (!int.TryParse(this.dict[DownKey], out int parsedDownPayment))
            {
                throw new ArgumentOutOfRangeException(DownKey, this.dict[DownKey], DownPaymentBadFormatMessage);
            }

            if (!int.TryParse(this.dict[TermKey], out int parsedTerm))
            {
                throw new ArgumentOutOfRangeException(TermKey, this.dict[TermKey], LoanTermBadFormatMessage);
            }

            // Initialize and return the object to be used with the loan calculator.
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
            // The dictionary contains the interest rate as a string.
            var rate = this.dict[RateKey];
            var rateWasGivenAsPercentage = false;

            if (rate.EndsWith("%"))
            {
                // Rate was given as a percentage, e.g., 5.5%
                rate = rate.Remove(rate.Length - 1);
                rateWasGivenAsPercentage = true;
            }

            if (!float.TryParse(rate, out parsedRate))
            {
                throw new ArgumentOutOfRangeException(RateKey, this.dict[RateKey], InterestRateBadFormatMessage);
            }

            if (!rateWasGivenAsPercentage)
            {
                // Rate was given as a value from 0 to 1, e.g., .055
                parsedRate *= 100;
            }
        }
    }
}
