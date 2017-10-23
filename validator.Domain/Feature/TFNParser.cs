using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace validator.Domain.Feature
{
    public class TFNParser
    {
        public static IEnumerable<IEnumerable<int>> FindConsecutiveNumSets(string tfn, int min = 4)
        {
            List<int> input = GenerateNewTFNtoCheck(tfn).ToList();
            List<List<int>> sets = new List<List<int>>();
            bool IsFound = false;
            for (int setLen = min; setLen <= 9; setLen++)
            {
                IsFound = getConsecutiveSetsWithGivenLen(input, setLen, ref sets);
                if (!IsFound)
                {
                    break;
                }
            }
     
            return sets;
        }

        private static bool getConsecutiveSetsWithGivenLen(List<int> input, int setLen, ref List<List<int>> sets)
        {
            int sum = 0;
            bool Iffound = false;
            for (int i = 0; i <= input.Count - setLen; i++)
            {
                sum = 0;
                //find  setLen numbers consecutive number sets in ascend order              
                
                for (int n = 0; n < setLen; n++)
                {
                    sum += input[i + n];
                }
                //check if in an ascend order
                List<int> subList = input.GetRange(i, setLen);
                
                var OrderedList = subList.OrderBy(x => x).ToList();
                var OrderedListByDescend = subList.OrderByDescending(x => x).ToList();
                bool IsEqual = subList.SequenceEqual(OrderedList);
                bool IsEqualDescend = subList.SequenceEqual(OrderedListByDescend);

                if ((input[i + 1] - input[i]) == 1 && IsEqual && input[i] >= 1 && input[i] <= (10- setLen))
                {
                    if (sum == (setLen * input[i] + ((setLen - 1) * setLen) / 2))
                    {
                        List<int> set = new List<int>();
                        for (int m = 0; m < setLen; m++)
                        {
                            set.Add(input[i + m]);
                        }

                        sets.Add(set);
                        Iffound = true;
                    }
                }
                //find  setLen numbers consecutive number sets in descend order
                if ((input[i] - input[i + 1]) == 1 && IsEqualDescend && input[i] >= setLen && input[i] <= 9)
                {
                    if (sum == (setLen * input[i] - ((setLen - 1) * setLen) / 2))
                    {
                        List<int> set = new List<int>();
                        for (int m = 0; m < setLen; m++)
                        {
                            set.Add(input[i + m]);
                        }
                        sets.Add(set);
                        Iffound = true;
                    }
                }
            }
            return Iffound;
        }

        static private IEnumerable<int> GenerateNewTFNtoCheck(string tfn)
        {
            var digits = tfn.ToString().Select(n => n.ToString()).ToArray();
            int[] array = new int[digits.Length];
            if (digits.Length == 9 || digits.Length == 8)
            {
                for (int i = 0; i < digits.Length; i++)
                {
                    array[i] = Convert.ToInt32(digits[i]);
                }
            }
            return array;
        }
    }
}
