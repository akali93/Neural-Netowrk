using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public abstract class Input : Transmitter
    {
        public int ID { get; internal set; }

        public double Value { get; set; }
    }
}
