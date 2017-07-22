using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    /// <summary>
    /// Represents the ability of an instanct to receive data.
    /// </summary>
    internal interface IReceiver : IIdentifiable
    {
        /// <summary>
        /// This method will fire when a Transmitter transmits data to this Receiver.
        /// </summary>
        /// <param name="t">The Transmitter that sent a value.</param>
        void Receive(ITransmitter t);
    }
}
