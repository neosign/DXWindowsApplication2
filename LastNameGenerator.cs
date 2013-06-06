using System;
using System.Collections.Generic;
using System.Text;

namespace DXWindowsApplication2
{
    public class LastNameGenerator
    {
        public string GetLastName()
        {
            StringBuilder sb = new StringBuilder();
            StringGetter[] Getters = Generator[rnd.Next(Generator.Count - 1)];

            bool first = true;
            foreach (StringGetter g in Getters)
            {
                string s = g();

                if (first)
                {
                    s = string.Format("{0}{1}", Char.ToUpper(s[0]), s.Substring(1));
                    first = false;
                }

                sb.Append(s);

            }

            return sb.ToString();
        }

        // certainly I've missed a few
        static List<string> NonEndingConsonantChunks = new List<string>(new string[]
        {
            "b", "br", "bl", "c", "ch", "cr", "cl", "d",
            "dr", "f", "fl", "fr", "g", "gr", "gl", "gh", "h", "j", "k",
            "kr", "kl", "l", "m", "n", "p", "pl", "pr", "q", "r", "s",
            "st", "str", "sh", "sl", "sp", "sk", "sc", "sm", "sn",
            "t", "tr", "v", "w", "x", "y", "z"
        });

        static List<string> EndingConsonantChunks = new List<string>(new string[]
        {
            "b",
            "c", "c", "c", "c", "c", "c", "c", "c", "c",
            "ld",
            "d",
            "f",
            "g",
            "gh",
            "h",
            "k",
            "l",
            "lm",
            "ln",
            "m",
            "n", "n","n", "n", "n", "n", "n", "n", "n", "n", "n", "n",
            "nd",
            "p",
            "r",
            "rd",
            "rn",
            "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s",
            "t", "t", "t", "t", "t",
            "v",
            "w",
            "x",
            "y", "y", "y", "y", "y",
            "z"
        });

        static List<string> Vowelies = new List<string>(new string[] {
            "a",
            "e",
            "i",
            "o",
            "u",
            "ou",
            "ea",
            "ie",
            "ei"
        });

        // seed the random
        Random rnd = null;

        public string NEC()
        {
            return NonEndingConsonantChunks[rnd.Next(NonEndingConsonantChunks.Count - 1)];
        }

        public string V()
        {
            return Vowelies[rnd.Next(Vowelies.Count - 1)];
        }

        public string EC()
        {
            return EndingConsonantChunks[rnd.Next(EndingConsonantChunks.Count - 1)];
        }

        public delegate string StringGetter();

        public List<StringGetter[]> Generator = null;

        StringGetter[] Seq(params StringGetter[] arr)
        {
            return arr;
        }

        public LastNameGenerator()
        {
            Generator = new List<StringGetter[]>();

            rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int32.MaxValue));

            Generator.AddRange(new StringGetter[][] {
                Seq(NEC, V, NEC, V, EC),
                Seq(NEC, V, NEC, V, NEC, V, EC),
                Seq(NEC, V),
                Seq(NEC, V, EC)}
            );
        }

    }

}
