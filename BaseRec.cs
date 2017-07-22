using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class BaseRec : Receiver
    {
        public int ID { get; set; }
        public double Value { get; set; }

        public void Receive(Transmitter t)
        {
            //Console.WriteLine(t.Value);
            this.Value = t.Value;
        }
    }
}
