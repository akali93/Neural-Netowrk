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
    public abstract class Output : Receiver
    {
        /// <summary>
        /// A unique ID for internal network use.
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// The value recieved by this Receiver.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Recieve a vllue from a Transmitter, keep it in Value prop.
        /// </summary>
        /// <param name="t">The Transmitter send the value.</param>
        void Receiver.Receive(Transmitter t)
        {
            Value = t.Value;
            Receive();
        }

        /// <summary>
        /// This method is fired when this Receiver recieved a value.
        /// </summary>
        public abstract void Receive();

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
