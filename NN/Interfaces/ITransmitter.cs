using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    /// <summary>
    /// Represents the ability of an instanct to transmit data.
    /// </summary>
    internal interface ITransmitter : IIdentifiable
    {
        /// <summary>
        /// The value this instance will transmit.
        /// </summary>
        double Value { get; }
    }
}
