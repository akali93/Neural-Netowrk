using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    /// <summary>
    /// Represents an input to a neural network.
    /// </summary>
    public abstract class Input : Identifiable, ITransmitter
    {
        /// <summary>
        /// The value transmitted from this Transmitter.
        /// </summary>
        public double Value { get; set; }
    }
}
