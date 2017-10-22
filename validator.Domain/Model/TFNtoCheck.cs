using System;
using System.Collections.Generic;
using System.Text;

namespace tfn.Domain.Model
{
    public class TFNtoCheck
    {
        public int Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Linked { get; set; }
        public int Remainder { get; set; }
    }
}
