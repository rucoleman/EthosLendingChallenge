using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanCalc;

namespace LoanPaymentCalculatorApp
{
    class InputConverter
    {
        private Dictionary<string, string> _dict;
        public InputConverter(Dictionary<string, string> dict)
        {
            _dict = dict;
        }

        public const string LoanAmountBadFormatMessage = "Loan amount not formatted correctly";
        public const string InterestRateBadFormatMessage = "Interest rate not formatted correctly";
        public const string DownPaymentBadFormatMessage = "Down payment not formatted correctly";
        public const string LoanTermBadFormatMessage = "Loan term not formatted correctly";

        private const string AMOUNT = "amount";
        private const string RATE = "interest";
        private const string DOWN = "downpayment";
        private const string TERM = "term";

        public LoanCalcInput ToLoanCalcInput()
        {
            int parsedloanamount;
            float parsedrate;
            int parseddownpayment;
            int parsedterm;
            if (!int.TryParse(_dict[AMOUNT], out parsedloanamount))
            {
                throw new ArgumentOutOfRangeException(AMOUNT, _dict[AMOUNT], LoanAmountBadFormatMessage);
            }
            parsedrate = GetRateFromDict();
            if (!int.TryParse(_dict[DOWN], out parseddownpayment))
            {
                throw new ArgumentOutOfRangeException(DOWN, _dict[DOWN], DownPaymentBadFormatMessage);
            }
            if (!int.TryParse(_dict[TERM], out parsedterm))
            {
                throw new ArgumentOutOfRangeException(TERM, _dict[TERM], LoanTermBadFormatMessage);
            }

            LoanCalcInput loanCalcInput = new LoanCalcInput()
            {
                amount = parsedloanamount,
                interest = parsedrate,
                downpayment = parseddownpayment,
                term = parsedterm
            };

            return loanCalcInput;
        }
        private float GetRateFromDict()
        {
            string rate = _dict[RATE];
            bool givenAsPercentage = false;
            if (rate.EndsWith("%"))
            {
                rate = rate.Remove(rate.Length - 1);
                givenAsPercentage = true;
            }
            float parsedrate;
            if (!float.TryParse(rate, out parsedrate))
            {
                throw new ArgumentOutOfRangeException(RATE, _dict[RATE], InterestRateBadFormatMessage);
            }
            if (!givenAsPercentage)
            {
                parsedrate *= 100;
            }

            return parsedrate;
        }
    }
}
