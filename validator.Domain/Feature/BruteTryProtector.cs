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
            tfns = new Dictionary<string,TFNtoCheck>();
        }
        public static bool IfBruteAttack(int newTfn)
        {
            var tfn = new TFNtoCheck
            {
                Value = newTfn,
                TimeStamp = DateTime.Now
            };
            if(tfns.Count == 0)
            {
                tfns.Add(newTfn.ToString(), tfn);
            }
            else
            {
                RemoveOldTfn(tfn);
                if (!tfns.ContainsKey(newTfn.ToString())){
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
}
