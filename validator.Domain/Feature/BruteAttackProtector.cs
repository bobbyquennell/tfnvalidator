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

        public static List<TFNtoCheck> tfns;//server side in-memory TFN pool.
        public static Dictionary<string, string> linkedMap;// linked TFN scan result (A, B) A linked to B, (C, 0) C linked to no-one
        public static int timeOutInSecond;

        public static void CreateTfnPool(int TimeOutInSecond = 30)
        {
            tfns = new List<TFNtoCheck>();//will create a global instance in the Application.
            linkedMap = new Dictionary<string, string>();
            timeOutInSecond = TimeOutInSecond;
        }
        public static bool IfBruteAttack(int newTfn)
        {
            string LinkedTfn = string.Empty;

            //1) keep Tfn pool updated
            RefreshTfnPool(newTfn);

            if(!linkedMap.ContainsKey(newTfn.ToString()))
            {
                //and an entry in the linkedMap for the newTfn:  for the 1st time
                linkedMap.Add(newTfn.ToString(), string.Empty); //(newTfn, 0)
            }

            //2) Scan in the TFN pool,check with newTFN
            for (int i = 0; i < tfns.Count; i++)
            {
                //if newTfn  "linked" to tfns[i]
                if(TFNParser.IfTfnPairLinked(tfns[i].Value.ToString(), newTfn.ToString()))
                {
                    LinkedTfn = newTfn.ToString();
                    //store the "linked" relationship in the linkedMap
                    if (linkedMap.ContainsKey(tfns[i].Value.ToString()) && linkedMap[tfns[i].Value.ToString()] == string.Empty )//if find (A, 0)
                    {
                        // found the first linked pair, store in the map (A, B)
                        linkedMap[tfns[i].Value.ToString()] = newTfn.ToString();

                        if(linkedMap.ContainsKey(newTfn.ToString()) && linkedMap[newTfn.ToString()] == string.Empty)//if find (B,0)
                        {
                            linkedMap[newTfn.ToString()] = tfns[i].Value.ToString();//store the reverse pair (B, A) into the map
                        }
                        else if (linkedMap.ContainsKey(newTfn.ToString()) && linkedMap[newTfn.ToString()] != string.Empty)//if find (B,X)
                        {
                            if (tfns[i].Value.ToString() != newTfn.ToString()) {
                                return true;
                            }
                        }
                    }
                    else if (linkedMap.ContainsKey(tfns[i].Value.ToString()) && linkedMap[tfns[i].Value.ToString()] != string.Empty)
                    {
                        // found the second linked pair
                        if(linkedMap[tfns[i].Value.ToString()] == newTfn.ToString())
                        {

                        }
                        if (linkedMap.ContainsKey(newTfn.ToString()) && linkedMap[newTfn.ToString()] == string.Empty)//if find (B,0)
                        {
                            linkedMap[newTfn.ToString()] = tfns[i].Value.ToString();//store the reverse pair (B, A) into the map
                        }
                        return true;
                    }
                }
                //
            }
            
            //3) store the new TFN into the Tfn pool
            InsertTfnPool(newTfn);
            return false;
        }

        private static void RefreshTfnPool(int newTfn)
        {
            var tfn = new TFNtoCheck
            {
                Value = newTfn,
                TimeStamp = DateTime.Now
            };
            if (tfns.Count > 0)
            {
                RemoveOldTfn(tfn, timeOutInSecond);
            }

        }
        private static void InsertTfnPool(int newTfn)
        {
            var tfn = new TFNtoCheck
            {
                Value = newTfn,
                TimeStamp = DateTime.Now
            };
            tfns.Add(tfn); 

        }

        private static void RemoveOldTfn(TFNtoCheck tfn, int timeOut= 30)
        {
            //update Tfb Pool
            var list = tfns;
            int OldTfnValue = 0;
            var removals = new List<string>();
            for (int i = 0; i < tfns.Count; i++)
            {
                var diffInSeconds = (tfn.TimeStamp - list[i].TimeStamp).TotalSeconds;
                if (diffInSeconds >= timeOut)
                {
                    
                    OldTfnValue = list[i].Value;
                    tfns.RemoveAt(i);//FIFO, remove the oldes TFN
                    //remove Old TFN relationship from linkedMap
                    var remainList = tfns.Select(t => t).Where(t => t.Value == OldTfnValue).ToList();
                    if (remainList.Count == 0)// is tfn A does not exist in tfn pool anymore
                    {
                        //remove (A, A), (A, X), and (X, A) from linkedMap

                        if (linkedMap.ContainsKey(OldTfnValue.ToString())) {
                            linkedMap.Remove(OldTfnValue.ToString());// (A, A) or (A, X) will be removed.
                        }


                            //remove (X, A) from Linked Map
                        foreach (KeyValuePair<string, string> item in linkedMap)
                        {
                            if (item.Value.ToString() == OldTfnValue.ToString())
                            {
                                removals.Add(item.Key.ToString());
                            }
                        }
                        for (int n = 0; n < removals.Count; n++)
                        {
                            linkedMap.Remove(removals[n]);
                        }
                    }
                    if (remainList.Count == 1)// is only one tfn A left in tfn pool 
                    {
                        // only remove (A,A), remain (A, X), and (X, A)
                        if (linkedMap.ContainsKey(OldTfnValue.ToString()) && linkedMap[OldTfnValue.ToString()] == OldTfnValue.ToString())
                        {
                            linkedMap.Remove(OldTfnValue.ToString());
                        }

                    }
                    if (remainList.Count > 1)
                    {
                        //remain (A,A), (A, X), and (X, A), do nothing
                    }

                    
                }
            };

 


        }


    }
}
