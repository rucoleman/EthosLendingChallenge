using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace LoanCalc
{
    public class LoanCalcInput
    {
        public int amount { get; set; }
        public float interest { get; set; }
        public int downpayment { get; set; }
        public int term { get; set; }
    }
    public class LoanCalcOutput
    {
        [JsonProperty(PropertyName = "monthly payment")]
        public string monthlyPayment { get; set; }

        [JsonProperty(PropertyName = "total interest")]
        public string totalInterest { get; set; }

        [JsonProperty(PropertyName = "total payment")]
        public string totalPayment { get; set; }
    }

    public class LoanCalculator
    {
        public const string DownpaymentMoreThanLoanAmountMessage = "Down payment must be less than loan amount";
        public const string NegativeNumberNotAllowedMessage = "Negative number not allowed";

        public string DoTheMath(LoanCalcInput input)
        {
            // M = payment amount
            // P = principal (amount of money borrowed)
            // J = effective interest rate
            // N = total number of payments

            // P = amount - downpayment
            // M = P * (J / (1 - (1 + J)^-N))
            // TotalPayments = M * N
            // TotalInterest = TotalPayments - P

            ValdateInput(input);

            int principal = input.amount - input.downpayment;
            int numpayments = input.term * 12;
            double negnumpayments = numpayments * -1.0;
            double effectiveinterest = (input.interest / 100.0) / 12;
            double monthlypayment = principal * (effectiveinterest / (1 - Math.Pow(1 + effectiveinterest, negnumpayments)));

            double totalPayments = monthlypayment * numpayments;
            double totalInterest = totalPayments - principal;

            string monthlypaymentFormatted = string.Format("{0:0.00}", monthlypayment);
            string totalInterestFormatted = string.Format("{0:0.00}", totalInterest);
            string totalPaymentsFormatted = string.Format("{0:0.00}", totalPayments);

            LoanCalcOutput loanCalcOutput = new LoanCalcOutput
            {
                monthlyPayment = monthlypaymentFormatted,
                totalInterest = totalInterestFormatted,
                totalPayment = totalPaymentsFormatted
            };
            return JsonConvert.SerializeObject(loanCalcOutput);
        }

        private void ValdateInput(LoanCalcInput input)
        {
            if (input.amount < 0)
            {
                throw new ArgumentOutOfRangeException("input.amount", input.amount, NegativeNumberNotAllowedMessage);
            }
            if (input.downpayment < 0)
            {
                throw new ArgumentOutOfRangeException("input.downpayment", input.downpayment, NegativeNumberNotAllowedMessage);
            }
            if (input.term < 0)
            {
                throw new ArgumentOutOfRangeException("input.term", input.term, NegativeNumberNotAllowedMessage);
            }
            if (input.interest < 0)
            {
                throw new ArgumentOutOfRangeException("input.interest", input.interest, NegativeNumberNotAllowedMessage);
            }
            if (input.downpayment > input.amount)
            {
                throw new ArgumentOutOfRangeException("input.downpayment", input.downpayment, DownpaymentMoreThanLoanAmountMessage);
            }
        }
    }
}
