using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanCalc;

namespace LoanPaymentCalculatorApp
{
    class LoanCalcInputFromDict
    {
        private Dictionary<string, string> _dict;
        public LoanCalcInputFromDict(Dictionary<string, string> dict)
        {
            _dict = dict;
        }

        public const string LoanAmountBadFormatMessage = "Loan amount not formatted correctly";
        public const string InterestRateBadFormatMessage = "Interest rate not formatted correctly";
        public const string DownPaymentBadFormatMessage = "Down payment not formatted correctly";
        public const string LoanTermBadFormatMessage = "Loan term not formatted correctly";

        public LoanCalcInput GetLoanCalcInput()
        {
            int parsedloanamount;
            float parsedrate;
            int parseddownpayment;
            int parsedterm;
            if (!int.TryParse(_dict["amount"], out parsedloanamount))
            {
                throw new ArgumentOutOfRangeException("amount", _dict["amount"], LoanAmountBadFormatMessage);
            }
            parsedrate = GetRateFromDict();
            if (!int.TryParse(_dict["downpayment"], out parseddownpayment))
            {
                throw new ArgumentOutOfRangeException("downpayment", _dict["downpayment"], DownPaymentBadFormatMessage);
            }
            if (!int.TryParse(_dict["term"], out parsedterm))
            {
                throw new ArgumentOutOfRangeException("term", _dict["term"], LoanTermBadFormatMessage);
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
            string rate = _dict["interest"];
            bool givenAsPercentage = false;
            if (rate.EndsWith("%"))
            {
                rate = rate.Remove(rate.Length - 1);
                givenAsPercentage = true;
            }
            float parsedrate;
            if (!float.TryParse(rate, out parsedrate))
            {
                throw new ArgumentOutOfRangeException("interest", _dict["interest"], InterestRateBadFormatMessage);
            }
            if (!givenAsPercentage)
            {
                parsedrate *= 100;
            }

            return parsedrate;
        }
    }
}
