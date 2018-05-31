using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAryFromUserInput
{
    class CreateInputDictionary
    {
        public class Pair
        {
            public string v1 { get; set; }
            public string v2 { get; set; }


            public Pair(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
        public static Dictionary<string, string> CreateDict()
        {
            const int MAXLINES = 10;
            const int EXPECTEDLINECOUNT = 4;

            List<Pair> input = new List<Pair>();

            for (int i = 0; i < MAXLINES; i++)
            {
                string inputstr = Console.ReadLine();

                if (inputstr is null || inputstr.Length == 0)
                {
                    break;
                }
                string[] inary = inputstr.Split(':');
                input.Add(new Pair(inary[0].Trim().ToLower(), inary[1].Trim().ToLower()));
            }
            int linesgiven = input.Count;
            if (linesgiven != EXPECTEDLINECOUNT)
            {
                Console.WriteLine(string.Format("Unexpected number of input lines: {0}, expecting {1}", linesgiven, EXPECTEDLINECOUNT));
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (Pair pair in input)
            {
                dict.Add(pair.v1, pair.v2);
            }

            return dict;
        }
    }
}
