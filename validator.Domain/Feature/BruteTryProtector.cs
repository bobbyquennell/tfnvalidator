using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using validator.Domain.Model;

namespace validator.Domain.Feature
{
    public class BruteTryProtector
    {
        //contain all tfns stored in 30 seconds, should stay in server side globally.

        public static Dictionary<string, TFNtoCheck> tfns;//server side in-memory TFN pool.

        public static void CreateTfnPool()
        {
            tfns = new Dictionary<string, TFNtoCheck>();
        }
        public static bool IfBruteAttack(int newTfn)
        {

            //List<string> conNumGroup= ConNumberParser.findConsecutiveNumbers(newTfn.ToString(), 4);



            var tfn = new TFNtoCheck
            {
                Value = newTfn,
                TimeStamp = DateTime.Now
            };
            if (tfns.Count == 0)
            {
                tfns.Add(newTfn.ToString(), tfn);
            }
            else
            {
                RemoveOldTfn(tfn);
                if (!tfns.ContainsKey(newTfn.ToString()))
                {
                    tfns.Add(newTfn.ToString(), tfn);
                }
            }

            //check if  3 linked TFN found in the tfns list
            return false;
        }

        private static void RemoveOldTfn(TFNtoCheck tfn)
        {
            var list = tfns.Select(kvp => kvp.Value).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var diffInSeconds = (tfn.TimeStamp - list[i].TimeStamp).TotalSeconds;
                if (diffInSeconds >= 30)
                {
                    tfns.Remove(list[i].Value.ToString());
                }
            };
        }


    }

    public class ConNumberParser
    {

        public static List<String> findConsecutiveNumbers(String s, int length)
        {
            List<String> matches = new List<String>();

            // seek to first digit
            int i = 0;
            while (i < s.Length && !Char.IsDigit(s[i]))
            {
                i++;
            }
            // store the beginning of a consecutive series
            int? matchedIndex = i;
            // and the direction
            Boolean increasing = false;

            for (i=0; i < s.Length; i++)
            {
                Char c = s[i];
                if (Char.IsDigit(c))
                {
                    if (null == matchedIndex)
                    {
                        // first digit after other characters
                        matchedIndex = i;
                        increasing = false;
                        continue;
                    }
                    int difference = Convert.ToInt32(c) - Convert.ToInt32(s[i - 1]);
                    if (Math.Abs(difference) > 1)
                    {
                        // no conescutive digits
                        matchedIndex = i;
                        increasing = false;
                        continue;
                    }
                    if (length > 2)
                    {
                        if (false == increasing)
                        {
                            // found first consecutive digit
                            increasing = (difference == 1);
                            continue;
                        }
                        int expectedDiff = increasing ? 1 : -1;
                        if (difference != expectedDiff)
                        {
                            // no conescutive digits in the right direction
                            matchedIndex = i - 1;
                            increasing = (difference == 1);
                            continue;
                        }
                    }
                    if (i - matchedIndex + 1 == length)
                    {
                        // consecutive digits of given length found
                        matches.Add(s.Substring((int)matchedIndex, (int)matchedIndex + length));
                        matchedIndex++; // move by one to keep matching overlapping
                                        // sequences
                    }
                }
                else
                {
                    matchedIndex = null;
                }
            }

            return matches;
        }
    }
}
