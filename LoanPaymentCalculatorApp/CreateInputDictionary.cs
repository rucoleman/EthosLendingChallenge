using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPaymentCalculatorApp
{
    class CreateInputDictionary
    {
        public static Dictionary<string, string> CreateDict()
        {
            const int MAXLINES = 10;
            const int EXPECTEDLINECOUNT = 4;

            var dict = new Dictionary<string, string>();

            for (int i = 0; i < MAXLINES; i++)
            {
                string inputstr = Console.ReadLine();

                if (inputstr is null || inputstr.Length == 0)
                {
                    break;
                }
                string[] inary = inputstr.Split(':');
                dict.Add(inary[0].Trim().ToLower(), inary[1].Trim().ToLower());
            }
            if (dict.Count != EXPECTEDLINECOUNT)
            {
                throw new ArgumentException(string.Format("Unexpected number of input lines: {0}, expecting {1}", dict.Count, EXPECTEDLINECOUNT));
            }

            return dict;
        }
    }
}
