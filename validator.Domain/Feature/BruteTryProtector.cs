using System;
using System.Collections.Generic;
using System.Text;
using validator.Domain.Model;

namespace validator.Domain.Feature
{
    public class BruteTryProtector
    {
        //contain all tfns stored in 30 seconds, should stay in server side globally.

        private List<TFNtoCheck> tfns = new List<TFNtoCheck>();//TODO:move to server side in memory cache.

        public bool IfBruteAttack(int newTfn)
        {
            var tfn = new TFNtoCheck
            {
                Value = newTfn,
                TimeStamp = DateTime.Now
            };
            if(tfns.Count == 0)
            {
                tfns.Add(tfn);
            }
            else
            {
                RemoveOldTfn(tfn);
            }

            //check if  3 linked TFN found in the tfns list
            return false;
        }

        private void RemoveOldTfn(TFNtoCheck tfn)
        {
            for (int i = 0; i < tfns.Count; i++)
            {
                var diffInSeconds = (tfns[i].TimeStamp - tfn.TimeStamp).TotalSeconds;
                if (diffInSeconds >= 30)
                {
                    tfns.RemoveAt(i);
                }
            };
        }
    }
}
