using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public abstract class Output : Receiver
    {
        public int ID { get; internal set; }

        public double Value { get; private set; }

        void Receiver.Receive(Transmitter t)
        {
            Value = t.Value;
            Receive();
        }

        public abstract void Receive();

    }
}
