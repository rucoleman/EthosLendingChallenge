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
        public LoanCalcInput GetLoanCalcInput()
        {
            int parsedloanamount;
            float parsedrate;
            int parseddownpayment;
            int parsedterm;
            if (!int.TryParse(_dict["amount"], out parsedloanamount))
            {
                Console.WriteLine(string.Format("loan amount given {0} is not in the correct format", _dict["amount"]));
            }
            if (!float.TryParse(_dict["interest"], out parsedrate))
            {
                Console.WriteLine(string.Format("interest rate given {0} is not in the correct format", _dict["interest"]));
            }
            if (!int.TryParse(_dict["downpayment"], out parseddownpayment))
            {
                Console.WriteLine(string.Format("down payment amount given {0} is not in the correct format", _dict["downpayment"]));
            }
            if (!int.TryParse(_dict["term"], out parsedterm))
            {
                Console.WriteLine(string.Format("loan term given {0} is not in the correct format", _dict["term"]));
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
    }
}
