using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class Neuron : Transmitter, Receiver
    {
        /// <summary>
        /// A unique ID for internal network use.
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// The current value of this neuron.
        /// </summary>
        public double Value { get; private set; }
        /// <summary>
        /// The list of transmitters this neuron receives values from.
        /// </summary>
        private List<Transmitter> Inputs { get; set; }
        /// <summary>
        /// The list of receivers this neuron sends values to.
        /// </summary>
        private List<Receiver> Outputs { get; set; }
        /// <summary>
        /// Used to calculate and som the data received from the inputs.
        /// </summary>
        private Dictionary<int, double> Weights { get; set; }
        /// <summary>
        /// Used to count how many of the inputs already sent values.
        /// </summary>
        private int RecieveCounter;

        public Neuron(int id)
        {
            ID = id;
            RecieveCounter = 0;
            Inputs = new List<Transmitter>();
            Outputs = new List<Receiver>();
            Weights = new Dictionary<int, double>();
        }

        public bool AddInput(Transmitter input, double weight)
        {
            if (InputExists(input))
                return false;
            Inputs.Add(input);
            return UpdateWeight(input, weight);
        }

        public bool AddOutput(Receiver output)
        {
            if (OutputExists(output))
                return false;
            Outputs.Add(output);
            return true;
        }

        public bool UpdateWeight(Transmitter input, double newWeight)
        {
            if (!InputExists(input))
                return false;
            Weights[input.ID] = newWeight;
            return true;
        }

        public void AutoRecieve()
        {
            foreach (Transmitter input in Inputs)
                Receive(input);
        }

        public void Receive(Transmitter t)
        {
            RecieveCounter++;
            Value += t.Value * Weights[t.ID];
            if (RecieveCounter >= Inputs.Count)
                Transmit();
        }

        public void Transmit()
        {
            RecieveCounter = 0;
            foreach (Receiver output in Outputs)
                output.Receive(this);
            this.Value = 0;
        }

        public bool InputExists(Transmitter input)
        {
            return Inputs.Contains(input);
        }

        public bool OutputExists(Receiver output)
        {
            return Outputs.Contains(output);
        }

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

        public override string ToString()
        {
            return "ID: " + ID + ", Value: " + Value;
        }
    }
}
