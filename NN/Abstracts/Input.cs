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
    public abstract class Input : Transmitter
    {
        /// <summary>
        /// A unique ID for internal network use.
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// The value transmitted from this Transmitter.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Determines whether the specified Identifiable is equal to the current Identifiable.
        /// </summary>
        /// <param name="other">The Identifiable to compare with the current Identifiable.</param>
        /// <returns></returns>
        public bool Equals(Identifiable other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        /// <summary>
        /// Returns the hash code for the current object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
