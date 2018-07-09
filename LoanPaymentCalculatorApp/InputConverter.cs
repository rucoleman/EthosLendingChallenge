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

        private const string AMOUNT = "amount";
        private const string RATE = "interest";
        private const string DOWN = "downpayment";
        private const string TERM = "term";

        private Dictionary<string, string> dict;

        public InputConverter(Dictionary<string, string> dict)
        {
            this.dict = dict;
        }

        public LoanCalcInput ToLoanCalcInput()
        {
            int parsedloanamount;
            float parsedrate;
            int parseddownpayment;
            int parsedterm;
            if (!int.TryParse(this.dict[AMOUNT], out parsedloanamount))
            {
                throw new ArgumentOutOfRangeException(AMOUNT, this.dict[AMOUNT], LoanAmountBadFormatMessage);
            }

            parsedrate = this.GetRateFromDict();
            if (!int.TryParse(this.dict[DOWN], out parseddownpayment))
            {
                throw new ArgumentOutOfRangeException(DOWN, this.dict[DOWN], DownPaymentBadFormatMessage);
            }

            if (!int.TryParse(this.dict[TERM], out parsedterm))
            {
                throw new ArgumentOutOfRangeException(TERM, this.dict[TERM], LoanTermBadFormatMessage);
            }

            LoanCalcInput loanCalcInput = new LoanCalcInput()
            {
                Amount = parsedloanamount,
                Interest = parsedrate,
                DownPayment = parseddownpayment,
                Term = parsedterm
            };

            return loanCalcInput;
        }

        private float GetRateFromDict()
        {
            string rate = this.dict[RATE];
            bool givenAsPercentage = false;
            if (rate.EndsWith("%"))
            {
                rate = rate.Remove(rate.Length - 1);
                givenAsPercentage = true;
            }

            float parsedrate;
            if (!float.TryParse(rate, out parsedrate))
            {
                throw new ArgumentOutOfRangeException(RATE, this.dict[RATE], InterestRateBadFormatMessage);
            }

            if (!givenAsPercentage)
            {
                parsedrate *= 100;
            }

            return parsedrate;
        }
    }
}
