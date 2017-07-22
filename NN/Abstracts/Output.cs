using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    /// <summary>
    /// Represents an output from a neural network.
    /// </summary>
    public abstract class Output : Identifiable, IReceiver
    {
        /// <summary>
        /// The value recieved by this Receiver.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Recieve a vllue from a Transmitter, keep it in Value prop.
        /// </summary>
        /// <param name="t">The Transmitter send the value.</param>
        void IReceiver.Receive(ITransmitter t)
        {
            Value = t.Value;
            Receive();
        }

        /// <summary>
        /// This method is fired when this Receiver recieved a value.
        /// </summary>
        public abstract void Receive();
    }
}
