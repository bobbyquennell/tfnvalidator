using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using validator.Domain.Model;

namespace validator.Domain.Feature
{
    public class BruteAttackProtector
    {
        //contain all tfns stored in 30 seconds, should stay in server side globally.

        public static Dictionary<string, TFNtoCheck> tfns;//server side in-memory TFN pool.  (tfn string value, tfn object)
        public static Dictionary<string, Dictionary<string, TFNtoCheck>> securityScanMap;//server side in-memory consecutive number sets and it's tfn list.
        //      key                               value
        //consecutive value (string)      TFN  that contains this consecutive value (dictionary) 

        public static Dictionary<string, string> linkedMap;// linked TFN scan result
        public static int timeOutInSecond;

        public static void CreateTfnPool(int TimeOutInSecond = 30)
        {
            tfns = new Dictionary<string, TFNtoCheck>();//will create a global instance in the Application.
            securityScanMap = new Dictionary<string, Dictionary<string, TFNtoCheck>>();
            linkedMap = new Dictionary<string, string>();
            timeOutInSecond = TimeOutInSecond;
        }
        public static bool IfBruteAttack(int newTfn)
        {
            
            //1) keep Tfb pool updated
            RefreshTfnPool(newTfn);

            //2) generate ConsecutiveSets Map and perform security scan

            foreach (KeyValuePair<string, TFNtoCheck> item in tfns)
            {
                //if this tfn already scanned, skip this loop
                if (linkedMap.ContainsKey(item.Key))
                {
                    continue;
                }
                List<IEnumerable<int>> sets = TFNParser.FindConsecutiveNumSets(item.Key).ToList();
                List<string> ConsecutiveSets = new List<string>();
                //Dictionary<string, string> linkedMap = new Dictionary<string, string>();
                foreach (var set in sets)
                {
                    ConsecutiveSets.Add(string.Join("", set));
                }
                foreach (string set in ConsecutiveSets) // set: consecutive number subset from a given TFN
                {
                    if (securityScanMap.ContainsKey(set))// if this consecutive number already exists
                    {
                        if(securityScanMap[set].Count == 1)
                        {
                            //found a A-B single linked TFN pair
                            
                            //update the inital linked map pair: (A,0) to (A, B) 
                            string linkedMapKey = securityScanMap[set].Keys.ToArray()[0];
                            if (linkedMap.ContainsKey(linkedMapKey)) {
                                if (linkedMap[linkedMapKey] == "0")
                                {
                                    linkedMap[linkedMapKey] = item.Key;//item.key : tfn string value
                                }
                                else if(linkedMap[linkedMapKey] == item.Key)
                                {
                                    //found TFN A and B shared the second set !
                                    // do nothing
                                }
                                else if(linkedMap[linkedMapKey] !="0" && linkedMap[linkedMapKey] != item.Key)
                                {
                                    //c!= b, ok, we already have (A, B), now we found (A, C)
                                    //found 3 linked TFN : (A,B) shared set 1, (A, C) shared set 2 !
                                    return true;
                                }
                            }
                            //else
                            //{
                                //todo: error handling
                            //    throw new Exception(); 
                            //}
                        }
                        if(securityScanMap[set].Count == 2)
                        {
                            //found 3 linked TFN shared the same set !
                            return true;
                        }
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

                        // linked map: (A, 0) add initial key pair
                        if (!linkedMap.ContainsKey(item.Key)) { linkedMap.Add(item.Key, "0"); }
                    }
                }
            }
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
                RemoveOldTfn(tfn, timeOutInSecond);
                //every tfn stored in the tfn pool is unique
                if (!tfns.ContainsKey(newTfn.ToString()))
                {
                    tfns.Add(newTfn.ToString(), tfn);
                }
            }
        }

        private static void RemoveOldTfn(TFNtoCheck tfn, int timeOut= 30)
        {
            //update Tfb Pool
            var list = tfns.Select(kvp => kvp.Value).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var diffInSeconds = (tfn.TimeStamp - list[i].TimeStamp).TotalSeconds;
                if (diffInSeconds >= timeOut)
                {
                    tfns.Remove(list[i].Value.ToString());//FIFO, remove the oldes TFN
                    //remove Old TFN from securityScanMap
                    foreach(KeyValuePair<string, Dictionary<string, TFNtoCheck>> item in securityScanMap)
                    {
                        item.Value.Remove(list[i].Value.ToString());
                    }
                    //remove Old TFN from linkedMap
                    linkedMap.Remove(list[i].Value.ToString());
                }
            };


        }


    }
}
