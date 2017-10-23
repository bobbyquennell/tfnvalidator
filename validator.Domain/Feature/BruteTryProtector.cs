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

        public static Dictionary<string, TFNtoCheck> tfns;//server side in-memory TFN pool.  (tfn string value, tfn object)
        public static Dictionary<string, Dictionary<string, TFNtoCheck>> securityScanMap;//server side in-memory consecutive number sets and it's tfn list.
        //      key                               value
        //consecutive value (string)      TFN  that contains this consecutive value (dictionary) 
        public static void CreateTfnPool()
        {
            tfns = new Dictionary<string, TFNtoCheck>();//will create a global instance in the Application.
            securityScanMap = new Dictionary<string, Dictionary<string, TFNtoCheck>>();
        }
        public static bool IfBruteAttack(int newTfn)
        {
            int linkDepth = 0;
            //1) keep Tfb pool updated
            RefreshTfnPool(newTfn);

            //2) generate ConsecutiveSets Map for security scan

            foreach (KeyValuePair<string, TFNtoCheck> item in tfns)
            {
                List<IEnumerable<int>> sets = TFNParser.FindConsecutiveNumSets(item.Key).ToList();
                List<string> ConsecutiveSets = new List<string>();
                Dictionary<string, string> linkedMap = new Dictionary<string, string>();
                foreach (var set in sets)
                {
                    ConsecutiveSets.Add(string.Join("", set));
                }
                foreach (string set in ConsecutiveSets)
                {
                    if (securityScanMap.ContainsKey(set))// if this consecutive number exists
                    {
                        // add the TFN to the TFN dictionary under this consecutive number
                        if (! securityScanMap[set].ContainsKey(item.Key)) //item.key : tfn string value
                        {
                            securityScanMap[set].Add(item.Key, item.Value);
                            //linkedMap.Add(item.Key, ""); // linked map: (A, 0) add initial key pair
                        }
                    }
                    else
                    { // add new consecutive number into securityScanMap
                        var newTfnDictionary = new Dictionary<string, TFNtoCheck>();
                        newTfnDictionary.Add(item.Key, item.Value);
                        securityScanMap.Add(set, newTfnDictionary);
                    }
                }
            }
            //3) perform security scan
            /* int linkDepth = 0;
            foreach (KeyValuePair<string, TFNtoCheck> tfn in tfns)
            {
                foreach (KeyValuePair<string, Dictionary<string, TFNtoCheck>> set in securityScanMap)
                {
                    if (set.Value.ContainsKey(tfn.Key))
                    {

                    }
                }
            }*/ 
            return false;
        }

        private static void RefreshTfnPool(int newTfn)
        {
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
        }

        private static void RemoveOldTfn(TFNtoCheck tfn)
        {
            //update Tfb Pool
            var list = tfns.Select(kvp => kvp.Value).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var diffInSeconds = (tfn.TimeStamp - list[i].TimeStamp).TotalSeconds;
                if (diffInSeconds >= 300)
                {
                    tfns.Remove(list[i].Value.ToString());
                }
            };

            //todo: update Tfb consecutive sets pool
        }


    }
}
